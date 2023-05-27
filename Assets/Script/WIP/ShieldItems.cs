using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItems : WeaponClass
{
    //hits take to destroy this object
    [SerializeField] private float durability = 3;
    [SerializeField] private float inflictStunDuration = 2;

    public ShieldItems()
    {
        type = WeaponType.SHIELD;
    }
    public override void LeftClick()
    {
        Throw();
    }

    public override void RightClick()
    {
        Throw();
    }

    public override void ThrowInteractions(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(durability > 0)
            {
                //update this code when parent class is implemented, hard coding for now since im out of time sorry future me
                if(collision.gameObject.GetComponent<EnemyRanged>())
                {
                    EnemyRanged enemy = collision.gameObject.GetComponent<EnemyRanged>();
                    style.enemyStun();
                    rb.velocity = (-rb.velocity).normalized * 1;
                    enemy.Stun(inflictStunDuration);
                }

                if(collision.gameObject.GetComponent<EnemyMelee>())
                {
                    EnemyMelee enemy = collision.gameObject.GetComponent<EnemyMelee>();
                    style.enemyStun();
                    rb.velocity = (-rb.velocity).normalized * 1;
                    enemy.Stun(inflictStunDuration);
                }
                
            }
            durability --;
            if(durability <= 0){
                Destroy(gameObject);
            }
        }
    }

    public void takeDamage(float dmg)
    {
        style.bulletBlock();
        durability -= dmg;
        
        if(durability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
