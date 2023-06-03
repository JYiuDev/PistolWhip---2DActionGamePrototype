using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    //Status
    [SerializeField] private float visualRange;
    [SerializeField]private Transform player;
    
    //Detection
    private Detection detection;
    private Transform targetPos;

    //States
    public enum State{patrol, alert, aim, stunned};
    [SerializeField] private State state;

    //Timings
    [SerializeField] private float timer;
    [SerializeField] float aimTime = 0.8f;     //Time to shoot as line of sight is maintained
    [SerializeField] float searchTime;  //Time to chase after losing sight of player
    [SerializeField] float chaseSpeed;
    [SerializeField] float attackDistance = 1;
    private Vector2 patrolCenter; // Center of the patrol area
    //private float stunDuration = 5f;
    private float stunTimer = 0f;
    [SerializeField] private GameObject stunnedObject;

    //Weapon
    [SerializeField]private WeaponClass weaponHeld = null;

    //UI
    [SerializeField] bool visionRender = false;
    private CircleRendererMelee circleRenderer;

    //Animator
    private Animator animator;
    //Pathfinding
    private EnemyPath path;
    private Rigidbody2D rb;
    private styleScriptTwo style;
    private State previousState;

    [SerializeField] private Transform weaponPos;

    void Awake()
    {
        weaponHeld = GetComponentInChildren<WeaponClass>();
        detection = GetComponent<Detection>();
        circleRenderer = GetComponentInChildren<CircleRendererMelee>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        path = GetComponent<EnemyPath>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        style = GameObject.FindGameObjectWithTag("Player").GetComponent<styleScriptTwo>();
        state = State.patrol;
        patrolCenter = transform.position;
        stunnedObject.SetActive(false);
        patrolDirection = Random.insideUnitCircle.normalized; // Initialize patrol direction
        
        if(visionRender)
        {
            circleRenderer.setToggle(true);
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
                    path.target = player;
                    timer = searchTime;
                    state = State.alert;
                    return;
                }

            break;

            case State.alert:
                
                timer -= Time.deltaTime;

                if (distanceToPlayer <= attackDistance)
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

                if(timer <= 0)
                {
                    weaponHeld.EnemyAttack();
                    timer = aimTime + 0.3f;

                    if(distanceToPlayer > attackDistance * 1.1f)
                    {
                        path.target = player;
                        timer = searchTime;
                        state = State.alert;
                    }
                }

                
            break;

            case State.stunned:

                // Countdown stun timer
                stunTimer -= Time.deltaTime;
                rb.velocity = Vector2.zero;
                // If stun timer is up, resume patrol or aim at player
                if (stunTimer <= 0f)
                {
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

    public Vector2 getTargetPos()
    {
        return player.position;
    }

    public float getVisualRange()
    {
        return visualRange;
    }

    private SpriteRenderer spriteRenderer;
    private Vector2 patrolDirection;
    
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

    private void updateWeapon()
    {
        if(weaponPos.childCount > 0)
        {
            weaponHeld = weaponPos.GetChild(0).GetComponent<WeaponClass>();
        } else
        {
            weaponHeld = null;
        }
    }

    public void EnhancedDetectionCheck()
    {
        
    }

}