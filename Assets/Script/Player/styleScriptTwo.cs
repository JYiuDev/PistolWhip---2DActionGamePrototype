using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        else if (styleAmount < 100)
        {
            rankTitle = "Deputy";
            styleDecay = 5f;
            adrenalineGain = 2;
        }
        else if (styleAmount >= 100 && styleAmount < 250)
        {
            rankTitle = "Sherrif";
            styleDecay = 7.5f;
            adrenalineGain = 3;
        }
        else if (styleAmount >= 250 && styleAmount < 450)
        {
            rankTitle = "Vigilante";
            styleDecay = 12.5f;
            adrenalineGain = 5;
        }
        else if (styleAmount >= 450 && styleAmount < 700)
        {
            rankTitle = "Hero";
            styleDecay = 20f;
            adrenalineGain = 8;
        }
        else if (styleAmount >= 700 && styleAmount < 1000)
        {
            rankTitle = "Legend";
            styleDecay = 32.5f;
            adrenalineGain = 13;
        }
        else if (styleAmount >= 1000)
        {
            rankTitle = "Myth";
            styleDecay = 50f;
            adrenalineGain = 21;
        }

        //adrenaline stuff
        if (adrenalinePoints >= 100)
        {
            adrenalinePoints = 100;
            hasAdrenaline = true;
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

    public void takeDamage()
    {
        hasAdrenaline = false;
        adrenalinePoints = 0;
        styleAmount -= 200f;
        Debug.Log("You've been hit!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the Bullet object
        if (other.CompareTag("Bullet"))
        {
            //takeDamage();
        }
    }
}
