using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponClass
{
    [SerializeField] private float thrownDamage = 1;

    //damage value player inflicts when weilding the knife
    [SerializeField] public float meleeDamagePlayer = 1;
    //damage value enemy inflicts when weilding the knife
    //[SerializeField] public float meleeDamageEnemy = 1;
    [SerializeField] public Animator animator;
    [SerializeField] private int durability;

    public Knife()
    {
        type = WeaponType.KNIFE;
    }
    
    private void Start(){

    }

    private void Udpate()
    {

    }
    public override void LeftClick()
    {
        Attack();
    }

    public override void RightClick()
    {
        animator.Play("Base Layer.KnifeIdle", 0, 0);
        Throw();
    }

    public override void Throw()
    {
        throwItem.Launch(launchSpeed);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public override void ThrowInteractions(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHP enemy = collision.GetComponent<EnemyHP>();
            enemy.takeDamage(thrownDamage);
        }
    }

    public override void EnemyAttack()
    {
        Attack();
    }

    public void useDurability(float n)
    {
        durability --;
    }
}
