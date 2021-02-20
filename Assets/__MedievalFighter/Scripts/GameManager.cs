using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public Character myCharacter;
    public GameObject camHolder;
    public GameObject arrow;

    [Header("Properties")]
    public float camOffset;
    public float gameOverDelay = 2.5f;
    public float nextLevelDelay = 1.5f;



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

    public SoundController _soundController;

    public PlayerController playerController;
    public Data gameData;

    public bool gameActive { get; private set; }
    public bool playerHasreachedTheFinish=false;
    public int CoinsAmount {
        get
        {
            return _coinsAmount;
        }
        set
        {
            _coinsAmount += value;
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

        //ui_manager.healthIMG();
        currentWeapon = weapons[Random.Range(0, weapons.Count)];
        ui_manager = GetComponent<UiManager>();
        gameData = new Data();
        inventoryList = new List<InventoryData>();


        LoadGameInformation();
        LoadNextLevel();
    }

    public void StartGame()
    {
        gameActive = true;
        camHolder.GetComponent<CameraController2>().StartRotation(false);
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
        currentLvl = Instantiate(levels[gameData.lvlID]);
        GetRandomColor();
    }

    public void GameOver()
    {
        gameActive = false;
        Invoke(Constants.HardRestartString, gameOverDelay);
    }

    public void FinishStart()
    {
        gameActive = false;
        camHolder.GetComponent<CameraController2>().Finish();
    }

    public void FinishEnd()
    {
        Invoke(Constants.HardRestartString, nextLevelDelay);
        gameData.lvlID++;
        gameData.lvlID = Mathf.Clamp(gameData.lvlID, 0, levels.Count - 1);
        CheckSave();
    }

    public void ButtonClicked(int buttonID) {
        if (!buttons[buttonID].isAvialable) {
            Debug.Log("There is no item in that slot");
        }
        else if (buttons[buttonID].isAvialable) {
            switch (buttons[buttonID].myButtonState) {
                case buttonState.REDSWORD:
                    myCharacter.ChangeSword(true);
                    break;

                case buttonState.GREENSWORD:
                    myCharacter.ChangeSword(false);
                    break;
                case buttonState.HEALTH:
                    myCharacter.ChangeHealthValue(buttons[buttonID].healthValue, true);
                    buttons[buttonID].itemAmount -= 1;
                    buttons[buttonID].totalAmountTxt.text = buttons[buttonID].itemAmount.ToString();
                    break;
            }
            if (buttons[buttonID].itemAmount <= 0) {
                Color temp = buttons[buttonID].Btn.GetComponent<Image>().color;
                temp = new Color(temp.r, temp.g, temp.b, 0.1f);
                buttons[buttonID].Btn.GetComponent<Image>().sprite = null;
                buttons[buttonID].isAvialable = false;
                buttons[buttonID].myButtonState = buttonState.NOTHING;
                buttons[buttonID].Btn.GetComponent<Image>().color = temp;
            }
        }
    }
    public void AddToInventory(buttonState state, Sprite buttonSprite, float value) {
        for (int i = 0; i != buttons.Count; i++) {                     //amas rame movuxerxot
            for (int a = 0; a != buttons.Count; a++) {
                if (buttons[a].isAvialable == true && buttons[a].myButtonState == state && buttons[a].itemAmount > 0 && value == buttons[a].healthValue) {
                    buttons[a].itemAmount += 1;
                    buttons[a].totalAmountTxt.text = buttons[a].itemAmount.ToString();
                    buttons[a].healthValue = value;
                    return;
                }
            }
            if (buttons[i].isAvialable == false) {
                Color temp = buttons[i].Btn.GetComponent<Image>().color;
                temp = new Color(temp.r, temp.g, temp.b, 1f);
                //Debug.Log(buttons[i].isAvialable);
                buttons[i].isAvialable = true;
                buttons[i].myButtonState = state;
                buttons[i].Btn.GetComponent<Image>().sprite = buttonSprite;
                buttons[i].Btn.GetComponent<Image>().color = temp;
                buttons[i].itemAmount += 1;
                buttons[i].totalAmountTxt.text = buttons[i].itemAmount.ToString();
                buttons[i].healthValue = value;



                return;

            }
        }

    }
    public void AddToInventory(buttonState state, Sprite buttonSprite, float value, bool isSync) {
        for (int i = 0; i != buttons.Count; i++) {                     //amas rame movuxerxot
            for (int a = 0; a != buttons.Count; a++) {
                if (buttons[a].isAvialable == true && buttons[a].myButtonState == state && buttons[a].itemAmount > 0 && value == buttons[a].healthValue) {
                    buttons[a].itemAmount += 1;
                    buttons[a].totalAmountTxt.text = buttons[a].itemAmount.ToString();
                    buttons[a].healthValue = value;
                    return;
                }
            }
            if (buttons[i].isAvialable == false) {
                Color temp = buttons[i].Btn.GetComponent<Image>().color;
                temp = new Color(temp.r, temp.g, temp.b, 1f);
                //Debug.Log(buttons[i].isAvialable);
                buttons[i].isAvialable = true;
                buttons[i].myButtonState = state;
                buttons[i].Btn.GetComponent<Image>().sprite = buttonSprite;
                buttons[i].Btn.GetComponent<Image>().color = temp;
                buttons[i].itemAmount += 1;
                buttons[i].totalAmountTxt.text = buttons[i].itemAmount.ToString();
                buttons[i].healthValue = value;
                return;

            }
        }
    }

    public bool CheckForSameButton(buttonState state, float healthAmount) {
        int index = 0;
        bool finalResult = false;
        for (int i = 0; i != buttons.Count; i++) {
            if (buttons[i].isAvialable == true && buttons[i].myButtonState == state && buttons[i].itemAmount > 0 && healthAmount == buttons[i].healthValue) {
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

    private void OnApplicationQuit() {
        gameData.isFirstRun = false;

        //Position
        gameData.xPos = myCharacter.transform.position.x;
        gameData.yPos = myCharacter.transform.position.y;
        gameData.zPos = myCharacter.transform.position.z;

        //Health
        gameData.currentHealthAmount = myCharacter.currentHealth;

        //Coins
        gameData.coinsAmount = CoinsAmount;

        CheckSave();
    }

    public void HardRestart()
    {
        SceneManager.LoadScene(0);
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
        public bool isAvialable;
        public Text totalAmountTxt;
        public float healthValue;
        public float itemAmount;
        public buttonState myButtonState;
    }


    [SerializeField]
    public List<InventoryData> inventoryList;

    [System.Serializable]
    public enum buttonState
    {
        REDSWORD, GREENSWORD, HEALTH, NOTHING
    }


    [System.Serializable]
    public class Data
    {
        public bool isFirstRun = true;
        public int coinsAmount;
        public float currentHealthAmount;
        public float xPos, yPos, zPos;
        public int lvlID;
        public List<InventoryData> inventoryData;
        //racewereba araaq mnishvneloba
    }


    [ContextMenu("SaveChecker")]
    public void CheckSave() {
        //gameData.coinsAmount = 666;
        //gameData.currentHealthAmount = 111;
        //gameData.lvlID = 17;
        SaveInventory();
        SaveGameInformation();
    }
    private void SaveInventory() {

        for (int i = 0; i < buttons.Count; i++) {
            InventoryData tmp = new InventoryData();
            tmp.state = buttons[i].myButtonState;
            tmp.amount = (int)buttons[i].itemAmount;
            inventoryList.Add(tmp);
        }
        gameData.inventoryData = inventoryList;
    }




    public void SaveGameInformation() {
        XmlSerializer xmlSer = new XmlSerializer(typeof(Data));
        using (StringWriter sw = new StringWriter()) {
            xmlSer.Serialize(sw, gameData);
            // Debug.Log(sw.ToString());
            PlayerPrefs.SetString("Data", sw.ToString());
        }
    }

    [ContextMenu("GetData")]

    public void LoadGameInformation() {
        XmlSerializer xmlSer = new XmlSerializer(typeof(Data));
        string entireText = PlayerPrefs.GetString("Data");
        if (entireText.Length == 0) {
            gameData = new Data();
        }
        else {
            using (var reader = new System.IO.StringReader(entireText)) {
                gameData = xmlSer.Deserialize(reader) as Data;
            }
        }
        SyncInventoryFromDataBase();
        SyncData();

    }

    public void SyncData()
    {
        if(gameData.xPos + gameData.yPos + gameData.zPos != 0)
        {
            //position
            /*
            Vector3 temp = new Vector3(gameData.xPos, gameData.yPos, gameData.zPos);
            myCharacter.transform.position = temp;
            camHolder.transform.position = new Vector3(myCharacter.transform.position.x+camOffset, camHolder.transform.position.y, camHolder.transform.position.z);
            */

            //Health
            myCharacter.currentHealth = gameData.currentHealthAmount;
            UpdateHealth();

            //Coins
            CoinsAmount = gameData.coinsAmount;

        }

    }

    public void SyncInventoryFromDataBase() {
        if (gameData.inventoryData == null)
            return;

        for (int i = 0; i < gameData.inventoryData.Count; i++) {
            for (int k = 0; k < gameData.inventoryData[i].amount; k++) {
                AddToInventory(gameData.inventoryData[i].state, ui_manager.buttonSprites[(int)gameData.inventoryData[i].state], gameData.inventoryData[i].amount, true);
            }
        }
        //AddToInventory();
    }


    [System.Serializable]
    public class InventoryData
    {
        public int amount;
        public buttonState state;
    }
}
