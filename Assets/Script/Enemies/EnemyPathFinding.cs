using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathFinding : MonoBehaviour
{
    public Transform target;
    
    public float speed = 2f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndPath = false;
    
    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndPath = true;
            return;
        } else
        {
            reachedEndPath = false;
        }

        Vector2 directon = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
    }

    void OnPathComplete(Path p)
    {
        if (!path.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
