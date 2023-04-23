using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToWorld : MonoBehaviour
{

    [SerializeField] private float interactDistance = 3f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteractionWithSceneObjects();
        }
    }

    private void CheckInteractionWithSceneObjects()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= interactDistance)
        {
            SceneManager.LoadScene("LevelTest");
            Debug.Log("You have returned to the main world");
        }
    }
}
