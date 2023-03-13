using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    private Vector2 AimPosition;
    private Camera cam;
    private Vector3 mousePos;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //translate screen mouse position to world
        Vector3 direction = (mousePos - transform.position).normalized;       //Vector from weapon to mouse

        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  //Calculate rotation angle from vector

        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
