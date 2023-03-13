using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    //Status
    [SerializeField] float hp;
    [SerializeField] float visualRange;
    
    //Detection
    private Detection detection;

    //States
    private enum State{patrol, alert, aim, stunned};
    private State state;

    //Timings
    private float timer;
    [SerializeField] float aimTime;     //Time to shoot as line of sight is maintained
    [SerializeField] float searchTime;  //Time to chase after losing sight of player

    void Awake()
    {
        detection = GetComponentInChildren<Detection>();
    }
    void Start()
    {
        state =  State.patrol;
        detection.SetRange(visualRange);
    }

    void Update()
    {
        switch(state)
        {
            case State.patrol:
            
            break;
            
            case State.alert:

            break;

            case State.aim:

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
}
