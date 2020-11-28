using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public Character myCharacter; 

    [HideInInspector]
    public UiManager ui_manager;

    public List<LevelData> levelInfo;
    public List<GameObject> levels;

    public Material playerMat;
    public Material groundMat;
    public Material cubesMat;
    public Text LevelLength;
    public Text playerID;
    public GameObject currentLvl;

    public List<World> worlds;
    public List<Weapon> weapons;


    public Weapon currentWeapon;
    public World currentWorld;
   

    private int _coinsAmount = 0;



    [Space(30)]
    public List<InventoryButton> buttons;

    public int CoinsAmount {
        get
        {
            return _coinsAmount;
        }
        set
        {
            _coinsAmount+= value;
            ui_manager.AddCoin(_coinsAmount);
        }
    }

    public static GameManager Instance { set; get; }
    private void OnTriggerEnter(Collider other) {

    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }


    void Start() {
        currentWeapon = weapons[Random.Range(0, weapons.Count)];
        LoadNextLevel();
        ui_manager = GetComponent<UiManager>();
    }


    void GetRandomColor() {
        LevelData currentData = levelInfo[Random.Range(0, levelInfo.Count)];
        playerMat.SetColor("_Color", currentData.playerColor);
        groundMat.SetColor("_Color", currentData.groundColor);
        cubesMat.SetColor("_Color", currentData.cubesColor);
        LevelLength.text = "LevelLength: " + currentData.levelLength.ToString();
        playerID.text = "ID: " + currentData.playerID.ToString();
    }
    void LoadNextLevel() {
        Destroy(currentLvl);
        currentLvl = Instantiate(levels[Random.Range(0, levels.Count)]);
        GetRandomColor();
    }


    public void ButtonClicked(int buttonID)
    {
        if(!buttons[buttonID].isUsed)
        {
            Debug.Log("There is no item in that slot");
        }
        else if(buttons[buttonID].isUsed)
        {
            switch (buttons[buttonID].myButtonState)
            {
                case buttonState.REDSWORD:
                    myCharacter.ChangeSword(true);
                    break;

                case buttonState.GREENSWORD:
                    myCharacter.ChangeSword(false);
                    break;
                case buttonState.HEALTH:
                    myCharacter.ChangeHealthValue(buttons[buttonID].healthValue,true);
                    buttons[buttonID].itemAmount -= 1;
                    buttons[buttonID].totalAmountTxt.text = buttons[buttonID].itemAmount.ToString();
                    break;
            }
            if(buttons[buttonID].itemAmount <= 0)
            {
                Color temp = buttons[buttonID].Btn.GetComponent<Image>().color;
                temp = new Color(temp.r, temp.g, temp.b, 0.1f);
                buttons[buttonID].Btn.GetComponent<Image>().sprite = null;
                buttons[buttonID].isUsed = false;
                buttons[buttonID].myButtonState = buttonState.NOTHING;
                buttons[buttonID].Btn.GetComponent<Image>().color = temp;
            }
        }
    }
    public void ChangeButtonState(buttonState state,Sprite buttonSprite,float healthAmount)
    {
        for(int i = 0;i != buttons.Count;i++)
        {
            for (int a = 0; a != buttons.Count; a++)
            {
                if (buttons[a].isUsed == true && buttons[a].myButtonState == state && buttons[a].itemAmount > 0 && healthAmount == buttons[a].healthValue)
                {
                    buttons[a].itemAmount += 1;
                    buttons[a].totalAmountTxt.text = buttons[a].itemAmount.ToString();
                    buttons[a].healthValue = healthAmount;
                    return;
                }

            }
            if (buttons[i].isUsed == false)
            {
                Color temp = buttons[i].Btn.GetComponent<Image>().color;
                temp = new Color(temp.r, temp.g, temp.b, 1f);
                Debug.Log(buttons[i].isUsed);
                buttons[i].isUsed = true;
                buttons[i].myButtonState = state;
                buttons[i].Btn.GetComponent<Image>().sprite = buttonSprite;
                buttons[i].Btn.GetComponent<Image>().color = temp;
                buttons[i].itemAmount += 1;
                buttons[i].totalAmountTxt.text = buttons[i].itemAmount.ToString();
                buttons[i].healthValue = healthAmount;
                return;
                
            }
          
        }
    }

    public bool CheckForSameButton(buttonState state, float healthAmount)
    {
        int index = 0;
        bool finalResult = false;
        for (int i = 0;i != buttons.Count; i++)
        {
            if (buttons[i].isUsed == true && buttons[i].myButtonState == state && buttons[i].itemAmount > 0 && healthAmount == buttons[i].healthValue)
            {
                finalResult = true;
                index = i;
            }

        }
        return finalResult;
    }

    public void UpdateHealth() {
        ui_manager.ChangeHealthUI(myCharacter.currentHealth);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            LoadNextLevel();
        }

    }


    [System.Serializable]
    public enum WorldEnum
    {
        gondor, rohan, moria, saxli, wyaltubo
    }
    public enum WeaponEnum
    {
        sword, spear, axe, hand, horse
    }

  




    [System.Serializable]
    public class World
    {
        public WorldEnum worldName;
        public GameObject worldObject;
        public int distance;
        public float timeToTravel;
    }


    [System.Serializable]
    public class Weapon
    {
        public WeaponEnum weaponName;
        public float damage;
        public int lvl;
    }

    [System.Serializable]
    public class InventoryButton
    {
        public Button Btn;
        public bool isUsed;
        public Text totalAmountTxt;
        public float healthValue;
        public float itemAmount;
        public buttonState myButtonState;
    }

    [System.Serializable]
    public enum buttonState
    {
        REDSWORD, GREENSWORD, HEALTH,NOTHING
    }
}
