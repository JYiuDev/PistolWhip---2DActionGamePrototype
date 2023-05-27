using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBounce : MonoBehaviour
{
    [SerializeField] private float bounceAngle = 120f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if ((transform.parent == null && collision.CompareTag("Bullet"))&& rb.velocity != Vector2.zero)
        {
            // Bounce the bullet off the gun in a random direction
            Vector2 bulletDirection = collision.GetComponent<BulletMove>().GetDirection();
            Vector2 bounceDirection = Quaternion.Euler(0f, 0f, Random.Range(-bounceAngle, bounceAngle)) * bulletDirection;
            collision.GetComponent<Rigidbody2D>().velocity = bounceDirection * collision.GetComponent<BulletMove>().GetSpeed();
            
            rb.velocity = (-collision.GetComponent<Rigidbody2D>().velocity) * 0.3f;
        }
    }
}
