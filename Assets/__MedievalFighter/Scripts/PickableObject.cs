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
    Coin, Gem, Health, Mana, Item, BlueSword,RedSword,GreenSword
}
