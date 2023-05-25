using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userInterface : MonoBehaviour
{
    public static userInterface instance = null;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
