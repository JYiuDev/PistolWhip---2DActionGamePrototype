using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private int LevelOneKills;
    private float LevelOneTime;
    private bool LevelOneCollected;
    private Text LevelOneScore;

    private int LevelTwoKills;
    private float LevelTwoTime;
    private bool LevelTwoCollected;
    private Text LevelTwoScore;

    private int LevelThreeKills;
    private float LevelThreeTime;
    private bool LevelThreeCollected;
    private Text LevelThreeScore;

    private int LevelFourKills;
    private float LevelFourTime;
    private bool LevelFourCollected;
    private Text LevelFourScore;

    private int LevelFiveKills;
    private float LevelFiveTime;
    private bool LevelFiveCollected;
    private Text LevelFiveScore;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
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
