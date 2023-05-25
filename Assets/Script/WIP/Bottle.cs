using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : WeaponClass
{
    [SerializeField] private int durability; 
    [SerializeField] private int breakAfter;
    [SerializeField] private Sprite[] brokenSprites;
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
            durability --;
            if(durability == breakAfter)
            {
                int randomNum = Random.Range(0,brokenSprites.Length);
                spriteRenderer.sprite = brokenSprites[randomNum];
            }

            if(durability > 0)
            {
                style.enemyStun();
                Debug.Log("collide with enemy");
                rb.velocity = (-rb.velocity).normalized * 1;
            }
            else{
                Destroy(gameObject);
            }
            
        }
    }
}
