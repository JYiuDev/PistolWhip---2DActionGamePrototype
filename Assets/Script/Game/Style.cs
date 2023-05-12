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
    void Start()
    {
        rankTitle = "";
        styleAmount = 0f;
        styleDecay = 0f;
        multikillTimer = 0f;
        multikillCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //decay and multikill checks
        styleAmount -= styleDecay * Time.deltaTime;
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
        }
        else if (styleAmount < 100)
        {
            rankTitle = "Deputy";
            styleDecay = 5f;
        }
        else if (styleAmount >= 100 && styleAmount < 250)
        {
            rankTitle = "Sherrif";
            styleDecay = 7.5f;
        }
        else if (styleAmount >= 250 && styleAmount < 450)
        {
            rankTitle = "Vigilante";
            styleDecay = 12.5f;
        }
        else if (styleAmount >= 450 && styleAmount < 700)
        {
            rankTitle = "Hero";
            styleDecay = 20f;
        }
        else if (styleAmount >= 700 && styleAmount < 1000)
        {
            rankTitle = "Legend";
            styleDecay = 32.5f;
        }
        else if (styleAmount >= 1000)
        {
            rankTitle = "Myth";
            styleDecay = 50f;
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
}