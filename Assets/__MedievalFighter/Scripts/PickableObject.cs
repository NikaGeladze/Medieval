using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{


    public PickType currentType;
    public int value;


    public  PickType ReturnMyType() {
        return currentType;
    }

  
}
public enum PickType
{
    Coin, Gem, Health, FakeHealth, HealthUI, Mana, Item, RedSwordUI, GreenSwordUI
}
