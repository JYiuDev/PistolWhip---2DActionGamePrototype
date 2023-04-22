using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb; 
    private Animator animator;

    [SerializeField]private Vector2 moveDir;
    private Transform weaponPos;
    private enum State{};
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weaponPos = transform.Find("WeaponPivot").Find("WeaponPos");
    }

    void Update()
    {
        //Read input and translate to move direciton
        moveDir.x = Input.GetAxisRaw(InputAxes.Horizontal);
        moveDir.y = Input.GetAxisRaw(InputAxes.Vertical);
        moveDir = moveDir.normalized;
        animator.SetFloat("Speed", moveDir.SqrMagnitude());
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
}
