using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairRetry : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite targetSprite;   

    private SpriteRenderer spriteRenderer;
    public static CrosshairRetry instance = null;

    private void Awake()
    {

            //Check if instance already exists
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = originalSprite;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        transform.position = mousePosition;

        Collider2D[] hits = Physics2D.OverlapCircleAll(mousePosition, 0.1f);
        bool isAboveEnemyOrPullObject = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy") || hit.gameObject.layer == LayerMask.NameToLayer("PullObjects"))
            {
                isAboveEnemyOrPullObject = true;
                break;
            }
        }

        if (isAboveEnemyOrPullObject)
        {
            spriteRenderer.sprite = targetSprite;
        }
        else
        {
            spriteRenderer.sprite = originalSprite;
        }
    }
}
