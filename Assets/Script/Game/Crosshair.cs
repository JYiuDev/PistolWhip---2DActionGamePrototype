using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image crosshairImage;
    [SerializeField] private Sprite generalSprite;
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float maxDistance;

    private RectTransform rectTransform;
    private Vector3 mousePosition;
    private bool isOverEnemy;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        isOverEnemy = false;
        maxDistance = 100f;
    }

    void Update()
    {
        // Get the mouse position in world space
        mousePosition = Input.mousePosition;
        mousePosition.z = 10; // Distance from the camera to the UI element

        // Convert the mouse position to a position on the canvas
        Vector3 canvasPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        rectTransform.position = canvasPosition;

        // Check if the mouse is over an enemy
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero, maxDistance, enemyLayerMask);

        if (hit.collider != null)
        {
            // If the mouse is over an enemy, change the crosshair sprite to the enemy sprite
            if (!isOverEnemy)
            {
                isOverEnemy = true;
                crosshairImage.sprite = enemySprite;
            }
        }
        else
        {
            // If the mouse is not over an enemy, change the crosshair sprite to the general sprite
            if (isOverEnemy)
            {
                isOverEnemy = false;
                crosshairImage.sprite = generalSprite;
            }
        }
    }
}