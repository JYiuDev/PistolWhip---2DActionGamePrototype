using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipAttack : MonoBehaviour
{
    private Collider2D hitbox;
    [SerializeField] private LayerMask targetLayer;
    private WhipPivot pivot; 
    public bool attack = false;


    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        pivot = GetComponentInParent<WhipPivot>();
    }

    void Update()
    {
        if(attack)
        {
            pivot.stopRotate();
        } else if (!attack)
        {
            pivot.startRotate();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Enemy":
                //Gona need further fixing to acoomodate other types of enemies
                //Need to make a parent class for all enemies
                EnemyRanged enemy = other.GetComponent<EnemyRanged>();
                enemy.takeDamage(2);
            break;
        }
    }

    public void lockePivot()
    {
        pivot.stopRotate();
    }
    public void unlockPivot()
    {
        pivot.startRotate();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Enemy":
                //Gona need further fixing to acoomodate other types of enemies
                //Need to make a parent class for all enemies
                EnemyRanged enemy = other.GetComponent<EnemyRanged>();
                enemy.takeDamage(1);

            break;

            case "Player":
                Debug.Log("Player hit");
            break;
        }
    }
}
