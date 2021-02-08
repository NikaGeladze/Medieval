using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public float damage;
    public float ragdollDestroyDelay = 3f;
    public float ragdollDissolveSpeed = 4f;
    public BoxCollider enemyAttackCollider;
    public Animator enemyAnimator;
    public enemyType Enemytype;

    public GameObject regdoll;
    public Rigidbody middleSpine;


    private Rigidbody rb;

    private bool isAttacking = false;

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
        isAttacking = true;
        enemyAnimator.ResetTrigger(Constants.DashTag);
        enemyAnimator.ResetTrigger(Constants.IdleAnimTrig);
        enemyAnimator.SetTrigger(Constants.AttackAnimTrig);
    }


    public void ActivateCollision()
    {
        enemyAttackCollider.enabled = true;
    }

    public void DeactivateCollision()
    {
        enemyAttackCollider.enabled= false;
        isAttacking = false;
    }

    public void Dash(Transform playerPos)
    {
        if (Enemytype == enemyType.DASH && ! isAttacking)
        {
            enemyAnimator.SetTrigger(Constants.DashTag);

            rb.velocity = new Vector3(playerPos.position.x - transform.position.x, 0, 0);
        }
    }


    public void Death()
    {

        //Destroy(gameObject);
        StartCoroutine(SpawnRagdoll());
    }

    public IEnumerator SpawnRagdoll()
    {
        regdoll.SetActive(true);
        regdoll.transform.SetParent(null);
        middleSpine.AddForce(new Vector3(Random.Range(0, 3), Random.Range(0, 3f), Random.Range(0, 3f)), ForceMode.Impulse);
        ActivateRagdollShader();
        DestroyMyself();
        yield return new WaitForSeconds(ragdollDestroyDelay);
        Destroy(regdoll);
    }

    public void ActivateRagdollShader()
    {
        Material ragdollMat = regdoll.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
        ragdollMat.DOFloat(1.1f, Constants.ColorFillProp, ragdollDissolveSpeed);
    }

    public void DestroyMyself()
    {
        foreach(Transform comp in gameObject.transform)
        {
            Destroy(comp.gameObject);
        }
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        enemyAnimator.enabled = false;
        rb.isKinematic = true;
    }

    public enum enemyType
    { 
        IDLE,DASH
    }

}
