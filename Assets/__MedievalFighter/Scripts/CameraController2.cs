using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController2 : MonoBehaviour
{
    public Transform target;

    private GameObject mainCam;

    public float lerpSpeed;
    public bool followX, followY, followZ;
    public bool canFollow = true;
    public float FirstYRotation = 8;
    public float FirstRotationDuration = 1.5f;

    [Space(10)]
    [Header("Finish Properties")]
    public Vector3 finishCamRotation;
    public Transform finishCamPos;
    public float finishCamRotationDuration = 1.5f;

    private Vector3 offSet;
    private Vector3 firstPos;
    private Vector3 newPos;

    private float startCamY;
    private bool resetYPos = false;
    private Vector3 startOffset;
    private bool isAlreadyActivated = false;

    private void Start() {
        offSet = target.position - transform.position;
        firstPos = target.position - offSet;
        newPos = transform.position;
        mainCam = transform.GetChild(0).gameObject;
        startOffset = offSet;
        if(!GameManager.Instance.gameData.isFirstRun)
        {
            StartRotation(true);
        }
    }
    private void LateUpdate()
    {
        if (canFollow)
        {
            FollowTarget();
        }
    }

    public void StartRotation(bool instantly)
    {
        float yRotation = FirstYRotation;
        Vector3 rotationAngle = new Vector3(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);
        if (instantly)
        {
            transform.rotation = Quaternion.Euler(rotationAngle);
            GameManager.Instance.ui_manager.RotationFinished();
        }
        else
        {
            transform.DORotate(rotationAngle, FirstRotationDuration).OnComplete(() => {
                GameManager.Instance.ui_manager.RotationFinished();
            });
        }       
    }

    private void FollowTarget()
    {
        FollowTargetX();
        FollowTargetY();
        FollowTargetZ();
        transform.position = newPos;
    }
    private void FollowTargetX()
    {
        if (followX)
        {
            newPos.x = Mathf.Lerp(transform.position.x, target.position.x - offSet.x, lerpSpeed);
        }
        else
        {
            newPos.x = Mathf.Lerp(newPos.x, firstPos.x, lerpSpeed);
        }
    }
    private void FollowTargetY()
    {
        if (followY)
        {
            newPos.y = Mathf.Lerp(transform.position.y, target.position.y - offSet.y, lerpSpeed);
        }
        else
        {
            newPos.y = Mathf.Lerp(newPos.y, firstPos.y, lerpSpeed);
        }
    }
    private void FollowTargetZ()
    {
        if (followZ)
        {
            newPos.z = Mathf.Lerp(transform.position.z, target.position.z - offSet.z, lerpSpeed);
        }
        else
        {
            newPos.z = Mathf.Lerp(newPos.z, firstPos.z, lerpSpeed);
        }
    }

    public void Finish()
    {
        if (!isAlreadyActivated)
        {
            Camera.main.transform.DOMove(finishCamPos.position, finishCamRotationDuration);
            Camera.main.transform.DORotate(finishCamRotation, finishCamRotationDuration);
            isAlreadyActivated = true;
        }
    }

    public void ZoomOut(int count)
    {
        //mainCam.transform.DOLocalMove(new Vector3(mainCam.transform.localPosition.x, mainCam.transform.localPosition.y + 1f, mainCam.transform.localPosition.z - 0.2f), 1f);
        //GameManager.Instance.ChangeSpeed(GameManager.Instance.speedIncrementValue);
        for (int i = 0; i < count; i++)
        {
            offSet -= new Vector3(0, 0.3f, -0.2f);
        }
    }
    public void ZoomIn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (offSet != startOffset)
            {
                //mainCam.transform.DOLocalMove(new Vector3(mainCam.transform.localPosition.x, mainCam.transform.localPosition.y - 0.2f, mainCam.transform.localPosition.z +0.1f), 1f);
                //GameManager.Instance.ChangeSpeed(-GameManager.Instance.speedIncrementValue);
                offSet += new Vector3(0, 0.3f, -0.2f);
            }
        }
    }

    [ContextMenu("testCam")]
    public void CamOnGameWon()
    {
        canFollow = false;
      // mainCam.transform.DOLookAt(target.transform.localPosition, 2f);
        //mainCam.transform.DOLocalRotate(new Vector3(12.485f, 24.635f, 360 - 5.089f), 0.2f);
        //mainCam.transform.DOLocalMove(new Vector3(mainCam.transform.localPosition.x - 10, mainCam.transform.localPosition.y - 3f, mainCam.transform.localPosition.z - 3f), 0.5f);


    }
}
