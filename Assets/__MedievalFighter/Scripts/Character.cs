using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Totalhealth;

    [HideInInspector]
    public float currentHealth;


    private void Start() {
        currentHealth = Totalhealth;
    }
    public GameObject[] mySwords; //0 blue,1 red, 2 green
    

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PickableObject>()!=null) {
            PickableObject temp = other.GetComponent<PickableObject>();
            PickType type = temp.ReturnMyType();
            switch (type) {
                case PickType.Coin:
                    GameManager.Instance.CoinsAmount=temp.value;
                    Destroy(other.gameObject);
                    break;
                case PickType.Gem:
                    GameManager.Instance.CoinsAmount = temp.value;
                    Destroy(other.gameObject);
                    break;
                case PickType.RedSwordUI:
                    GameManager.Instance.AddToInventory(GameManager.buttonState.REDSWORD,GameManager.Instance.ui_manager.buttonSprites[0],temp.value);
                    Destroy(other.gameObject);
                    break;
                case PickType.GreenSwordUI:
                    GameManager.Instance.AddToInventory(GameManager.buttonState.GREENSWORD, GameManager.Instance.ui_manager.buttonSprites[1], temp.value);
                    Destroy(other.gameObject);
                    break;
                case PickType.HealthUI:
                    GameManager.Instance.AddToInventory(GameManager.buttonState.HEALTH, GameManager.Instance.ui_manager.buttonSprites[2], temp.value);
                    Destroy(other.gameObject);
                    break;
                case PickType.FakeHealth:
                    ChangeHealthValue(temp.value, false);
                    Destroy(other.gameObject);
                    break;
                case PickType.Health:
                    ChangeHealthValue(temp.value, true);
                    Destroy(other.gameObject);
                    break;
            }
        }
    }

    public void ChangeHealthValue(float amount,bool wantToIncrease) {
        currentHealth = wantToIncrease ? currentHealth + amount : currentHealth - amount;
        GameManager.Instance.gameData.currentHealthAmount = currentHealth;
        GameManager.Instance.UpdateHealth();
    }

    public void ChangeSword(bool red)
    {
        if(red)
        {
            mySwords[1].SetActive(true);
            mySwords[0].SetActive(false);
            mySwords[2].SetActive(false);
        }
        else
        {
            mySwords[2].SetActive(true);
            mySwords[0].SetActive(false);
            mySwords[1].SetActive(false);
        }
    }
}
