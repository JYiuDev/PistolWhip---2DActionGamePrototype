using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public abstract class WeaponClass : MonoBehaviour
{
    //Parent Class for pickup weapons
    public enum WeaponType {NONE = 0, REVOLVER = 1, BOTTLE = 2, SHIELD = 3,};
    protected WeaponType type;
    protected Vector2 weaponPos;
    protected float launchSpeed = 8;
    protected ThrownObj throwItem;
    protected Rigidbody2D rb;
    protected styleScriptTwo style;

    public WeaponClass()
    {
        type = WeaponType.NONE;
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
        throwItem = GetComponent<ThrownObj>();
        style = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>();
    }

    protected virtual void Throw()
    {
        throwItem.Launch(launchSpeed);
    }

    public virtual void LeftClick(){}
    public virtual void RightClick(){}
    public virtual void ThrowInteractions(Collider2D collision){}
}
