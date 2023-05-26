using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class styleScriptTwo : MonoBehaviour
{
    // Public variables
    public float styleAmount;
    public float styleDecay;
    public string rankTitle;
    public float multikillTimer;
    public int multikillCount;
    public float adrenalinePoints;
    public float adrenalineGain;
    public bool hasAdrenaline;

    private void Awake()
    {

    }

    private void Start()
    {
        rankTitle = "";
        styleAmount = 0f;
        styleDecay = 0f;
        multikillTimer = 0f;
        multikillCount = 0;
        hasAdrenaline = false;
        hasDied = false;
    }

    private void Update()
    {
        // Decay and multikill checks
        styleAmount -= styleDecay * Time.deltaTime;
        adrenalinePoints += adrenalineGain * Time.deltaTime;
        multikillTimer -= Time.deltaTime;
        if (multikillTimer <= 0)
        {
            multikillCount = 0;
            multikillTimer = 0;
        }

        // Ranks and style count
        if (styleAmount <= 0)
        {
            styleAmount = 0;
            rankTitle = "";
            styleDecay = 0f;
            adrenalineGain = 1;
        }
        else if (styleAmount <= 100)
        {
            rankTitle = "Deputy";
            styleDecay = 5f;
            adrenalineGain = 2;
        }
        else if (styleAmount > 100 && styleAmount <= 250)
        {
            rankTitle = "Sherrif";
            styleDecay = 7.5f;
            adrenalineGain = 3;
        }
        else if (styleAmount > 250 && styleAmount <= 450)
        {
            rankTitle = "Vigilante";
            styleDecay = 12.5f;
            adrenalineGain = 5;
        }
        else if (styleAmount > 450 && styleAmount <= 700)
        {
            rankTitle = "Hero";
            styleDecay = 20f;
            adrenalineGain = 8;
        }
        else if (styleAmount > 700 && styleAmount <= 1000)
        {
            rankTitle = "Legend";
            styleDecay = 32.5f;
            adrenalineGain = 13;
        }
        else if (styleAmount > 1000 && styleAmount <= 20000)
        {
            rankTitle = "Myth";
            styleDecay = 50f;
            adrenalineGain = 21;
        } 
        else if (styleAmount > 20000)
        {
            rankTitle = "Myth+";
            styleDecay = 100f;
            adrenalineGain = 30;
        }

        //adrenaline stuff
        if (adrenalinePoints >= 100)
        {
            adrenalinePoints = 100;
            hasAdrenaline = true;
            Debug.Log("Adrenaline has kicked in, you can take an extra hit!");
        } else
        {
            hasAdrenaline = false;
        }
    }

    public void enemyKill()
    {
        styleAmount += 50f;
        multikillCount++;
        multikillTimer = 3f;
        if (multikillCount == 2)
            styleAmount += 15f;
        else if (multikillCount == 3)
            styleAmount += 30f;
        else if (multikillCount >= 4)
            styleAmount += 50f;
    }

    public void bulletBlock()
    {
        styleAmount += 20f;
    }

    public void enemyStun()
    {
        styleAmount += 20f;
    }

    public int extraHitCount;

    public float xCoord;
    public float yCoord;
    public bool hasDied;

    public void takeDamage()
    {
        if (hasAdrenaline == true)
        {
            hasAdrenaline = false;
            adrenalinePoints = 0;
            styleAmount -= 200f;
            extraHitCount++;
            Debug.Log("You've been hit, don't get hit again!");
        } else if (hasAdrenaline == false)
        {
            xCoord = GameObject.FindGameObjectWithTag("Player").transform.position.x;
            yCoord = GameObject.FindGameObjectWithTag("Player").transform.position.y;
            hasDied = true;
            Debug.Log("You have died, please try complete the level again!");
            extraHitCount = 0;

            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().LevelComplete();
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().PrintLevelCompletionStatistics();
            if (GameObject.FindWithTag("GameManager").GetComponent<GameManager>().isPlaying == true)
            {
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().WriteCSV();
            }

            SceneReload();
        }
    }

    void SceneReload()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
