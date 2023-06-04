using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    //Status
    [SerializeField] private float visualRange;
    [SerializeField] private LayerMask obstacleLayer;
    private Transform player;
    
    //Detection
    private Detection detection;
    private Transform targetPos;

    //States
    public enum State{patrol, alert, aim, stunned};
    [SerializeField] private State state;

    //Timings
    [SerializeField] private float timer;
    [SerializeField] float aimTime;     //Time to shoot as line of sight is maintained
    [SerializeField] float searchTime;  //Time to chase after losing sight of player
    [SerializeField] float patrolRadius; // Raidus to patrol within
    [SerializeField] float patrolSpeed; // Speed at which the enemy patrols
    [SerializeField] float chaseSpeed;
    private Vector2 patrolCenter; // Center of the patrol area
    //private float stunDuration = 5f;
    private float stunTimer = 0f;
    [SerializeField] private GameObject stunnedObject;

    //Weapon
    [SerializeField] private EnemyWeapon weapon;

    //UI
    [SerializeField] bool visionRender = false;
    private CircleRenderer circleRenderer;

    //Animator
    private Animator animator;
    //Prefabs
    [SerializeField] private GameObject gunPrefab;
    //Pathfinding
    private EnemyPath path;
    private Rigidbody2D rb;

    private styleScriptTwo style;
    private State previousState;
    [SerializeField] private bool chaseOnSpawn= false;
    private AudioSource gunSound;

    void Awake()
    {
        weapon = GetComponentInChildren<EnemyWeapon>();
        detection = GetComponentInChildren<Detection>();
        circleRenderer = GetComponentInChildren<CircleRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        path = GetComponent<EnemyPath>();
        rb = GetComponent<Rigidbody2D>();
        style = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>();
        gunSound = GetComponent<AudioSource>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        state = State.patrol;
        patrolCenter = transform.position;
        stunnedObject.SetActive(false);
        patrolDirection = Random.insideUnitCircle.normalized; // Initialize patrol direction
        movingToEdge = false; // Initialize moving to edge
        
        if(visionRender)
        {
            circleRenderer.setToggle(true);
        }
        if(chaseOnSpawn)
        {
            path.target = player;
            timer = searchTime;
            state = State.alert;
            return;
        }
    }

    void Update()
    {
        //Get visible targets
        List<Transform> visibleTargets = detection.GetVisibleTargets();
        detection.SetRadius(visualRange);
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        switch(state)
        {
            case State.patrol:
                if(detection.isPlayerFound())
                {
                    timer = aimTime;
                    state = State.aim;
                    return;
                }

            break;

            case State.alert:
                
                timer -= Time.deltaTime;

                if (detection.isPlayerFound())
                {
                    timer = aimTime;
                    rb.velocity = Vector2.zero;
                    path.target = null;
                    state = State.aim;
                    return;
                }

                if(timer <= 0)
                {
                    rb.velocity = Vector2.zero;
                    path.target = null;
                    state = State.patrol;
                    return;
                }
                
                rb.velocity = path.naviDir * chaseSpeed;

                break;

            case State.aim:
                //Countdown aim time
                timer -= Time.deltaTime;

                if(!detection.isPlayerFound())
                {
                    path.target = player;
                    timer = searchTime;
                    state = State.alert;
                }

                if(timer <= 0)
                {
                    weapon.ShootBullet();
                    gunSound.Play();
                    timer = aimTime;
                }

            break;

            case State.stunned:

                // Countdown stun timer
                stunTimer -= Time.deltaTime;
                // If stun timer is up, resume patrol or aim at player
                if (stunTimer <= 0f)
                {
                    // if (!detection.isPlayerFound())
                    // {
                    //     movingToEdge = true;
                    //     patrolDirection = Random.insideUnitCircle.normalized;
                    //     state = State.patrol;
                    // }
                    // else
                    // {
                    //     timer = aimTime;
                    //     state = State.aim;
                    // }
                    state = previousState;
                    stunnedObject.SetActive(false); // Hide the stunned object
                }

                break;
        }

        if(visionRender)
        {
            circleRenderer.setToggle(true);
        } else
        {
            circleRenderer.setToggle(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Bottle"))
        // {
        //     // Set stun timer and change state to stunned
        //     takeDamage(1);
        //     stunTimer = stunDuration;
        //     state = State.stunned;

        //     // Show stun effect
        //     stunnedObject.SetActive(true);

        //     // Stop moving
        //     patrolDirection = Vector2.zero;
        //     movingToEdge = false;
        // }
        if (other.CompareTag("Enemy"))
        {
            // Move in opposite directions when colliding with another enemy
            //Vector2 collisionNormal = (other.transform.position - transform.position).normalized;
            //Vector2 perpendicular = new Vector2(-collisionNormal.y, collisionNormal.x);
            //patrolDirection = perpendicular * Mathf.Sign(Vector2.Dot(patrolDirection, perpendicular));
            //movingToEdge = false;
        }
    }

    public Vector2 getTargetPos()
    {
        return player.position;
    }

    public float getVisualRange()
    {
        return visualRange;
    }

    // public void takeDamage(float dmg)
    // {
    //     if (dmg > 0 && hp > 0)
    //     {
    //         hp -= dmg;
    //         if (hp <= 0)
    //         {
    //             // Enemy is dead
    //             Die();
    //         }
    //         else
    //         {
    //             // Enemy is hit but not dead
    //             animator.Play("Base Layer.EnemyHurt", 0, 0);
    //         }
    //     }
    //     Debug.Log("enemy took " + dmg + " damage");
    //     animator.Play("Base Layer.EnemyHurt", 0, 0);
    // }

    private SpriteRenderer spriteRenderer;
    private Vector2 patrolDirection;
    private bool movingToEdge;
    private float breakTimer;

    private void PatrolState()
    {
        if (!movingToEdge)
        {
            // If not moving to edge, move in current patrol direction
            transform.position += (Vector3)patrolDirection.normalized * patrolSpeed * Time.deltaTime;

            // Check if enemy has reached edge of patrol radius
            float distanceFromCenter = Vector2.Distance(transform.position, patrolCenter);
            if (distanceFromCenter >= patrolRadius)
            {
                // If reached edge, move to edge and pick new patrol direction
                transform.position = patrolCenter + patrolDirection.normalized * patrolRadius;
                patrolDirection = Random.insideUnitCircle.normalized;
                movingToEdge = true;
                breakTimer = Random.Range(1f, 3f); // Set break time to a random value between 1 and 3 seconds
            }
            else
            {
                // Flip sprite to face left or right depending on patrol direction
                spriteRenderer.flipX = (patrolDirection.x < 0);
            }
        } 
        else
        {
            // If moving to edge, wait for break time
            breakTimer -= Time.deltaTime;
            if (breakTimer <= 0f)
            {
                // If break time is over, move towards edge of patrol radius
                transform.position = Vector2.MoveTowards(transform.position, patrolCenter + patrolDirection.normalized * patrolRadius, patrolSpeed * Time.deltaTime);

                // Check if reached edge of patrol radius
                float distanceFromCenter = Vector2.Distance(transform.position, patrolCenter);
                if (distanceFromCenter >= patrolRadius)
                {
                    // If reached edge, stop moving and start moving in new patrol direction
                    transform.position = patrolCenter + patrolDirection.normalized * patrolRadius;
                    movingToEdge = false;
                }
                else
                {
                    // Flip sprite to face left or right depending on patrol direction
                    spriteRenderer.flipX = (patrolDirection.x < 0);
                }
            }
        }
    }
    
    // private void Die()
    // {
    //     Destroy(gameObject);
    //     GameObject gun =  Instantiate(gunPrefab, transform.position, transform.rotation);
    //     gun.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
    //     style.enemyKill();
    // }
    
    public void Stun(float duration)
    {
        previousState = state;
        state = State.stunned;
        stunTimer = duration;

        // Show stun effect
        stunnedObject.SetActive(true);

        rb.velocity = Vector2.zero;
    }

    public State GetState()
    {
        return state;
    }
    public void EnhancedDetectionCheck()
    {
        Vector2 targetDir = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, visualRange * 2.5f, obstacleLayer);
        if(hit)
        {
            if(hit.transform.CompareTag("Player"))
            {
                path.target = player;
                timer = searchTime;
                state = State.alert;
                return;
            }
        }
    }

}