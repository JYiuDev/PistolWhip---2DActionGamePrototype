using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    private styleScriptTwo style;
    private Animator animator;

    [SerializeField] private float hp;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    private float maxhp;
    

    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        style = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>();
        maxhp = hp;
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
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().CountDeaths();
            }
            else
            {
                // Enemy is hit but not dead
                animator.Play("Base Layer.EnemyHurt", 0, 0);
            }
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>().doDamage(dmg);

        //Debug.Log("enemy took " + dmg + " damage");
        animator.Play("Base Layer.EnemyHurt", 0, 0);
        Color c = Color.Lerp(endColor, startColor, (hp)/maxhp);
        sprite.color = c;

        if(GetComponent<EnemyMelee>())
        {
            GetComponent<EnemyMelee>().EnhancedDetectionCheck();
        } else if (GetComponent<EnemyRanged>())
        {
            GetComponent<EnemyRanged>().EnhancedDetectionCheck();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject drop =  Instantiate(dropPrefab, transform.position, transform.rotation);
        drop.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
        drop.GetComponent<ThrownObj>().StartCoroutine("SlowDown");
        style.enemyKill();
    }

}
