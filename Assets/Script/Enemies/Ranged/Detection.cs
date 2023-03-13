using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private CircleCollider2D detectionCircle;
    private bool playerFound = false;

    void Start()
    {
        detectionCircle = gameObject.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    public void SetRange(float rad)
    {
        detectionCircle.radius = rad;
    }

    public bool PlayerDetected()
    {
        return playerFound;
    }

    private void FindVisibleTargets()
    {
        
    }

    //OLD CODE FOR REFERENCE BELOW
    //  private void FindVisibleTarget()
    // {
    //     visibleTargets.Clear();
    //     //Find all targetMask objects in specified range
    //     targetsInRange = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask); 

    //     //RayCast to check if any Obstacles are hit, if not, add target to visible list!
    //     for(int i = 0; i < targetsInRange.Length; i++)
    //     {
    //         Transform target = targetsInRange[i].transform;
    //         Vector2 targetDir = (target.position - transform.position).normalized;

    //         if(Vector2.Angle(targetDir, transform.up) < viewAngle /2)
    //         {
    //             float targetDist = Vector2.Distance(transform.position, target.position);

    //             if(!Physics2D.Raycast(transform.position, targetDir, targetDist, obstacleMask))
    //             {
    //                 visibleTargets.Add(target);
    //             }
    //         }
    //     }
    // }

}
