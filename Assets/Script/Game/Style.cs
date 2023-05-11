using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Style : MonoBehaviour
{
    // Start is called before the first frame update
    public float styleAmount;
    public float styleDecay;
    public string rankTitle;
    void Start()
    {
        rankTitle = "";
        styleAmount = 0f;
        styleDecay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        styleAmount -= styleDecay * Time.deltaTime;

        if (styleAmount <= 0)
        {
            styleAmount = 0;
            rankTitle = "";
            styleDecay = 0f;
        }
        else if (styleAmount < 100)
        {
            rankTitle = "Deputy";
            styleDecay = 10f;
        }
        else if (styleAmount >= 100 && styleAmount < 250)
        {
            rankTitle = "Sherrif";
            styleDecay = 20f;
        }
        else if (styleAmount >= 250 && styleAmount < 450)
        {
            rankTitle = "Vigilante";
            styleDecay = 40f;
        }
        else if (styleAmount >= 450 && styleAmount < 700)
        {
            rankTitle = "Hero";
            styleDecay = 60f;
        }
        else if (styleAmount >= 700 && styleAmount < 1000)
        {
            rankTitle = "Legend";
            styleDecay = 80f;
        }
        else if (styleAmount >= 1000)
        {
            rankTitle = "Myth";
            styleDecay = 100f;
        }
    }
    public void enemyKill()
    {
        styleAmount += 50f;
    }
}