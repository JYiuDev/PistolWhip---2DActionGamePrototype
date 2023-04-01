using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private Collider2D hitbox;
    [SerializeField] private LayerMask targetLayer;


    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
    }

    void Update()
    {

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
}
