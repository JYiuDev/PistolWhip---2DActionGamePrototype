using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private float viewRadius;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float searchInterval = 0.1f;
    //private CircleCollider2D detectionCircle;
    [SerializeField] public bool playerFound = false;
    [SerializeField] private List<Transform> visibleTargets = new List<Transform>();
    Collider2D[] targetsInRange;
    [SerializeField] private Transform playerPos;

    void Start()
    {
        //detectionCircle = gameObject.GetComponent<CircleCollider2D>();
        StartCoroutine("FindTargetsWithDelay", searchInterval);
    }

    public bool PlayerDetected()
    {
        return playerFound;
    }
    void OnEnable()
    {
        StartCoroutine("FindTargetsWithDelay", searchInterval);
    }
    private void FindVisibleTargets() //referenced from old code 
    {
        visibleTargets.Clear();

        //Find all objects in range on specified target layer
        targetsInRange = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetLayer);

        

        //Raycast to check if any obstacles are in the way, if not, add target to visible list
        for(int i = 0; i < targetsInRange.Length; i++)
        {
            Transform target = targetsInRange[i].transform;
            Vector2 targetDir = (target.position - transform.position).normalized;

            float targetDist = Vector2.Distance(transform.position, target.position);

            if(!Physics2D.Raycast(transform.position, targetDir, targetDist, obstacleLayer))
            {
                visibleTargets.Add(target);
                if(target.CompareTag("Player")){
                    playerFound = true;
                    playerPos = target;
                }
            }
        }

        //If nothing is in range, player is not found
        if (visibleTargets.Count == 0)
        {
            playerFound = false;
        }
    }

    public List<Transform> GetVisibleTargets()
    {
        return visibleTargets;
    }

    //Search at specific intervals
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    
    public bool isPlayerFound()
    {
        return playerFound;
    }

    public void SetRadius(float r)
    {
        viewRadius = r;
    }

    public Transform getTargetPos()
    {
        if(playerFound)
        {
            return playerPos;
        } else
        {
            return null;
        }
        
    }
}
