using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb; 
    private Animator animator;

    [SerializeField]private Vector2 moveDir;
    private enum State{};
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
    }

    void FixedUpdate()
    {
        //Update Player Position
        rb.MovePosition((moveDir * moveSpeed * Time.fixedDeltaTime) + rb.position);
    }
}
