using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private int maxHits = 3;
    [SerializeField] private GameObject hitEffect;

    private int hitsLeft;

    private void Start()
    {
        hitsLeft = maxHits;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            hitsLeft--;
            Destroy(collision.gameObject);

            if (hitsLeft <= 0)
            {
                Destroy(gameObject);

                if (hitEffect != null)
                {
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                }
            }
        }
    }
}






