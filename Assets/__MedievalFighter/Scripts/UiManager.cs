using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text coinsAmountTxt;
    public Image healthIMG;
    public Sprite[] buttonSprites;

    public void AddCoin(int coinAmount) {
        coinsAmountTxt.text = coinAmount.ToString();
    }


    public void ChangeHealthUI(float newHealth) {
        float totalHealth = GameManager.Instance.myCharacter.Totalhealth;

        healthIMG.fillAmount = 1-((totalHealth - newHealth) / totalHealth);
    }

    private void Start() {
       // healthIMG = 17;
    }
}
