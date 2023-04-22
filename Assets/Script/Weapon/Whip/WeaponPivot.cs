using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPivot : MonoBehaviour
{
    private Vector2 AimPosition;
    private Camera cam;
    private Vector3 mousePos;
    private Animator animator;
    private bool rotate = true;
    private WhipAttack whipAttack;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(rotate)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //translate screen mouse position to world
            Vector3 direction = (mousePos - transform.position).normalized;       //Vector from weapon to mouse

            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  //Calculate rotation angle from vector

            transform.rotation = Quaternion.Euler(0, 0, rotation);

            // Flip the sprite if pointing left
            if (Mathf.Abs(rotation) > 90)
            {
                spriteRenderer.flipY = true;
                spriteRenderer.flipX = false;
            }
            // Flip the sprite back if pointing right
            else
            {
                spriteRenderer.flipY = false;
                spriteRenderer.flipX = false;
            }
        }
    }

    //Stopping rotation when function is called to accomodate for whip attack
    public void stopRotate()
    {
        rotate = false;
    }

    public void startRotate()
    {
        rotate = true;
    }
}
