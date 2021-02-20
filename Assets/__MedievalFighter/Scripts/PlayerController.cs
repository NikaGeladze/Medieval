using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody PlayerRb;

    public float PlayerSpeed = 5f;
    public float PlayerFinishSpeed = 3.5f;
    public float PlayerShieldedSpeed = 2.5f;
    public float shieldDuration = 3.5f;
    public float shieldCoolDown = 1.5f;

    public float forceToArrow;

    public PlayerAttack playerAttack;
    public GameObject myVisuals;
    public GameObject myRagdoll;


    [HideInInspector]
    public Animator PlayerAnim;

    private bool isShielded = false;
    private float PlayerInitSpeed;
    private bool isAttacking=false;
    private void Start() {
        InitValuesAndComponenets();
    }


    //Function to Init Values and components
    private void InitValuesAndComponenets() {
        PlayerRb = GetComponent<Rigidbody>();
        PlayerAnim = GetComponent<Animator>();
        PlayerInitSpeed = PlayerSpeed;
    }


    public float jumpStrength;
    public bool leftBtnIsPressed = false;
    public bool rightBtnIsPressed = false;
    public bool isGrounded;

    void Update()
    {

        if (GameManager.Instance.playerHasreachedTheFinish)
        {
            PlayerRb.velocity = new Vector3(PlayerSpeed, 0f, 0f);
            PlayerAnim.SetTrigger(Constants.RunAnimTrig);
            PlayerAnim.ResetTrigger(Constants.IdleAnimTrig);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
            return;
        }

        if (Input.GetKey(KeyCode.A) || leftBtnIsPressed)
        {
            PlayerRb.velocity = new Vector3(-PlayerSpeed, 0f, 0f);
            PlayerAnim.SetTrigger(Constants.RunAnimTrig);
            PlayerAnim.ResetTrigger(Constants.IdleAnimTrig);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,90, transform.eulerAngles.z);
        }
        else if(Input.GetKey(KeyCode.D) || rightBtnIsPressed)
        {
            PlayerRb.velocity = new Vector3(PlayerSpeed, 0f, 0f);
            PlayerAnim.SetTrigger(Constants.RunAnimTrig);
            PlayerAnim.ResetTrigger(Constants.IdleAnimTrig);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
        }      
        else
        {
            PlayerRb.velocity = Vector3.zero;
            PlayerAnim.SetTrigger(Constants.IdleAnimTrig);
            PlayerAnim.ResetTrigger(Constants.RunAnimTrig);
            ChangeShieldState(false);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y, transform.eulerAngles.z);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Attack();
        }
        if(Input.GetKey(KeyCode.Mouse1))
        {
            if (!isShielded)
            {
                ChangeShieldState(true);
            }
        }
        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            ChangeShieldState(false);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
         #if UNITY_EDITOR
            Jump();
         #endif
        }
    }
    public void Jump() {
        if (isGrounded) {
            PlayerAnim.SetTrigger(Constants.JumpAnimTrig);
            PlayerRb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
    }
    public void Attack() {
        if (!isAttacking) {
            PlayerAnim.SetTrigger(Constants.AttackAnimTrig);
            isAttacking = true;
        }
    }
    private void OnCollisionExit(Collision collision) {
        if (collision.transform.CompareTag(Constants.Groundtag)) {
            isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Constants.Groundtag)) {
            isGrounded = true;
        }
        if(collision.gameObject.CompareTag("Reset"))
        {
            Debug.Log("Reset");
        }
        if (collision.gameObject.CompareTag(Constants.ArrowTag))
        {
            if (!isShielded)
            {
                GameManager.Instance.myCharacter.ChangeHealthValue(collision.gameObject.transform.parent.
                    parent.GetComponent<ArcherController>().arrowDamage, false);
                collision.gameObject.SetActive(false);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(forceToArrow, 0, 0), ForceMode.VelocityChange);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.EnemyAttackTag))
        {
            PlayerHasBeenAttacked(other.transform.parent.gameObject.GetComponent<EnemyController>().damage);
        }
        if (other.gameObject.CompareTag(Constants.EnemyTag))
        {
            Debug.Log("Enter");
            other.gameObject.GetComponent<EnemyController>().Attack();
        }
        if (other.gameObject.CompareTag(Constants.SpikeTag))
        {
            other.gameObject.GetComponent<Animator>().SetTrigger(Constants.SpikeAttackTrigger);
        }
        if (other.gameObject.CompareTag(Constants.ActualSpikeTag))
        {
            Death();
        }
        if (other.gameObject.CompareTag(Constants.ArcherDetectionTag))
        {
            other.gameObject.transform.parent.GetComponent<ArcherController>().PlayerDetected();
        }
        if(other.gameObject.CompareTag(Constants.FinishStartTag))
        {
            GameManager.Instance.playerHasreachedTheFinish = true;
            other.gameObject.GetComponent<Animator>().SetTrigger(Constants.OpenAnimTrig);
            FinishStart();
        }
        if(other.gameObject.CompareTag(Constants.FinishEndTag))
        {
            other.transform.parent.gameObject.GetComponent<Animator>().SetTrigger(Constants.CloseAnimTrig);
            GameManager.Instance.FinishEnd();
        }
    }

    public void FinishStart()
    {
        GameManager.Instance.FinishStart();
    }

    public void Death()
    {
        ActivateRagdoll();
        GameManager.Instance.GameOver();
        GameManager.Instance.myCharacter.DeactivateAllSwords();
    }

    public void ActivateShield()
    {
        PlayerAnim.SetBool(Constants.isShieldedBool, true);
        isShielded = true;
        PlayerSpeed = PlayerShieldedSpeed;
    }

    public void DeactivateShield()
    {
        PlayerAnim.SetBool(Constants.isShieldedBool, false);
        PlayerAnim.ResetTrigger(Constants.RunAnimTrig);
        PlayerAnim.SetTrigger(Constants.IdleAnimTrig);
        isShielded = false;
        PlayerSpeed = PlayerInitSpeed;
    }

    public void ChangeShieldState(bool wantToActivate)
    {
        if(wantToActivate && PlayerRb.velocity != Vector3.zero)
        {
            ActivateShield();
        }
        else
        {
            DeactivateShield();
        }
    }

    public void ActivateRagdoll()
    {
        myVisuals.SetActive(false);
        myRagdoll.SetActive(true);
        myRagdoll.transform.SetParent(null);
        gameObject.SetActive(false);
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
        if (other.gameObject.CompareTag(Constants.ArcherDetectionTag))
        {
            other.gameObject.transform.parent.GetComponent<ArcherController>().PlayerLeftDetectionZone();
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
        isAttacking = false;
    }
   

    public void ResetPlayerPosition(Transform PlayerStartPos)
    {
        transform.position = PlayerStartPos.position;
    }

}
