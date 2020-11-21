using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float damage;
    public BoxCollider enemyAttackCollider;
    public Animator enemyAnimator;
    public enemyType Enemytype;

    public GameObject regdoll;
    public Rigidbody middleSpine;


    private Rigidbody rb;

    private void Start()
    {
        enemyAnimator.SetTrigger(Constants.IdleAnimTrig);
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Constants.PlayerAttackTag))
        {
            Death();
        }
    }

    public void Attack()
    {
        enemyAnimator.SetTrigger(Constants.AttackAnimTrig);
    }


    public void ActivateCollision()
    {
        enemyAttackCollider.enabled = true;
    }

    public void DeactivateCollision()
    {
        enemyAttackCollider.enabled= false;
    }

    public void Dash(Transform playerPos)
    {
        if (Enemytype == enemyType.DASH)
        {
            enemyAnimator.SetTrigger(Constants.DashTag);

            rb.velocity = new Vector3(playerPos.position.x - transform.position.x,0,0);
        }
    }


    public void Death()
    {
        //Destroy(gameObject);
        regdoll.SetActive(true);
        regdoll.transform.SetParent(null);
        middleSpine.AddForce(new Vector3(Random.Range(0,3), Random.Range(0, 3f), Random.Range(0, 3f)), ForceMode.Impulse);
        Destroy(gameObject);
    }

    public enum enemyType
    { 
        IDLE,DASH
    }

}
