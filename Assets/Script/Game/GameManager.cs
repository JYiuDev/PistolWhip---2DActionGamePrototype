using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    private float interactDistance = 1f;

    public bool isPlaying = false;
    private string filename;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    float totalEnemyCount;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        totalEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        filename = Application.dataPath + "/Playthrough_" + timestamp + ".csv";

        UnityEditor.EditorApplication.playModeStateChanged += PlayModeStateChanged;

    }

    private void OnDestroy()
    {
        UnityEditor.EditorApplication.playModeStateChanged -= PlayModeStateChanged;
    }

    private void PlayModeStateChanged(UnityEditor.PlayModeStateChange stateChange)
    {
        if (stateChange == UnityEditor.PlayModeStateChange.EnteredPlayMode)
        {
            isPlaying = true;
        }
        else if (stateChange == UnityEditor.PlayModeStateChange.ExitingPlayMode)
        {
            isPlaying = false;
        }
    }

    float levelOneCount;
    float levelTwoCount;
    float levelThreeCount;

    private void CheckInteractionWithLevelEndObjects()
    {
        string levelName = SceneManager.GetActiveScene().name;

        if (GameObject.FindWithTag("Player") != null && GameObject.FindWithTag("LevelOneEntry") != null)
        {
            if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("LevelOneEntry").transform.position) <= interactDistance)
            {
                SceneManager.LoadScene("GetToEndTest");
                levelOneCount++;
            }
        } 

        if (GameObject.FindWithTag("Player") != null && GameObject.FindWithTag("LevelTwoEntry") != null)
        {
            if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("LevelTwoEntry").transform.position) <= interactDistance)
            {
                SceneManager.LoadScene("LevelKillTest");
                levelTwoCount++;
            }
        } 

        if (GameObject.FindWithTag("Player") != null && GameObject.FindWithTag("LevelThreeEntry") != null)
        {
            if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("LevelThreeEntry").transform.position) <= interactDistance)
            {
                SceneManager.LoadScene("LevelHeistTest");
                levelThreeCount++;
            }
        } 

        //GET TO END LEVEL OBJECTIVE
        if (GameObject.FindWithTag("Player") != null && GameObject.FindWithTag("EndObject") && SceneManager.GetActiveScene().name == "GetToEndTest")
        {
            if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("EndObject").transform.position) <= interactDistance)
            {
                LevelComplete();
                SceneManager.LoadScene("MainWorldTest");
                Debug.Log("You have returned to the main world and completed " + levelName);
                PrintLevelCompletionStatistics();
                if (isPlaying)
                {
                    //WriteCSV();
                }
            }
        }

        //KILL ALL ENEMIES LEVEL OBJECTIVE

        if (SceneManager.GetActiveScene().name == "LevelKillTest")
        {
            if (GameObject.FindWithTag("Player") != null && GameObject.FindWithTag("EndObject") && GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().maxSpawns >= 20 && GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
            {
                if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("EndObject").transform.position) <= interactDistance)
                {
                    LevelComplete();
                    SceneManager.LoadScene("MainWorldTest");
                    Debug.Log("You have returned to the main world and completed " + levelName);
                    PrintLevelCompletionStatistics();
                    if (isPlaying)
                    {
                        //WriteCSV();
                    }
                }
            }
        }

        GameObject weaponPosObject = GameObject.FindWithTag("WeaponPos");  

        //HEIST LEVEL OBJECTIVE
        if (GameObject.FindWithTag("Player") != null && GameObject.FindWithTag("EndObject") && weaponPosObject != null && weaponPosObject.transform.childCount > 0 && weaponPosObject.transform.GetChild(0).CompareTag("HeistItem") && SceneManager.GetActiveScene().name == "LevelHeistTest")
        {
            if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("EndObject").transform.position) <= interactDistance)
            {
                LevelComplete();
                SceneManager.LoadScene("MainWorldTest");
                Debug.Log("You have returned to the main world and completed " + levelName);
                PrintLevelCompletionStatistics();
                if (isPlaying)
                {
                    //WriteCSV();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteractionWithLevelEndObjects();
        }

        totalEnemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
        combo = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>().styleAmount;
        extrahits = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>().extraHitCount;
        currentAdr = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>().adrenalinePoints;
        rank = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>().rankTitle;

        if (SceneManager.GetActiveScene().name == "MainWorldTest")
        {

            //LEVEL ONE HIGH SCORES
            if (levelCompletionTimes.ContainsKey("GetToEndTest"))
            {
                GameObject.FindGameObjectWithTag("LevelOneTime").GetComponent<Text>().text = "Time: " + levelCompletionTimes["GetToEndTest"];
            }
            //GameObject.FindGameObjectWithTag("LevelOneTime").GetComponent<Text>().text = "Time: " + levelCompletionTimes["GetToEndTest"];

            if (levelCombo.ContainsKey("GetToEndTest"))
            {
                if (levelCombo["GetToEndTest"] <= 0)
                {
                    GameObject.FindGameObjectWithTag("LevelOneRank").GetComponent<Text>().text = "Rank: " + "";
                }
                else if (levelCombo["GetToEndTest"] <= 100)
                {
                    GameObject.FindGameObjectWithTag("LevelOneRank").GetComponent<Text>().text = "Rank: " + "Deputy";
                }
                else if (levelCombo["GetToEndTest"] > 100 && levelCombo["GetToEndTest"] <= 250)
                {
                    GameObject.FindGameObjectWithTag("LevelOneRank").GetComponent<Text>().text = "Rank: " + "Sheriff";
                }
                else if (levelCombo["GetToEndTest"] > 250 && levelCombo["GetToEndTest"] <= 450)
                {
                    GameObject.FindGameObjectWithTag("LevelOneRank").GetComponent<Text>().text = "Rank: " + "Vigilante";
                }
                else if (levelCombo["GetToEndTest"] > 450 && levelCombo["GetToEndTest"] <= 700)
                {
                    GameObject.FindGameObjectWithTag("LevelOneRank").GetComponent<Text>().text = "Rank: " + "Hero";
                }
                else if (levelCombo["GetToEndTest"] > 700 && levelCombo["GetToEndTest"] <= 1000)
                {
                    GameObject.FindGameObjectWithTag("LevelOneRank").GetComponent<Text>().text = "Rank: " + "Legend";
                }
                else if (levelCombo["GetToEndTest"] > 1000 && levelCombo["GetToEndTest"] <= 1500)
                {
                    GameObject.FindGameObjectWithTag("LevelOneRank").GetComponent<Text>().text = "Rank: " + "Myth";
                }
                else if (levelCombo["GetToEndTest"] > 1500)
                {
                    GameObject.FindGameObjectWithTag("LevelOneRank").GetComponent<Text>().text = "Rank: " + "Myth+";
                }
            }

            if (levelTotalEnemiesRemaining.ContainsKey("GetToEndTest"))
            {
                GameObject.FindGameObjectWithTag("LevelOneKills").GetComponent<Text>().text = "Remaining Enemies: " + levelTotalEnemiesRemaining["GetToEndTest"];
            }
            //GameObject.FindGameObjectWithTag("LevelOneKills").GetComponent<Text>().text = "Remaining Enemies: " + levelTotalEnemiesRemaining["GetToEndTest"];

            //LEVEL TWO HIGH SCORES

            if (levelCompletionTimes.ContainsKey("LevelKillTest"))
            {
                GameObject.FindGameObjectWithTag("LevelTwoTime").GetComponent<Text>().text = "Time: " + levelCompletionTimes["LevelKillTest"];
            }

            //GameObject.FindGameObjectWithTag("LevelTwoTime").GetComponent<Text>().text = "Time: " + levelCompletionTimes["LevelKillTest"];

            if (levelCombo.ContainsKey("LevelKillTest"))
            {
                if (levelCombo["LevelKillTest"] <= 0)
                {
                    GameObject.FindGameObjectWithTag("LevelTwoRank").GetComponent<Text>().text = "Rank: " + "";
                }
                else if (levelCombo["LevelKillTest"] <= 100)
                {
                    GameObject.FindGameObjectWithTag("LevelTwoRank").GetComponent<Text>().text = "Rank: " + "Deputy";
                }
                else if (levelCombo["LevelKillTest"] > 100 && levelCombo["LevelKillTest"] <= 250)
                {
                    GameObject.FindGameObjectWithTag("LevelTwoRank").GetComponent<Text>().text = "Rank: " + "Sheriff";
                }
                else if (levelCombo["LevelKillTest"] > 250 && levelCombo["LevelKillTest"] <= 450)
                {
                    GameObject.FindGameObjectWithTag("LevelTwoRank").GetComponent<Text>().text = "Rank: " + "Vigilante";
                }
                else if (levelCombo["LevelKillTest"] > 450 && levelCombo["LevelKillTest"] <= 700)
                {
                    GameObject.FindGameObjectWithTag("LevelTwoRank").GetComponent<Text>().text = "Rank: " + "Hero";
                }
                else if (levelCombo["LevelKillTest"] > 700 && levelCombo["LevelKillTest"] <= 1000)
                {
                    GameObject.FindGameObjectWithTag("LevelTwoRank").GetComponent<Text>().text = "Rank: " + "Legend";
                }
                else if (levelCombo["LevelKillTest"] > 1000 && levelCombo["LevelKillTest"] <= 1500)
                {
                    GameObject.FindGameObjectWithTag("LevelTwoRank").GetComponent<Text>().text = "Rank: " + "Myth";
                }
                else if (levelCombo["LevelKillTest"] > 1500)
                {
                    GameObject.FindGameObjectWithTag("LevelTwoRank").GetComponent<Text>().text = "Rank: " + "Myth+";
                }
            }

            if (levelTotalEnemiesRemaining.ContainsKey("LevelKillTest"))
            {
                GameObject.FindGameObjectWithTag("LevelTwoKills").GetComponent<Text>().text = "Remaining Enemies: " + levelTotalEnemiesRemaining["LevelKillTest"];
            }
            //GameObject.FindGameObjectWithTag("LevelTwoKills").GetComponent<Text>().text = "Remaining Enemies: " + levelTotalEnemiesRemaining["LevelKillTest"];

            //LEVEL THREE HIGH SCORES

            if (levelCompletionTimes.ContainsKey("LevelHeistTest"))
            {
                GameObject.FindGameObjectWithTag("LevelThreeTime").GetComponent<Text>().text = "Time: " + levelCompletionTimes["LevelHeistTest"];
            }

            //GameObject.FindGameObjectWithTag("LevelThreeTime").GetComponent<Text>().text = "Time: " + levelCompletionTimes["LevelHeistTest"];

            if (levelCombo.ContainsKey("LevelHeistTest"))
            {
                if (levelCombo["LevelHeistTest"] <= 0)
                {
                    GameObject.FindGameObjectWithTag("LevelThreeRank").GetComponent<Text>().text = "Rank: " + "";
                }
                else if (levelCombo["LevelHeistTest"] <= 100)
                {
                    GameObject.FindGameObjectWithTag("LevelThreeRank").GetComponent<Text>().text = "Rank: " + "Deputy";
                }
                else if (levelCombo["LevelHeistTest"] > 100 && levelCombo["LevelHeistTest"] <= 250)
                {
                    GameObject.FindGameObjectWithTag("LevelThreeRank").GetComponent<Text>().text = "Rank: " + "Sheriff";
                }
                else if (levelCombo["LevelHeistTest"] > 250 && levelCombo["LevelHeistTest"] <= 450)
                {
                    GameObject.FindGameObjectWithTag("LevelThreeRank").GetComponent<Text>().text = "Rank: " + "Vigilante";
                }
                else if (levelCombo["LevelHeistTest"] > 450 && levelCombo["LevelHeistTest"] <= 700)
                {
                    GameObject.FindGameObjectWithTag("LevelThreeRank").GetComponent<Text>().text = "Rank: " + "Hero";
                }
                else if (levelCombo["LevelHeistTest"] > 700 && levelCombo["LevelHeistTest"] <= 1000)
                {
                    GameObject.FindGameObjectWithTag("LevelThreeRank").GetComponent<Text>().text = "Rank: " + "Legend";
                }
                else if (levelCombo["LevelHeistTest"] > 1000 && levelCombo["LevelHeistTest"] <= 1500)
                {
                    GameObject.FindGameObjectWithTag("LevelThreeRank").GetComponent<Text>().text = "Rank: " + "Myth";
                }
                else if (levelCombo["LevelHeistTest"] > 1500)
                {
                    GameObject.FindGameObjectWithTag("LevelThreeRank").GetComponent<Text>().text = "Rank: " + "Myth+";
                }
            }

            if (levelTotalEnemiesRemaining.ContainsKey("LevelHeistTest"))
            {
                GameObject.FindGameObjectWithTag("LevelThreeKills").GetComponent<Text>().text = "Remaining Enemies: " + levelTotalEnemiesRemaining["LevelHeistTest"];
            }
            //GameObject.FindGameObjectWithTag("LevelThreeKills").GetComponent<Text>().text = "Remaining Enemies: " + levelTotalEnemiesRemaining["LevelHeistTest"];
        }

    }

    private Dictionary<string, float> levelCompletionTimes = new Dictionary<string, float>();
    private Dictionary<string, float> levelTotalEnemiesRemaining = new Dictionary<string, float>();
    private Dictionary<string, float> levelCombo = new Dictionary<string, float>();
    float combo;
    bool died;
    string rank;
    float currentAdr;
    float extrahits;
    float totalEnemiesRemaining;
    float totalShieldsUsed;
    float totalBottlesUsed;
    float totalGunsUsed;
    float xOnDeath;
    float yOnDeath;
    

    public void LevelComplete()
    {
        string levelName = SceneManager.GetActiveScene().name;

        if (!levelCompletionTimes.ContainsKey(levelName))
        {
            levelCompletionTimes.Add(levelName, Time.timeSinceLevelLoad);
            //levelTotalEnemiesRemaining.Add(levelName, totalEnemiesRemaining);
            //levelCombo.Add(levelName, combo);
        }
        else
        {
            float previousCompletionTime = levelCompletionTimes[levelName];
            float newCompletionTime = Time.timeSinceLevelLoad;

            if (newCompletionTime < previousCompletionTime || previousCompletionTime == float.NaN)
            {
                levelCompletionTimes[levelName] = newCompletionTime;
            }

            //float previousTotalEnemiesRemaining = levelTotalEnemiesRemaining[levelName];
            //float newTotalEnemiesRemaining = totalEnemiesRemaining;

            //if (newTotalEnemiesRemaining < previousTotalEnemiesRemaining || previousTotalEnemiesRemaining == float.NaN)
            //{
            //    levelTotalEnemiesRemaining[levelName] = newTotalEnemiesRemaining;
            //}

            //float previousLevelCombo = levelCombo[levelName];
            //float newLevelCombo = combo;

            //if (previousLevelCombo < newLevelCombo || previousLevelCombo == float.NaN)
            //{
            //    levelCombo[levelName] = newLevelCombo;
            //}
        }

        if (!levelTotalEnemiesRemaining.ContainsKey(levelName))
        {
            levelTotalEnemiesRemaining.Add(levelName, totalEnemiesRemaining);
        } else
        {
            float previousTotalEnemiesRemaining = levelTotalEnemiesRemaining[levelName];
            float newTotalEnemiesRemaining = totalEnemiesRemaining;

            if (newTotalEnemiesRemaining < previousTotalEnemiesRemaining || previousTotalEnemiesRemaining == float.NaN)
            {
                levelTotalEnemiesRemaining[levelName] = newTotalEnemiesRemaining;
            }
        }

        if (!levelCombo.ContainsKey(levelName))
        {
            levelCombo.Add(levelName, combo);
        }
        else
        {
            float previousLevelCombo = levelCombo[levelName];
            float newLevelCombo = combo;

            if (previousLevelCombo < newLevelCombo || previousLevelCombo == float.NaN)
            {
                levelCombo[levelName] = newLevelCombo;
            }
        }

    }

    public void PrintLevelCompletionStatistics()
    {
        foreach (KeyValuePair<string, float> entry in levelCompletionTimes)
        {
            if (entry.Key == SceneManager.GetActiveScene().name)
            {
                Debug.Log("You completed the level " + entry.Key + " in " + Time.timeSinceLevelLoad + " seconds" + ", Compared to your highscore " + entry.Value + " seconds.");
                Debug.Log("You have completed" + entry.Key + levelOneCount + " times.");
            }
        }
        foreach (KeyValuePair<string, float> entry in levelTotalEnemiesRemaining)
        {
            if (entry.Key == SceneManager.GetActiveScene().name)
            {
                Debug.Log("You completed the level " + entry.Key + " with a total enemies remaining = " + entry.Value + " out of " + totalEnemyCount);
            }
        }

        totalGunsUsed = GameObject.FindWithTag("Whip").GetComponent<WhipPullClick>().gunCount;
        totalShieldsUsed = GameObject.FindWithTag("Whip").GetComponent<WhipPullClick>().shieldCount;
        totalBottlesUsed = GameObject.FindWithTag("Whip").GetComponent<WhipPullClick>().bottleCount;
        Debug.Log("You used the bottle " + totalBottlesUsed + " times." +
                  " The gun a total of " + totalGunsUsed + " times." +
                  " The shield a total of " +totalShieldsUsed + " times.");

        Debug.Log("You have completed Level One " + levelOneCount + " times." +
                  " Level Two " + levelTwoCount + " times." +
                  " Level Three " + levelThreeCount + " times.");

        died = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>().hasDied;
        xOnDeath = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>().xCoord;
        yOnDeath = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>().yCoord;
        //Debug.Log("You died at " + xOnDeath + "," + yOnDeath);

    }

    public void WriteCSV()
    {
        bool fileExists = File.Exists(filename);

        using (StreamWriter tw = new StreamWriter(filename, true))
        {
            if (!fileExists)
            {
                string header = "Level Name,Level Completion Time,Total Enemy Count, Total Enemies Remaining,Total Bottles Used,Total Guns Used,Total Shields Used,Level One Completions,Level Two Completions,Level Three Completions,Adrenaline,Rank,Combo,Extra Hits,Death,xOnDeath,yOnDeath";
                tw.WriteLine(header);
            }

            string data = SceneManager.GetActiveScene().name + "," + Time.timeSinceLevelLoad + "," + totalEnemyCount + "," + totalEnemiesRemaining + "," + totalBottlesUsed + "," + totalGunsUsed + "," + totalShieldsUsed + "," + levelOneCount + "," + levelTwoCount + "," + levelThreeCount + "," + currentAdr + "," + rank + "," + combo + "," + extrahits + "," + died + "," + xOnDeath + "," + yOnDeath;
            tw.WriteLine(data);
        }
    }
}
