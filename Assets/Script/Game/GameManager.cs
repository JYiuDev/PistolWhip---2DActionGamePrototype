using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    private float interactDistance = 1f;

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
    }

    private void CheckInteractionWithLevelEndObjects()
    {
        string levelName = SceneManager.GetActiveScene().name;

        if (GameObject.FindWithTag("Player") != null && GameObject.FindWithTag("LevelOneEntry") != null)
        {
            if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("LevelOneEntry").transform.position) <= interactDistance)
            {
                SceneManager.LoadScene("GetToEndTest");
            }
        } 

        if (GameObject.FindWithTag("Player") != null && GameObject.FindWithTag("LevelTwoEntry") != null)
        {
            if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("LevelTwoEntry").transform.position) <= interactDistance)
            {
                SceneManager.LoadScene("LevelKillTest");
            }
        } 

        if (GameObject.FindWithTag("Player") != null && GameObject.FindWithTag("LevelThreeEntry") != null)
        {
            if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("LevelThreeEntry").transform.position) <= interactDistance)
            {
                SceneManager.LoadScene("LevelHeistTest");
            }
        } 

        if (GameObject.FindWithTag("Player") != null && GameObject.FindWithTag("EndObject"))
        {
            if (Vector2.Distance(GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("EndObject").transform.position) <= interactDistance)
            {
                LevelComplete();
                SceneManager.LoadScene("MainWorldTest");
                Debug.Log("You have returned to the main world and completed " + levelName);
                PrintLevelCompletionStatistics();
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
    }

    private Dictionary<string, float> levelCompletionTimes = new Dictionary<string, float>();
    private Dictionary<string, float> levelTotalEnemiesRemaining = new Dictionary<string, float>();
    float totalEnemiesRemaining;
    float totalShieldsUsed;
    float totalBottlesUsed;
    float totalGunsUsed;
    

    public void LevelComplete()
    {
        string levelName = SceneManager.GetActiveScene().name;

        if (!levelCompletionTimes.ContainsKey(levelName))
        {
            levelCompletionTimes.Add(levelName, Time.timeSinceLevelLoad);
            levelTotalEnemiesRemaining.Add(levelName, totalEnemiesRemaining);
        }
        else
        {
            float previousCompletionTime = levelCompletionTimes[levelName];
            float newCompletionTime = Time.timeSinceLevelLoad;

            if (newCompletionTime < previousCompletionTime)
            {
                levelCompletionTimes[levelName] = newCompletionTime;
            }

            float previousTotalEnemiesRemaining = levelTotalEnemiesRemaining[levelName];
            float newTotalEnemiesRemaining = totalEnemiesRemaining;

            if (newTotalEnemiesRemaining < previousTotalEnemiesRemaining)
            {
                levelTotalEnemiesRemaining[levelName] = newTotalEnemiesRemaining;
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
            }
        }
        foreach (KeyValuePair<string, float> entry in levelTotalEnemiesRemaining)
        {
            if (entry.Key == SceneManager.GetActiveScene().name)
            {
                Debug.Log("You completed the level " + entry.Key + " with a total enemies remaining = " + entry.Value + " out of " + totalEnemyCount);
            }
        }
    }
}
