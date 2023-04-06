using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChecker : MonoBehaviour
{
    public Transform playerTransform;
    public float maxDistance = 3.5f;
    public float radius = 0.5f;
    public LayerMask interactableMask;
    public KeyCode pullKey = KeyCode.Mouse1;
    //public float pullForce = 10f;
    public Color lineColor = Color.blue;
    public Color otherColor = Color.red;
    public float throwSpeed = 2f;

    public Camera mainCamera;
    

    private void Awake()
    {
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(pullKey))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 directionToMouse = mousePosition - (Vector2)playerTransform.position;

            // Calculate the check position based on the maximum distance from the player
            Vector2 checkPosition = (Vector2)playerTransform.position + directionToMouse.normalized * maxDistance;

            // Check for interactable objects within a 0.3 radius around the mouse position
            Collider2D[] mouseInteractables = Physics2D.OverlapCircleAll(mousePosition, 0.3f, interactableMask);

            // Check for interactable objects within the radius around the check position
            Collider2D[] interactables = Physics2D.OverlapCircleAll(checkPosition, radius, interactableMask);

            // If the mouse is positioned beyond the max distance, only check for interactable objects at the max distance
            if (directionToMouse.magnitude > maxDistance)
            {
                interactables = Physics2D.OverlapCircleAll(playerTransform.position + new Vector3(directionToMouse.x, directionToMouse.y, 0).normalized * maxDistance, radius, interactableMask);
            }

            List<Collider2D> filteredMouseInteractables = new List<Collider2D>();

            // Filter the mouseInteractables array to only include objects within the maximum distance
            foreach (Collider2D interactable in mouseInteractables)
            {
                if (Vector2.Distance(playerTransform.position, interactable.transform.position) <= maxDistance)
                {
                    filteredMouseInteractables.Add(interactable);
                }
            }

            // Merge the arrays of interactable objects
            Collider2D[] allInteractables = new Collider2D[interactables.Length + filteredMouseInteractables.Count];
            interactables.CopyTo(allInteractables, 0);
            filteredMouseInteractables.CopyTo(allInteractables, interactables.Length);

            Transform currentWeapon = playerTransform.Find("PlayerWeapon");

            // Loop through all interactable objects and check their tag
            foreach (Collider2D interactable in allInteractables)
            {
                if (interactable.CompareTag("Interactable"))
                {
                    Debug.Log("You have found an interactable object");
                    Transform newWeapon = interactable.transform;
                    newWeapon.SetParent(playerTransform);
                    newWeapon.position = currentWeapon.position;
                    newWeapon.name = "PlayerWeapon";
                    currentWeapon.gameObject.SetActive(false);
                    Debug.Log("You have replaced your old weapon");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            // Get the PlayerWeapon object from the player transform
            Transform playerWeapon = playerTransform.Find("PlayerWeapon");

            if (playerWeapon != null)
            {
                // Detach the PlayerWeapon from the player transform
                playerWeapon.SetParent(null);

                // Add a Rigidbody2D component to the PlayerWeapon
                Rigidbody2D rb = playerWeapon.gameObject.AddComponent<Rigidbody2D>();

                // Set the velocity of the PlayerWeapon in the direction of the mouse
                Vector2 throwDirection = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - playerWeapon.position).normalized;
                rb.velocity = throwDirection * throwSpeed;

                // Replace the PlayerWeapon with an empty gameObject
                GameObject emptyWeapon = new GameObject();
                emptyWeapon.name = "PlayerWeapon";
                emptyWeapon.transform.SetParent(playerTransform);
                emptyWeapon.transform.position = playerWeapon.position;

                // Disable the current PlayerWeapon object
                //playerWeapon.gameObject.SetActive(false);
            }
        }

        //THE FOLLOWING CODE ONLY CHECKS AT MAX DISTSANCE NOT IN-BETWEEN.

        //if (Input.GetKeyDown(pullKey))
        //{
        //    Debug.Log("Interaction key pressed!");
        //    Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //    Vector2 directionToMouse = mousePosition - (Vector2)playerTransform.position;
        //    Vector2 checkPosition = (Vector2)playerTransform.position + directionToMouse.normalized * maxDistance;
        //
        //    Collider2D[] interactables = Physics2D.OverlapCircleAll(checkPosition, radius, interactableMask);
        //
        //    foreach (Collider2D interactable in interactables)
        //    {
        //        if (interactable.CompareTag("Interactable"))
        //        {
        //            //Rigidbody2D rb = interactable.GetComponent<Rigidbody2D>();
        //
        //            //if (rb != null)
        //            //{
        //            //    Vector2 pullDirection = (Vector2)playerTransform.position - interactable.transform.position;
        //            //    rb.AddForce(pullDirection.normalized * pullForce, ForceMode2D.Impulse);
        //            //}
        //
        //            Debug.Log("You have found an interactable object");
        //        }
        //    }
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = lineColor;

        // Draw the wire sphere around the player
        Gizmos.DrawWireSphere(playerTransform.position, maxDistance);

        // Calculate the position between the player and the mouse
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = mousePosition - (Vector2)playerTransform.position;
        Vector2 checkPosition = (Vector2)playerTransform.position + directionToMouse.normalized * maxDistance;

        // Draw the line from the player to the check position
        Gizmos.DrawLine(playerTransform.position, checkPosition);

        // Draw the wire sphere at the check position
        Gizmos.DrawWireSphere(checkPosition, radius);
    }
}
