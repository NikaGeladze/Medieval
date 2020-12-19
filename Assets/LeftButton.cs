using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class LeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData) {
        GameManager.Instance.playerController.leftBtnIsPressed = true;
        GameManager.Instance.playerController.rightBtnIsPressed = false;

    }

    public void OnPointerUp(PointerEventData eventData) {
        GameManager.Instance.playerController.leftBtnIsPressed = false;

    }


}
