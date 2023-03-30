using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private Collider2D hitbox;
    [SerializeField] private LayerMask targetLayer;

    //Attack Values
    [SerializeField] float startUp;
    [SerializeField] float active;



    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider2D other)
    {
        switch(other.tag)
        {
            case "Enemy":

            break;
        }
    }
}
