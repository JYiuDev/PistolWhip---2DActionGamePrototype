using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private float speed = 8f;      //arbitrary default value
    private float lifeTime = 5;   //arbitrary default value
    [SerializeField] private float enemyDmg = 2;
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
                EnemyHP enemy = other.GetComponent<EnemyHP>();
                enemy.takeDamage(enemyDmg);
                Destroy(gameObject);

            break;

            case "Player":
                styleScriptTwo player = other.GetComponent<styleScriptTwo>();
                player.takeDamage();
                //Debug.Log("Player hit");
            break;

            case "Block":
                Destroy(gameObject);
            break;

            case "Shield":
                other.GetComponent<ShieldItems>().takeDamage(1);
                Destroy(gameObject);
            break;
        }
    }

    public Vector2 GetDirection()
    {
        return transform.right;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
