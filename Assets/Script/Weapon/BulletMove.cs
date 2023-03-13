using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private float speed = 8f;      //arbitrary default value
    private float lifeTime = 5;   //arbitrary default value
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetBulletSpeed(float s)
    {
        speed = s;
    }
}
