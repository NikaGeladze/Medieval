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
        StartPanel.SetActive(true);
    }


    public void RotationFinished()
    {
        InGamePanel.SetActive(true);
        StartPanel.SetActive(false);
    }

    public void AddCoin(int coinAmount) {
        coinsAmountTxt.text = coinAmount.ToString();
    }


    public void ChangeHealthUI(float newHealth) {
        float totalHealth = GameManager.Instance.myCharacter.Totalhealth;

        healthIMG.fillAmount = 1-((totalHealth - newHealth) / totalHealth);
    }

 
}
