using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenTouched : MonoBehaviour
{
    [SerializeField] private WhipPullClick whip;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            whip.whipInactive();
            Destroy(gameObject);
        }
        
    }
}
