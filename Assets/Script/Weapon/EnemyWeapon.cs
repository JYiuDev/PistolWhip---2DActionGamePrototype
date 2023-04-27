using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private EnemyRanged main;
    private Vector2 AimPosition;
    [SerializeField] private Vector2 targetPos;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed;

    // Reference to the Detection script
    public Detection detection;


    void Awake()
    {
        main = GetComponentInParent<EnemyRanged>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        // Check if player is detected before rotating weapon
        if (detection != null && detection.playerFound)
        {
            targetPos = main.getTargetPos();
            Vector3 direction = (targetPos - (Vector2)transform.position).normalized;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotation);

            //Check which direction the object is facing and flip it accordingly
            if (Mathf.Abs(rotation) > 90)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        bullet.GetComponent<BulletMove>().SetBulletSpeed(bulletSpeed);
    }

    public void dropWeapon()
    {
        
    }
}
