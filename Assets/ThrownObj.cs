using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObj : MonoBehaviour
{
    [SerializeField] private float initSpeed = 8f;      //arbitrary default value
    private float speed;
    private float lifeTime = 5;   //arbitrary default value
    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    [SerializeField] private WhipPullClick whip;
    [SerializeField] private float shieldSlowdown = 10f;
    [SerializeField] private float gunSlowdown = 100f;
    [SerializeField] private float bottleSlowdown = 30f;

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
               rb.velocity = newDir * lastVelocity.magnitude;
           break;
       }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && (transform.parent != null))
        {
            Launch(initSpeed);
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
        transform.SetParent(null);
        rb.velocity = transform.right * s;
        gameObject.layer = LayerMask.NameToLayer(CollisionLayer.PullObjects);

        if(gameObject.CompareTag("Shield"))
        {
            Collider2D col = GetComponent<BoxCollider2D>();
            col.isTrigger = false;
        }
        // Gradually reduce velocity to zero over time
        StartCoroutine(SlowDown());

    }

    private IEnumerator SlowDown()
    {
        while (rb.velocity.magnitude > 0.1f)
        {
            

            if(gameObject.CompareTag("Shield"))
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, shieldSlowdown * Time.deltaTime);
            }

            else if (gameObject.CompareTag("Gun"))
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, gunSlowdown * Time.deltaTime);
            }

            else if (gameObject.CompareTag("Bottle"))
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, bottleSlowdown * Time.deltaTime);
            }
            yield return null;
        }

        // Stop the object completely
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && gameObject.CompareTag("Bottle"))
        {
            rb.velocity = Vector2.one;
        }
    }
    public void Attach(Transform parent)
    {
        gameObject.layer = LayerMask.NameToLayer(CollisionLayer.IgnoreCollision);
        transform.SetParent(parent);
        transform.position = parent.position;
        transform.rotation = parent.rotation;

        //Note to self, take this out eventually plz
        if(gameObject.CompareTag("Shield"))
        {
            Collider2D col = GetComponent<BoxCollider2D>();
            col.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Shields");
        }
    }
}
