using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class styleScriptTwo : MonoBehaviour
{

    [SerializeField] private float hp = 2f;

    // Public variables
    public float styleAmount;
    public float styleDecay;
    public string rankTitle;
    public float multikillTimer;
    public int multikillCount;

    private void Awake()
    {

    }

    private void Start()
    {
        InitializeStyle();
    }

    private void Update()
    {
        // Decay and multikill checks
        styleAmount -= styleDecay * Time.deltaTime;
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

    private void InitializeStyle()
    {
        rankTitle = "";
        styleAmount = 0f;
        styleDecay = 0f;
        multikillTimer = 0f;
        multikillCount = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the Bullet object
        if (other.CompareTag("Bullet"))
        {
            BulletCollision();
        }
    }

    private void BulletCollision()
    {
        Debug.Log("I've been hit!");
    }

    public void takeDamage(float dmg)
    {
        if (dmg > 0 && hp > 0)
        {
            hp -= dmg;
            Debug.Log("Player is hit");
            if (hp <= 0)
            {
                // Player is dead
                Debug.Log("Player would be dead");
            }
            else
            {
                // Player is hit but not dead
                
            }
        }
        Debug.Log("player took " + dmg + " damage");
    }
}
