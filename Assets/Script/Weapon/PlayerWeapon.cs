using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Vector2 AimPosition;
    private Camera cam;
    private Vector3 mousePos;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //translate screen mouse position to world
        Vector3 direction = (mousePos - transform.position).normalized;       //Vector from weapon to mouse

        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  //Calculate rotation angle from vector

        transform.rotation = Quaternion.Euler(0, 0, rotation);

        // Flip the sprite if pointing left
        if (Mathf.Abs(rotation) > 90)
        {
            spriteRenderer.flipY = true;
            spriteRenderer.flipX = false;
        }
        // Flip the sprite back if pointing right
        else
        {
            spriteRenderer.flipY = false;
            spriteRenderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootBullet();
        }
    }

    public void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        bullet.GetComponent<BulletMove>().SetBulletSpeed(bulletSpeed);
    }

}
