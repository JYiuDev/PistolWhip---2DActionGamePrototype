using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : WeaponClass
{
    [SerializeField] private int durability; 
    [SerializeField] private int breakAfter;
    [SerializeField] private Sprite[] brokenSprites;
    [SerializeField] private float inflictStunDuration = 2;
    [SerializeField] private float brokenThrowDmg = 1;
    private SpriteRenderer spriteRenderer;

    public Bottle()
    {
        type = WeaponType.BOTTLE;
    }
    
    private void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                if(durability > breakAfter)
                {
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
                } else
                {
                    EnemyHP enemy = collision.GetComponent<EnemyHP>();
                    enemy.takeDamage(brokenThrowDmg);
                }
                //update this code when parent class is implemented, hard coding for now since im out of time sorry future me
                
                
            }
            durability --;
            if(durability == breakAfter)
            {
                int randomNum = Random.Range(0,brokenSprites.Length);
                spriteRenderer.sprite = brokenSprites[randomNum];
            }
            if(durability <= 0){
                Destroy(gameObject);
            }
            
        }
    }
}
