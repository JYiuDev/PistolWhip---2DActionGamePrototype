using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRender : MonoBehaviour
{
    [SerializeField] WhipPoint whipPoint;
    private Vector2 targetPos;
    private Vector2 firePos;
    private LineRenderer line;
    private float moveTime = 0;

    //The amount of points in line renderer
    [Range(0,80)][SerializeField] private int percision = 40;
    [SerializeField] AnimationCurve ropeAnimationCurve;
    [SerializeField] AnimationCurve ropeProgressionCurve;
    [SerializeField] float ropeProgressionSpeed;
    private bool straightLine = true;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        //reset whip to initial point
        moveTime = 0;
        line.positionCount = percision;
        
        for(int i = 0; i < percision; i++)
        {
            line.SetPosition(i, whipPoint.GetFirePoint());
        }

        line.enabled = true;
    }

    private void OnDisable()
    {
        line.enabled = false;
    }

    private void Update()
    {
        targetPos = transform.InverseTransformPoint(whipPoint.GetTargetPoint());
        firePos = transform.InverseTransformPoint(whipPoint.GetFirePoint());
        moveTime += Time.deltaTime;
        DrawRope();
    }


    void DrawRope()
    {
        // for(int i = 0; i < percision; i++)
        // {
        //     //value for difference between verticies depending on percision
        //     float delta = (float) i / ((float)percision - 1f);
            
        //     //Calculation for each line vertex
        //     Vector2 currentPosition = Vector2.Lerp(whipPoint.GetFirePoint(), whipPoint.GetTargetPoint(), ropeProgressionCurve.Evaluate(moveTime)* ropeProgressionSpeed);

        //     line.SetPosition(i, currentPosition);
        // }
        if(!straightLine)
        {

        }
        if(straightLine)
        {
            if(line.positionCount != 2)
            {
                line.positionCount = 2;
            }

            DrawRopeStraight();
        }
    }

    void DrawRopeStraight()
    {
        line.SetPosition(0, firePos);
        line.SetPosition(1, targetPos);
    }


    void DrawRopeWaves()
    {   
        for(int i = 0; i < percision; i++)
        {
            //Calculate each vertex's percentage in all verticies
            float delta = (float)i / ((float) percision - 1f);
            //Calculate offset from a straight line
            Vector2 offset = Vector2.Perpendicular(transform.InverseTransformVector(whipPoint.GetGrapplingDirection())*ropeAnimationCurve.Evaluate(delta) * 1);
            //Calculate target position for every vertex
            Vector2 targetPosition = Vector2.Lerp(firePos, targetPos, delta) + offset;
            //Calculate 
        }
    }

}
