using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    //Status
    [SerializeField] float hp;
    [SerializeField] float visualRange;
    private Transform player;
    
    //Detection
    private Detection detection;
    private Transform targetPos;

    //States
    private enum State{patrol, alert, aim, stunned};
    [SerializeField] private State state;

    //Timings
    [SerializeField]private float timer;
    [SerializeField] float aimTime;     //Time to shoot as line of sight is maintained
    [SerializeField] float searchTime;  //Time to chase after losing sight of player

    //Weapon
    private EnemyWeapon weapon;
    

    void Awake()
    {
        weapon = GetComponentInChildren<EnemyWeapon>();
        detection = GetComponentInChildren<Detection>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        state =  State.patrol;
    }

    void Update()
    {
        //Get visible targets
        List<Transform> visibleTargets = detection.GetVisibleTargets();

        switch(state)
        {
            case State.patrol:
                if(detection.isPlayerFound())
                {
                    //If target found, aim (temporary)
                    timer = aimTime;
                    state = State.aim;
                }
            break;
            
            case State.alert:

            break;

            case State.aim:
                //Countdown aim time
                timer -= Time.deltaTime;

                if(timer <= 0)
                {
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
}
