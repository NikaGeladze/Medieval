using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public EnemyAttack enemyAttack;

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
        enemyAttack.ActivateCollison();
    }

    public void DeactivateCollision()
    {
        enemyAttack.DeactivateCollision();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

}
