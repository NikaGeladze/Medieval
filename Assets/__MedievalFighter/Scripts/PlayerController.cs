using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody PlayerRb;

    public float PlayerSpeed = 5f;

    public PlayerAttack playerAttack;


    [HideInInspector]
    public Animator PlayerAnim;


    private void Start() {
        InitValuesAndComponenets();
    }


    //Function to Init Values and components
    private void InitValuesAndComponenets() {
        PlayerRb = GetComponent<Rigidbody>();
        PlayerAnim = GetComponent<Animator>();
    }



    public bool leftBtnIsPressed = false;
    public bool rightBtnIsPressed = false;

    void Update()
    {
        
        if(Input.GetKey(KeyCode.A) || leftBtnIsPressed)
        {
            PlayerRb.velocity = new Vector3(-PlayerSpeed, 0f, 0f);
            PlayerAnim.SetTrigger(Constants.MoveAnimTrig);
            PlayerAnim.ResetTrigger(Constants.IdleAnimTrig);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,90, transform.eulerAngles.z);
        }
        else if(Input.GetKey(KeyCode.D) || rightBtnIsPressed)
        {
            PlayerRb.velocity = new Vector3(PlayerSpeed, 0f, 0f);
            PlayerAnim.SetTrigger(Constants.MoveAnimTrig);
            PlayerAnim.ResetTrigger(Constants.IdleAnimTrig);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
        }      
        else
        {
            PlayerRb.velocity = Vector3.zero;
            PlayerAnim.SetTrigger(Constants.IdleAnimTrig);
            PlayerAnim.ResetTrigger(Constants.MoveAnimTrig);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, transform.eulerAngles.z);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Attack();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
         #if UNITY_EDITOR
            Jump();
         #endif
        }
    }
    public void Jump() {
        PlayerAnim.SetTrigger(Constants.JumpAnimTrig);
        PlayerAnim.ResetTrigger(Constants.IdleAnimTrig);
        PlayerAnim.ResetTrigger(Constants.MoveAnimTrig);
    }
    public void Attack() {
        PlayerAnim.SetTrigger(Constants.AttackAnimTrig);
        PlayerAnim.ResetTrigger(Constants.IdleAnimTrig);
        PlayerAnim.ResetTrigger(Constants.MoveAnimTrig);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Reset"))
        {
            Debug.Log("Reset");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.EnemyAttackTag)) {
            PlayerHasBeenAttacked(other.transform.parent.gameObject.GetComponent<EnemyController>().damage);
        }
        if (other.gameObject.CompareTag(Constants.EnemyTag)) {
            other.gameObject.GetComponent<EnemyController>().Attack();
        }
    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.DashTag))
        {
            other.gameObject.transform.parent.GetComponent<EnemyController>().Dash(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.DashTag))
        {
            other.gameObject.transform.parent.GetComponent<EnemyController>().enemyAnimator.SetTrigger(Constants.IdleAnimTrig);
        }
    }

    public void PlayerHasBeenAttacked(float damage)
    {
        GameManager.Instance.myCharacter.ChangeHealthValue(damage,false);

    }

    public void ActivateCollision()
    {
        playerAttack.ActivateCollison();
    }

    public void DeactivateCollision()
    {
        playerAttack.DeactivateCollision();
    }
   

    public void ResetPlayerPosition(Transform PlayerStartPos)
    {
        transform.position = PlayerStartPos.position;
    }

}
