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

    [SerializeField] private int maxBullets = 6;
    private int bulletCount = 0;


    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Start()
    {
        
    }

    void Update()
    {

        // Check if the object is a child of WeaponPos before allowing rotation
        if (transform.parent != null && transform.parent.CompareTag("WeaponPos"))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //translate screen mouse position to world
            Vector3 direction = (mousePos - transform.position).normalized; //Vector from weapon to mouse
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  //Calculate rotation angle from vector
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootBullet();
        }
  
    }


    public void ShootBullet()
    {
        // Check if the object is a child of WeaponPos and if the bulletCount is less than the maxBullets
        if (transform.parent != null && transform.parent.CompareTag("WeaponPos") && bulletCount < maxBullets)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            bullet.GetComponent<BulletMove>().SetBulletSpeed(bulletSpeed);
            bulletCount++; // Increment the bullet count
        }
    }
}
