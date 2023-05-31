using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(WeaponClass))]
[RequireComponent(typeof(Rigidbody2D))]

public class ThrownObj : MonoBehaviour
{
    [SerializeField] private float initSpeed = 8f;      //arbitrary default value
    private float speed;
    //private float lifeTime = 5;   //arbitrary default value
    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    [SerializeField] private float shieldSlowdown = 10f;
    [SerializeField] private float gunSlowdown = 100f;
    [SerializeField] private float bottleSlowdown = 30f;
    [SerializeField] private float knifeSlowdown = 1f;
    [SerializeField] private float itemSlowdown = 1f;
    private WeaponClass weapon;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<WeaponClass>();
    }

    void Start()
    {

    }

    void OnCollisionStay2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Block":

                /*  Debug.Log(other.contactCount);
                  if (other.contactCount == 1)
                  {
                      Vector2 _otherNormal = other.contacts[0].normal;
                      Vector2 newDir = Vector2.Reflect(lastVelocity, _otherNormal).normalized;
                      rb.velocity = newDir * lastVelocity.magnitude;
                  }
                  else
                  {
                      Vector2 prevPosition = other.contacts[0].point;
                      Vector2 prevNorm = other.contacts[0].normal;
                      Vector2 prevDir = Vector2.Reflect(lastVelocity, prevNorm);

                      for (int i = 1; i < other.contactCount; i++)
                      {
                          RaycastHit2D h = Physics2D.Raycast(prevPosition, prevDir);
                          if (h.collider == null) { break; }

                          Vector2 newDir = Vector2.Reflect(prevDir, prevNorm).normalized;

                          prevDir = newDir;
                          prevPosition = h.point;
                          prevNorm = h.normal;
                      }

                      rb.velocity = prevDir * lastVelocity.magnitude;
                  }
  */
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (transform.parent != null))
        {
            if(transform.parent.CompareTag("WeaponPos"))
            {
                GameObject weaponObjName = GameObject.FindWithTag("WeaponPos");

                Transform child = weaponObjName.transform.GetChild(0);

                Debug.Log("You threw the " + child.name + " at " + GameObject.FindWithTag("Player").transform.position.x + ", " + GameObject.FindWithTag("Player").transform.position.y + ".");
                
                if(gameObject.tag == "Knife")
                {
                    gameObject.GetComponent<Knife>().animator.Play("Base Layer.KnifeIdle", 0, 0);
                }

                Launch(initSpeed);

            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastVelocity, 0.4f);

        if(hit.collider != null && hit.transform.CompareTag("Block"))
        {
            Vector2 _otherNormal = hit.normal;
            Vector2 newDir = Vector2.Reflect(lastVelocity, _otherNormal).normalized;
            rb.velocity = newDir * lastVelocity.magnitude;
            lastVelocity = rb.velocity;
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
        //This is kinda scuffed but it works
        // PlayerController player = transform.parent.parent.GetComponent<WeaponPivot>().GetPlayerController();
        // player.RemoveWeapon();

        transform.SetParent(null);
        rb.velocity = transform.right * s;
        gameObject.layer = LayerMask.NameToLayer(CollisionLayer.PullObjects);

        // Gradually reduce velocity to zero over time
        StartCoroutine(SlowDown());
    }

    private IEnumerator SlowDown()
    {
        while (rb.velocity.magnitude > 0.2f)
        {
            if (gameObject.CompareTag("Shield"))
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

            else if (gameObject.CompareTag("Knife"))
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, knifeSlowdown * Time.deltaTime);
            }

             else if (gameObject.CompareTag("HeistItem"))
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, itemSlowdown * Time.deltaTime);
            }

            yield return null;
        }

        // Stop the object completely
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.layer == LayerMask.NameToLayer("PullObjects") && rb.velocity != Vector2.zero)
        {
            weapon.ThrowInteractions(other);
        }
        
    }

    public void Attach(Transform parent)
    {
        gameObject.layer = LayerMask.NameToLayer(CollisionLayer.IgnoreCollision);
        transform.SetParent(parent);
        transform.position = parent.position;
        transform.rotation = parent.rotation;

        //Note to self, take this out eventually plz
        if (gameObject.CompareTag("Shield"))
        {
            Collider2D col = GetComponent<BoxCollider2D>();
            col.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Shields");
        }
    }

    // void OnDrawGizmos()
    // {
    //     RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
    //     Gizmos.DrawLine(transform.position, transform.position + (transform.right * (hit.collider == null ? 5 : Vector2.Distance(hit.point, transform.position))));

    //     if (hit.collider != null)
    //     {
    //         Vector2 prevPosition = transform.position + (transform.right * Vector2.Distance(hit.point, transform.position));
    //         Vector2 prevDir = transform.right;
    //         Vector2 prevNorm = hit.normal;
    //         for (int i = 0; i < 5; i++)
    //         {
    //             Vector2 newDir = Vector2.Reflect(prevDir, prevNorm).normalized;
                
    //             RaycastHit2D h = Physics2D.Raycast(prevPosition, newDir);
    //             if (h.collider == null) { break; }
    //             Gizmos.DrawLine(prevPosition, prevPosition + (newDir * Vector2.Distance(prevPosition, h.point)));

    //             prevDir = newDir;
    //             prevPosition = h.point;
    //             prevNorm = h.normal;
    //         }
    //     }
    // }
}
