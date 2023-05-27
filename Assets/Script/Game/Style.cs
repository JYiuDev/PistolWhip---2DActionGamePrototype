using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Style : MonoBehaviour
{
    // Start is called before the first frame update
    public float styleAmount;
    public float styleDecay;
    public string rankTitle;
    public float multikillTimer;
    public int multikillCount;
    public float adrenalinePoints;
    public float adrenalineGain;
    public bool hasAdrenaline;
    void Start()
    {
        rankTitle = "";
        styleAmount = 0f;
        styleDecay = 0f;
        multikillTimer = 0f;
        multikillCount = 0;
        hasAdrenaline = true;
    }

    // Update is called once per frame
    void Update()
    {
        //decay and multikill checks
        styleAmount -= styleDecay * Time.deltaTime;
        adrenalinePoints += adrenalineGain;
        multikillTimer -= Time.deltaTime;
        if (multikillTimer <= 0)
        {
            multikillCount = 0;
            multikillTimer = 0;
        }

        //ranks and style count
        if (styleAmount <= 0)
        {
            styleAmount = 0;
            rankTitle = "";
            styleDecay = 0f;
            adrenalineGain = 1;
        }

        else if (styleAmount > 0 && styleAmount < 100)
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
        }
    }
    public void enemyKill()
    {
        styleAmount += 150f;
        multikillCount++;
        multikillTimer = 3f;
        if (multikillCount == 2)
            styleAmount += 50f;
        else if (multikillCount == 3)
            styleAmount += 100f;
        else if (multikillCount >= 4)
            styleAmount += 150f;
    }
    public void bulletBlock()
    {
        styleAmount += 50f;
    }
    public void enemyStun()
    {
        styleAmount += 50f;
    }
    public void takeDamage()
    {
        hasAdrenaline = false;
        adrenalinePoints = 0;
        styleAmount -= 300f;
       
    }
}