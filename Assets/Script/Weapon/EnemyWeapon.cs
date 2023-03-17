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


    void Awake()
    {
        main = GetComponentInParent<EnemyRanged>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        targetPos = main.getTargetPos();


        Vector3 direction = (targetPos - (Vector2)transform.position).normalized;       //Vector from weapon to mouse

        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  //Calculate rotation angle from vector

        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        bullet.GetComponent<BulletMove>().SetBulletSpeed(bulletSpeed);
    }
}
