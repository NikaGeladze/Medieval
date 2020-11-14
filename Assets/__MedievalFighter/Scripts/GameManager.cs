using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA;

public class GameManager : MonoBehaviour
{
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
    [HideInInspector]
    public UiManager ui_manager;

    private int _coinsAmount = 0;

    public Character myCharacter;
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


}
