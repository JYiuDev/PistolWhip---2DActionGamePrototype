using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetToEnd : MonoBehaviour
{

    private float interactDistance = 2f;
    public GameObject player;
    public bool levelComplete = false;

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
        } else
        {
            levelComplete = false;
        }
    }

    private void CheckInteractionWithSceneObjects()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= interactDistance)
        {
            levelComplete = true;
            SceneManager.LoadScene("LevelTest");
            Debug.Log("You have returned to the main world");
        }
    }
}
