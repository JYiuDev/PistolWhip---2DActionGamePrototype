using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : WeaponClass
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] public int bulletCount = 6;
    //[SerializeField] private float initialThrownSpd = 8;

    public Revolver()
    {
        type = WeaponType.REVOLVER;
    }

    public override void LeftClick()
    {
        Shoot();
    }

    public override void RightClick()
    {
        Shoot();
    }

    public override void ThrowInteractions(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            rb.velocity = (-rb.velocity).normalized * 1;
        }
    }

    public void Shoot()
    {
        if (bulletCount > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            bullet.GetComponent<BulletMove>().SetBulletSpeed(bulletSpeed);
            bulletCount--;
        }
    }
}
