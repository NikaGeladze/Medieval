using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float damage;
    public BoxCollider enemyAttackCollider;
    public Animator enemyAnimator;
    public enemyType Enemytype;

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
        Destroy(gameObject);
    }

    public enum enemyType
    { 
        IDLE,DASH
    }

}
