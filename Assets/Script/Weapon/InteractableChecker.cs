using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChecker : MonoBehaviour
{
    public Transform playerTransform;
    public LayerMask interactableMask;
    public float radius = 2f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = mousePosition - (Vector2)playerTransform.position;
        Vector2 checkPosition = (Vector2)playerTransform.position + directionToMouse.normalized * radius;

        Collider2D[] interactables = Physics2D.OverlapCircleAll(checkPosition, radius, interactableMask);

        foreach (Collider2D interactable in interactables)
        {
            if (interactable.CompareTag("interactable"))
            {
                Debug.Log("Interactable object is within range!");
                // Do something with the interactable object
            }
        }
    }
}
