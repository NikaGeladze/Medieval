using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Sprite[] buttonSprites;

    [SerializeField]private GameObject InGamePanel;
    [SerializeField]private GameObject StartPanel;
    [SerializeField]private Text coinsAmountTxt;
    [SerializeField]private Image healthIMG;

    private void Start()
    {
        if(GameManager.Instance.gameData.isFirstRun)
        {
            //StartPanel.SetActive(true);
            GameManager.Instance.StartGame();
        }
        else
        {
            GameManager.Instance.StartGame();
            StartPanel.SetActive(false);
            InGamePanel.SetActive(true);
        }
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
    }

    public void RotationFinished()
    {
        InGamePanel.SetActive(true);
    }    

    public void AddCoin(int coinAmount) {
        coinsAmountTxt.text = coinAmount.ToString();
    }


    public void ChangeHealthUI(float newHealth) {
        float totalHealth = GameManager.Instance.myCharacter.Totalhealth;

        healthIMG.fillAmount = 1-((totalHealth - newHealth) / totalHealth);
    }

 
}
