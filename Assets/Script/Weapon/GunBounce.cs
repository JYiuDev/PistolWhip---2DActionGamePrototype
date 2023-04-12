using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBounce : MonoBehaviour
{
    [SerializeField] private float bounceAngle = 45f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            // Bounce the bullet off the gun in a random direction
            Vector2 bulletDirection = collision.GetComponent<BulletMove>().GetDirection();
            Vector2 bounceDirection = Quaternion.Euler(0f, 0f, Random.Range(-bounceAngle, bounceAngle)) * bulletDirection;
            collision.GetComponent<Rigidbody2D>().velocity = bounceDirection * collision.GetComponent<BulletMove>().GetSpeed();
        }
    }
}
