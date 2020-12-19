using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData) {
        GameManager.Instance.playerController.rightBtnIsPressed = true;
        GameManager.Instance.playerController.leftBtnIsPressed = false;
    }

    public void OnPointerUp(PointerEventData eventData) {
        GameManager.Instance.playerController.rightBtnIsPressed = false;

    }


}
