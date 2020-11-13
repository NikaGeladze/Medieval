using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public void ActivateCollison()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
    public void DeactivateCollision()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
