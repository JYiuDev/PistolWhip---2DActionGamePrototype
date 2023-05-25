using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public Sprite rank1;
    public Sprite rank2;
    public Sprite rank3;
    public Sprite rank4;
    public Sprite rank5;
    public Sprite rank6;
    public Sprite rank7;
    public Sprite rank8;

    public Sprite emptyHand;
    public Sprite gunWithNone;
    public Sprite gunWith1;
    public Sprite gunWith2;
    public Sprite gunWith3;
    public Sprite gunWith4;
    public Sprite gunWith5;
    public Sprite gunWith6;
    public Sprite bottleEquipped;
    public Sprite shieldEquipped;

    public Sprite levelOne;
    public Sprite levelTwo;
    public Sprite levelThree;

    public Text comboText;

    public Sprite extraHitActive;
    public Sprite extraHitInactive;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject weaponPosObject = GameObject.FindWithTag("WeaponPos");

        //HOLDING NOTHING

        if (weaponPosObject != null && weaponPosObject.transform.childCount == 0)
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = emptyHand;
        }

        //BARREL UI

        if (weaponPosObject != null && weaponPosObject.transform.childCount > 0 && weaponPosObject.transform.GetChild(0).CompareTag("Gun"))
        {
            //gun has 0 bullets etc
            if (weaponPosObject.transform.GetChild(0).GetComponent<Revolver>().bulletCount == 0)
            {
                this.transform.GetChild(0).GetComponent<Image>().sprite = gunWithNone;
            } else if (weaponPosObject.transform.GetChild(0).GetComponent<Revolver>().bulletCount == 1)
            {
                this.transform.GetChild(0).GetComponent<Image>().sprite = gunWith1;
            } else if (weaponPosObject.transform.GetChild(0).GetComponent<Revolver>().bulletCount == 2)
            {
                this.transform.GetChild(0).GetComponent<Image>().sprite = gunWith2;
            } else if (weaponPosObject.transform.GetChild(0).GetComponent<Revolver>().bulletCount == 3)
            {
                this.transform.GetChild(0).GetComponent<Image>().sprite = gunWith3;
            } else if (weaponPosObject.transform.GetChild(0).GetComponent<Revolver>().bulletCount == 4)
            {
                this.transform.GetChild(0).GetComponent<Image>().sprite = gunWith4;
            } else if (weaponPosObject.transform.GetChild(0).GetComponent<Revolver>().bulletCount == 5)
            {
                this.transform.GetChild(0).GetComponent<Image>().sprite = gunWith5;
            } else if (weaponPosObject.transform.GetChild(0).GetComponent<Revolver>().bulletCount == 6)
            {
                this.transform.GetChild(0).GetComponent<Image>().sprite = gunWith6;
            }
        }

        if (weaponPosObject != null && weaponPosObject.transform.childCount > 0 && weaponPosObject.transform.GetChild(0).CompareTag("Bottle"))
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = bottleEquipped;
        }

        if (weaponPosObject != null && weaponPosObject.transform.childCount > 0 && weaponPosObject.transform.GetChild(0).CompareTag("Shield"))
        {
            this.transform.GetChild(0).GetComponent<Image>().sprite = shieldEquipped;
        }

        //RANK UI
        //none
        if (player.GetComponent<styleScriptTwo>().styleAmount == 0)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = rank1;
        }
        //deputy
        if (player.GetComponent<styleScriptTwo>().styleAmount > 0 && player.GetComponent<styleScriptTwo>().styleAmount <= 100)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = rank2;
        }
        //sheriff
        if (player.GetComponent<styleScriptTwo>().styleAmount > 100 && player.GetComponent<styleScriptTwo>().styleAmount <= 250)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = rank3;
        }
        //vigilante
        if (player.GetComponent<styleScriptTwo>().styleAmount > 250 && player.GetComponent<styleScriptTwo>().styleAmount <= 450)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = rank4;
        }
        //hero
        if (player.GetComponent<styleScriptTwo>().styleAmount > 450 && player.GetComponent<styleScriptTwo>().styleAmount <= 700)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = rank5;
        }
        //legend
        if (player.GetComponent<styleScriptTwo>().styleAmount > 700 && player.GetComponent<styleScriptTwo>().styleAmount <= 1000)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = rank6;
        }
        //myth
        if (player.GetComponent<styleScriptTwo>().styleAmount > 1000 && player.GetComponent<styleScriptTwo>().styleAmount <= 20000)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = rank7;
        }
        //myth+
        if (player.GetComponent<styleScriptTwo>().styleAmount > 20000)
        {
            this.transform.GetChild(1).GetComponent<Image>().sprite = rank8;
        }

        //LEVEL UI

        if (SceneManager.GetActiveScene().name == "MainWorldTest")
        {
            this.transform.GetChild(2).GetComponent<Image>().gameObject.SetActive(false);
        } else
        {
            this.transform.GetChild(2).GetComponent<Image>().gameObject.SetActive(true);
        }

        if (SceneManager.GetActiveScene().name == "GetToEndTest")
        {
            this.transform.GetChild(2).GetComponent<Image>().sprite = levelOne;
        }

        if (SceneManager.GetActiveScene().name == "LevelKillTest")
        {
            this.transform.GetChild(2).GetComponent<Image>().sprite = levelTwo;
        }

        if (SceneManager.GetActiveScene().name == "LevelHeistTest")
        {
            this.transform.GetChild(2).GetComponent<Image>().sprite = levelThree;
        }

        //COMBO UI

        comboText.text = "" + player.GetComponent<styleScriptTwo>().styleAmount;

        //EXTRA HIT UI

        if (player.GetComponent<styleScriptTwo>().hasAdrenaline == true)
        {
            this.transform.GetChild(4).GetComponent<Image>().sprite = extraHitActive;
        } else
        {
            this.transform.GetChild(4).GetComponent<Image>().sprite = extraHitInactive;
        }
    }
}
