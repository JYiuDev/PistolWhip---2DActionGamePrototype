using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleBreak : MonoBehaviour
{
    [SerializeField] private Sprite changeSprite; // Sprite to use when colliding with enemy

    private SpriteRenderer spriteRenderer; // Reference to the object's sprite renderer component

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get reference to the sprite renderer component
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            spriteRenderer.sprite = changeSprite; // Change the sprite to the enemy sprite
        }
    }
}
