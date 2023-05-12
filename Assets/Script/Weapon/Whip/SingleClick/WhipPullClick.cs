using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipPullClick : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public PullRopeClick grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;
    [SerializeField] private LayerMask obstacleLayer;
    
    private enum State{extend, retract, inactive};
    [SerializeField] private State state = State.inactive;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    //[SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    //[SerializeField] private float launchSpeed = 1;

    //[Header("No Launch To Point")]
    //[SerializeField] private bool autoConfigureDistance = false;
    //[SerializeField] private float targetDistance = 3;
    //[SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    [SerializeField] private Transform pulledObj;
    [SerializeField] float pullSpeed = 3;
    private AudioSource whipAudio;
    private void Awake()
    {
        whipAudio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        grappleRope.enabled = false;
    }

    private void Update()
    {
        switch(state)
        {
            case State.inactive:

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    // Check if WeaponPos has no child attached before grappling an object
                    if (gunHolder.GetComponent<PlayerController>().GetWeaponPos().childCount > 0)
                    {
                        return;
                    } else
                    {
                        SetGrapplePoint();
                    }
                }

            break;

            case State.extend:

                if (launchToPoint && grappleRope.isGrappling)
                {
                    pulledObj.gameObject.layer = LayerMask.NameToLayer(CollisionLayer.RetractObjects);
                    state = State.retract;
                }

            break;

            case State.retract:

                //Updating the grapple point position, pulled obj position, and rope origin position
                Vector3 pullDirection = (pulledObj.position - transform.position).normalized;
                pulledObj.transform.position -= pullDirection * pullSpeed * Time.deltaTime;
                Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                Vector2 targetPos = (Vector2)pulledObj.position - firePointDistnace;
                grapplePoint -= (Vector2)pullDirection * pullSpeed * Time.deltaTime;
                
                //I'm sorry for puting arbitrary numbers
                //If pulledObj gets close, attach
                if(Vector2.Distance(pulledObj.position, gunHolder.position) < 0.75f)
                {
                    pulledObj.GetComponent<ThrownObj>().Attach(gunHolder.GetComponent<PlayerController>().GetWeaponPos());
                    gunHolder.GetComponent<PlayerController>().SetWeapon(pulledObj.GetComponent<WeaponClass>());
                    whipInactive();
                }
            break;
        }

        //Point direction to whipped object if it is grabbed, and allow whip pivot to rotate if not
        if (grappleRope.enabled)
        {
            RotateGun(grapplePoint, false);
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    whipAudio.Play();
                    pulledObj =  _hit.transform;
                    grapplePoint = _hit.point;
                    pulledObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                    state = State.extend;
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

    public void whipInactive()
    {
        grappleRope.enabled = false;
        pulledObj = null;
        state = State.inactive;
    }
}
