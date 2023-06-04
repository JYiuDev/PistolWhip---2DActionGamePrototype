using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponClass
{
    [SerializeField] SpriteRenderer knifeSprite;
    [SerializeField] KnifeHitBox hitbox;
    [SerializeField] private float thrownDamage = 1;
    [SerializeField] private float throwDurabilityDmg;

    //damage value player inflicts when weilding the knife
    [SerializeField] public float meleeDamagePlayer = 1;
    //damage value enemy inflicts when weilding the knife
    //[SerializeField] public float meleeDamageEnemy = 1;
    [SerializeField] public Animator animator;
    [SerializeField] private float durability;
    private float maxDurability;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [SerializeField] float throwSpeed;
    private AudioSource knifeSFX;

    public Knife()
    {
        type = WeaponType.KNIFE;
    }
    
    private void Start(){
        maxDurability = durability;
        setThrowSpeed();
        knifeSFX = GetComponent<AudioSource>();
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
        knifeSFX.Play();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        knifeSFX.Play();
    }
    public override void ThrowInteractions(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHP enemy = collision.GetComponent<EnemyHP>();
            enemy.takeDamage(thrownDamage);
            useDurability(throwDurabilityDmg);
        }
    }

    public override void EnemyAttack()
    {
        animator.SetTrigger("Attack");
        knifeSFX.Play();
    }

    public void useDurability(float n)
    {
        durability -= n;

        if(durability <= 0)
        {
            Destroy(gameObject);
            return;
        }

        Color c = Color.Lerp(endColor, startColor, (durability-1)/maxDurability);
        knifeSprite.color = c;
    }

    public void calcDurability()
    {
        hitbox.calcDurability();
    }

    private void setThrowSpeed()
    {
        if(throwSpeed >= 0)
        {
            launchSpeed = throwSpeed;
        }
    }
}
