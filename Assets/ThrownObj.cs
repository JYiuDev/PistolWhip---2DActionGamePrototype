using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObj : MonoBehaviour
{
    private float speed = 20f;      //arbitrary default value
    private float lifeTime = 5;   //arbitrary default value
    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    [SerializeField] private WhipPullClick whip;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        whip = FindObjectOfType<WhipPullClick>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Block":
                Vector2 _otherNormal = other.contacts[0].normal;
                Vector2 newDir = Vector2.Reflect(lastVelocity, _otherNormal).normalized;
                rb.velocity = newDir * speed;
            break;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && (transform.parent != null))
        {
            Launch(5f);
        }
    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    public void SetProjectileSpeed(Vector2 s)
    {
        rb.velocity = s;
    }

    public void Launch(float s)
    {
        speed = s;
        //Detach from pivot
        transform.SetParent(null);
        rb.velocity = transform.right * speed;
        gameObject.layer = LayerMask.NameToLayer(CollisionLayer.PullObjects);
    }
    public void Attach(Transform parent)
    {
        gameObject.layer = LayerMask.NameToLayer(CollisionLayer.IgnoreCollision);
        transform.SetParent(parent);
        transform.position = parent.position;
        transform.rotation = parent.rotation;
    }
}
