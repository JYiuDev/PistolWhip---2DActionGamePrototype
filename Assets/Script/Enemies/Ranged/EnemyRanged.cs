using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    //Status
    [SerializeField] private float hp;
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

    //Weapon
    [SerializeField] private EnemyWeapon weapon;

    //UI
    private CircleRenderer circleRenderer;

    void Awake()
    {
        weapon = GetComponentInChildren<EnemyWeapon>();
        detection = GetComponentInChildren<Detection>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        circleRenderer = GetComponentInChildren<CircleRenderer>();
    }
    void Start()
    {
        state =  State.patrol;
        circleRenderer.CreatePoints();
        detection.SetRadius(visualRange);
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
            break;
            
            case State.alert:

            break;

            case State.aim:
                //Countdown aim time
                timer -= Time.deltaTime;

                if(visibleTargets.Count == 0)
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
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("hit");
            //TO IMPLEMENT
            //reduce hp
            //play hurt animation
            //if hp <=0, die
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
}