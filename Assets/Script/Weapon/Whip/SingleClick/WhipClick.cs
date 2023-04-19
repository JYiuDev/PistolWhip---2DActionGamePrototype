using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipClick : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private RopeRender rope;

    [SerializeField] private int grappleLayerNumber;

    
    [SerializeField] private Transform pulledObj;
    [SerializeField] private float pullSpeed;

    private Vector2 grapplePoint;
    
    void Start()
    {
        rope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If key, shoot grapple
        if(Input.GetKey(KeyCode.Mouse1))
        {
            SetGrapplePoint();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            rope.enabled = false;
            pulledObj = null;
        }
    }

    void SetGrapplePoint()
    {
        //Get firepoint to mouse position vector
        Vector2 raycastVector = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Raycast in that direction
        if(Physics2D.Raycast(transform.position, raycastVector.normalized))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastVector.normalized);
            if(hit.transform.gameObject.layer == grappleLayerNumber)
            {
                pulledObj = hit.transform;
                grapplePoint = pulledObj.position;
                rope.enabled = true;
            }
        }


        if (pulledObj != null)
        {   
            PullSnappedObject();
        }
        
    }






    private void PullSnappedObject()
    {
        Vector3 pullDirection = (pulledObj.position - transform.position).normalized;
        pulledObj.transform.position -= pullDirection * pullSpeed * Time.deltaTime;
    }

    public Vector2 GetTargetPoint()
    {
        return pulledObj.position;
    }

    public Vector2 GetFirePoint()
    {
        return transform.position;
    }

    public Vector2 GetGrapplingDirection()
    {
        return (pulledObj.position - transform.position).normalized;
    }



}
