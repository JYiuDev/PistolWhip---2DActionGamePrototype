using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItems : WeaponClass
{
    //hits take to destroy this object
    [SerializeField] private float durability = 3;

    private styleScriptTwo style;

    private void Awake()
    {
        style = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>();
    }
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
