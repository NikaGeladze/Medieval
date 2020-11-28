﻿using System;
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
                    Debug.Log("Avige Coin");
                    GameManager.Instance.CoinsAmount=temp.value;
                    Destroy(other.gameObject);
                    break;
                case PickType.Gem:
                    Debug.Log("Avige Gem");
                    GameManager.Instance.CoinsAmount = temp.value;
                    Destroy(other.gameObject);
                    break;
                case PickType.BlueSword:
                    mySwords[0].SetActive(true);
                    mySwords[1].SetActive(false);
                    mySwords[2].SetActive(false);
                    Destroy(other.gameObject);
                    Debug.Log("Blue");
                    break;
                case PickType.RedSword:
                    mySwords[1].SetActive(true);
                    mySwords[0].SetActive(false);
                    mySwords[2].SetActive(false);
                    Destroy(other.gameObject);
                    Debug.Log("Red");
                    break;
                case PickType.GreenSword:
                    mySwords[2].SetActive(true);
                    mySwords[0].SetActive(false);
                    mySwords[1].SetActive(false);
                    Destroy(other.gameObject);
                    Debug.Log("Green");
                    break;
                case PickType.Health:
                    currentHealth += temp.value;
                    GameManager.Instance.UpdateHealth();
                    Debug.Log("avige guli"+temp.value);
                    Destroy(other.gameObject);
                    break;
                case PickType.fakeHealth:
                    currentHealth -= temp.value;
                    GameManager.Instance.UpdateHealth();
                    Debug.Log("avige cudi guli" + temp.value);
                    Destroy(other.gameObject);
                    break;


            }
        }
    }

    internal void TakeDamage(float damage) {
        currentHealth -= damage;
        GameManager.Instance.UpdateHealth();
    }
}
