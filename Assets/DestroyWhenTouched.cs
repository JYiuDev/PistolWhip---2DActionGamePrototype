using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenTouched : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Picked up");
        }
        
    }
}
