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

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Enemy":
                //Gona need further fixing to acoomodate other types of enemies
                //Need to make a parent class for all enemies
                EnemyRanged enemy = other.GetComponent<EnemyRanged>();
                enemy.takeDamage(1);

            break;

            case "Player":
                Debug.Log("Player hit");
            break;
        }
    }
}
