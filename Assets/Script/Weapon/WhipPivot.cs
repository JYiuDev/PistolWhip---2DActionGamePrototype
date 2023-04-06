using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipPivot : MonoBehaviour
{
    private Vector2 AimPosition;
    private Camera cam;
    private Vector3 mousePos;
    private Animator animator;
    private bool rotate = true;
    private WhipAttack whipAttack;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //animator = GetComponentInChildren<Animator>();
        //whipAttack = GetComponentInChildren<WhipAttack>();
    }
    void Start()
    {
        
    }

    void Update()
    {

        // if(Input.GetButtonDown(InputAxes.WhipAttack))
        // {
        //     if(!whipAttack.attack)
        //     {
        //         animator.Play("Base Layer.whipAttack", 0, 0);
        //     } else 
        //     {
        //         Debug.Log("Still in Animation!");
        //     }   
        // }

        if(rotate)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //translate screen mouse position to world
            Vector3 direction = (mousePos - transform.position).normalized;       //Vector from weapon to mouse

            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  //Calculate rotation angle from vector

            transform.rotation = Quaternion.Euler(0, 0, rotation);
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

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     switch(other.tag)
    //     {
    //         case "Enemy":
    //             //Gona need further fixing to acoomodate other types of enemies
    //             //Need to make a parent class for all enemies
    //             EnemyRanged enemy = other.GetComponent<EnemyRanged>();
    //             enemy.takeDamage(1);

    //         break;

    //         case "Player":
    //             Debug.Log("Player hit");
    //         break;
    //     }
    // }
}
