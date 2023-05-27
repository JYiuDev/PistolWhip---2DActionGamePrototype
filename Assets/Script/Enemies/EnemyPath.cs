using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPath : MonoBehaviour {
    public Transform target;

    private Seeker seeker;

    public Path path;
    public float nextWaypointDistance = 1;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;
    private float pathUpdateTime = .5f;
    private Rigidbody2D rb;
    IEnumerator myCoroutine;
    public Vector2 naviDir;


    public void Start () {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        myCoroutine = PathRoutine(pathUpdateTime);
        StartCoroutine(myCoroutine);
    }

    public void OnPathComplete (Path p) {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void Update () {

        if (path == null || target == null) {
            return;
        }

        reachedEndOfPath = false;

        float distanceToWaypoint;
        while (true) {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance) {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count) {
                    currentWaypoint++;
                } else {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            } else {
                break;
            }
        }

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        naviDir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        // Vector3 velocity = naviDir * speed;

        //transform.position += velocity * Time.deltaTime;
        if(!reachedEndOfPath)
        {
            
        } else 
        {
            naviDir = Vector2.zero;
        }
    }

    public IEnumerator PathRoutine(float waitTime)
    {
        while(true)
        {
            if(seeker.IsDone() && target != null)
            {
                seeker.StartPath(transform.position, target.position, OnPathComplete);
            }

            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnDisable () {
        seeker.pathCallback -= OnPathComplete;
        naviDir = Vector2.zero;
        StopCoroutine(myCoroutine);
    }
}