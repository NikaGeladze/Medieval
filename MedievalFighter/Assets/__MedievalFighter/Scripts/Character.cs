using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PickableObject>()!=null) {
            PickableObject temp = other.GetComponent<PickableObject>();
            PickType type = temp.ReturnMyType();
            switch (type) {
                case PickType.Coin:
                    Debug.Log("Avige Coin");
                    Destroy(other.gameObject);
                    break;
                case PickType.Gem:
                    Debug.Log("Avige Gem");
                    Destroy(other.gameObject);
                    break;
                case PickType.Health:
                    break;
                case PickType.Mana:
                    break;
                case PickType.Weapon:
                    break;
                case PickType.Item:
                    break;
                default:
                    break;
            }
        }
    }
}
