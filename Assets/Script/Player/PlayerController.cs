using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb; 
    private Animator animator;

    private Vector2 moveDir;

    [SerializeField]private WeaponClass weaponHeld = null;
    [SerializeField] private Transform weaponPos;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        if(weaponPos.childCount > 0)
        {
            weaponHeld = weaponPos.GetChild(0).GetComponent<WeaponClass>();
        } else
        {
            weaponHeld = null;
        }
    }

    void Update()
    {
        //Read input and translate to move direciton
        moveDir.x = Input.GetAxisRaw(InputAxes.Horizontal);
        moveDir.y = Input.GetAxisRaw(InputAxes.Vertical);
        moveDir = moveDir.normalized;
        animator.SetFloat("Speed", moveDir.SqrMagnitude());

        // Flip the sprite if moving left
        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Flip the sprite back if moving right
        else if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        updateWeapon();
        if (Input.GetKeyDown(KeyCode.Mouse0) && weaponHeld != null)
        {
            weaponHeld.LeftClick();
        }
        if(Input.GetKeyDown(KeyCode.Mouse1) && weaponHeld != null)
        {
            weaponHeld.RightClick();
        }
    }

    void FixedUpdate()
    {
        //Update Player Position
        rb.MovePosition((moveDir * moveSpeed * Time.fixedDeltaTime) + rb.position);
    }

    public Transform GetWeaponPos()
    {
        return(weaponPos);
    }

    public void SetWeapon(WeaponClass weapon)
    {
        weaponHeld = weapon;
    }

    public void RemoveWeapon()
    {
        weaponHeld = null;
    }

    private void updateWeapon()
    {
        if(weaponPos.childCount > 0)
        {
            weaponHeld = weaponPos.GetChild(0).GetComponent<WeaponClass>();
        } else
        {
            weaponHeld = null;
        }
    }
}
