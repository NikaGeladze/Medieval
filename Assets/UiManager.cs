using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text coinsAmountTxt;



    public void AddCoin(int coinAmount) {
        coinsAmountTxt.text = coinAmount.ToString();
    }

}
