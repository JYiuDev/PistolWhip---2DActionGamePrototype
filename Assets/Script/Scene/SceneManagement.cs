using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public GameObject levelOneDoor;
    public GameObject levelTwoDoor;
    public GameObject levelThreeDoor;
    public GameObject levelFourDoor;
    public GameObject levelFiveDoor;

    private GameObject player;
    private float interactDistance = 20f;

    // Check the player
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

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
        if (Vector2.Distance(player.transform.position, levelOneDoor.transform.position) <= interactDistance)
        {
            SceneManager.LoadScene("LevelOne");
            Debug.Log("You have entered Leveel One");
        }
        else if (Vector2.Distance(player.transform.position, levelTwoDoor.transform.position) <= interactDistance)
        {
            SceneManager.LoadScene("LevelTwo");
            Debug.Log("You have entered Leveel Two");
        }
        else if (Vector2.Distance(player.transform.position, levelThreeDoor.transform.position) <= interactDistance)
        {
            SceneManager.LoadScene("LevelThree");
            Debug.Log("You have entered Leveel Three");
        }
        else if (Vector2.Distance(player.transform.position, levelFourDoor.transform.position) <= interactDistance)
        {
            SceneManager.LoadScene("LevelFour");
            Debug.Log("You have entered Leveel Four");
        }
        else if (Vector2.Distance(player.transform.position, levelFiveDoor.transform.position) <= interactDistance)
        {
            SceneManager.LoadScene("LevelFive");
            Debug.Log("You have entered Leveel Five");
        }
    }
}
