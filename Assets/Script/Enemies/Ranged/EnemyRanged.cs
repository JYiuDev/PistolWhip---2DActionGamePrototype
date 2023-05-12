using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    //Status
    [SerializeField] private float hp = 2f;
    [SerializeField] private float visualRange;
    private Transform player;
    
    //Detection
    private Detection detection;
    private Transform targetPos;

    //States
    private enum State{patrol, alert, aim, stunned};
    [SerializeField] private State state;

    //Timings
    [SerializeField] private float timer;
    [SerializeField] float aimTime;     //Time to shoot as line of sight is maintained
    [SerializeField] float searchTime;  //Time to chase after losing sight of player
    [SerializeField] float patrolRadius; // Raidus to patrol within
    [SerializeField] float patrolSpeed; // Speed at which the enemy patrols
    private Vector2 patrolCenter; // Center of the patrol area
    private float stunDuration = 5f;
    private float stunTimer = 0f;
    [SerializeField] private GameObject stunnedObject;

    //Weapon
    [SerializeField] private EnemyWeapon weapon;

    //UI
    private CircleRenderer circleRenderer;

    //Animator
    private Animator animator;
    //Prefabs
    [SerializeField] private GameObject gunPrefab;
    private Style style;

    void Awake()
    {
        weapon = GetComponentInChildren<EnemyWeapon>();
        detection = GetComponentInChildren<Detection>();
        circleRenderer = GetComponentInChildren<CircleRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        style = GameObject.FindGameObjectWithTag("GameController").GetComponent<Style>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        state = State.patrol;
        circleRenderer.CreatePoints();
        patrolCenter = transform.position;
        stunnedObject.SetActive(false);
        patrolDirection = Random.insideUnitCircle.normalized; // Initialize patrol direction
        movingToEdge = false; // Initialize moving to edge

    }

    void Update()
    {
        //Get visible targets
        List<Transform> visibleTargets = detection.GetVisibleTargets();
        detection.SetRadius(visualRange);

        switch(state)
        {
            case State.patrol:
                if(detection.isPlayerFound())
                {
                    //If target found, aim (temporary)
                    circleRenderer.SetColor(Color.red);
                    timer = aimTime;
                    state = State.aim;
                }
                else
                {
                    PatrolState(); //Patrol randomly if player is not found.
                }
            break;

            case State.alert:
                
                break;

            case State.aim:
                //Countdown aim time
                timer -= Time.deltaTime;

                if(!detection.isPlayerFound())
                {
                    circleRenderer.SetColor(Color.white);
                    state = State.patrol;
                }

                if(timer <= 0)
                {
                    circleRenderer.SetColor(Color.white);
                    weapon.ShootBullet();
                    state = State.patrol;
                }

            break;

            case State.stunned:

                // Countdown stun timer
                stunTimer -= Time.deltaTime;

                // If stun timer is up, resume patrol or aim at player
                if (stunTimer <= 0f)
                {
                    if (!detection.isPlayerFound())
                    {
                        movingToEdge = true;
                        patrolDirection = Random.insideUnitCircle.normalized;
                        state = State.patrol;
                    }
                    else
                    {
                        state = State.aim;
                        timer = aimTime;
                        circleRenderer.SetColor(Color.red);
                    }
                    stunnedObject.SetActive(false); // Hide the stunned object
                }

                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bottle"))
        {
            // Set stun timer and change state to stunned
            takeDamage(1);
            stunTimer = stunDuration;
            state = State.stunned;
            style.enemyStun();

            // Show stun effect
            stunnedObject.SetActive(true);

            // Stop moving
            patrolDirection = Vector2.zero;
            movingToEdge = false;
        }
        if (other.CompareTag("Enemy"))
        {
            // Move in opposite directions when colliding with another enemy
            //Vector2 collisionNormal = (other.transform.position - transform.position).normalized;
            //Vector2 perpendicular = new Vector2(-collisionNormal.y, collisionNormal.x);
            //patrolDirection = perpendicular * Mathf.Sign(Vector2.Dot(patrolDirection, perpendicular));
            //movingToEdge = false;
        }

        if (other.CompareTag("Bullet"))
        {
            this.takeDamage(1);
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
    
    private void Die()
    {
        Destroy(gameObject);
        GameObject gun =  Instantiate(gunPrefab, transform.position, transform.rotation);
        gun.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
        style.enemyKill();
    }
}