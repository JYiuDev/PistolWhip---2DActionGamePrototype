using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    private styleScriptTwo style;
    private Animator animator;

    [SerializeField] private float hp;
    [SerializeField] private GameObject dropPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        style = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>();
    }

    public void takeDamage(float dmg)
    {
        if (dmg > 0 && hp > 0)
        {
            hp -= dmg;
            if (hp <= 0)
            {
                // Enemy is dead
                Die();
            }
            else
            {
                // Enemy is hit but not dead
                animator.Play("Base Layer.EnemyHurt", 0, 0);
            }
        }
        Debug.Log("enemy took " + dmg + " damage");
        animator.Play("Base Layer.EnemyHurt", 0, 0);
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject drop =  Instantiate(dropPrefab, transform.position, transform.rotation);
        drop.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
        style.enemyKill();
    }

}
