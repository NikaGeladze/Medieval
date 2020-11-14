using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public BoxCollider enemyAttackCollider;

    public Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator.SetTrigger(Constants.IdleAnimTrig);
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

    public void Death()
    {
        Destroy(gameObject);
    }

}
