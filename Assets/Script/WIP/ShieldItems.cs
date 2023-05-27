using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItems : WeaponClass
{
    //hits take to destroy this object
    [SerializeField] private float durability = 3;

    public ShieldItems()
    {
        type = WeaponType.SHIELD;
    }

    void Update()
    {
        if(rb.velocity == Vector2.zero && transform.parent == null)
        {
            
        }
    }
    public override void LeftClick()
    {
        Throw();
    }

    public override void RightClick()
    {
        Throw();
    }

    public override void ThrowInteractions(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            rb.velocity = (-rb.velocity).normalized * 1;
        }
    }

    public void takeDamage(float dmg)
    {
        style.bulletBlock();
        Debug.Log("take " + dmg + " dmg");
        durability -= dmg;
        
        if(durability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
