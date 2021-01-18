using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public Transform arrowPos;
    public float arrowSpeed = 3.5f;
    public float arrowDamage = 100f;

    public float arrowVanishTime = 2.5f;
    public float shootDelay = 2f;

    public GameObject myVisuals;
    public GameObject ragdoll;
    public float ragdollVanishTime = 3;

    public List<GameObject> arrows;
    private Animator archerAnimator;
    private bool hasPlayerInShootZone = false;
    private bool isActive = true;

    private void Start()
    {
        archerAnimator = GetComponent<Animator>();
        archerAnimator.SetBool(Constants.ArcherIdleBool, true);
    }


    public void PlayerDetected()
    {
        hasPlayerInShootZone = true;
        StartCoroutine(Shoot());
    }

    public void PlayerLeftDetectionZone()
    {
        hasPlayerInShootZone = false;
        archerAnimator.SetBool(Constants.ArcherIdleBool,true);
        StopCoroutine(SpawnArrow());
    }

    public IEnumerator Shoot()
    {
        while (hasPlayerInShootZone && GameManager.Instance.gameActive)
        {
            archerAnimator.SetBool(Constants.ArcherIdleBool, false);
            archerAnimator.SetTrigger(Constants.ArcherReadyTrig);

            yield return new WaitForSeconds(shootDelay);

            if (hasPlayerInShootZone)
            {
                archerAnimator.SetTrigger(Constants.ArcherShootTrig);
            }
            else
            {
                archerAnimator.SetBool(Constants.ArcherIdleBool, true);
                break;
            }
        }
        if(!GameManager.Instance.gameActive)
        {
            archerAnimator.SetBool(Constants.ArcherIdleBool, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.PlayerAttackTag))
        {
            Death();
        }
    }

    public void Death()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        isActive = false;
        myVisuals.SetActive(false);
        ragdoll.SetActive(true);
        ragdoll.transform.SetParent(transform.parent);
        StartCoroutine(DestroyRagdoll());
    }

    public IEnumerator DestroyRagdoll()
    {
        yield return new WaitForSeconds(ragdollVanishTime);

        if (ragdoll != null)
        {
            Destroy(ragdoll);
        }
        gameObject.SetActive(false);
    }

    public IEnumerator SpawnArrow()
    {
        if (!isActive || !GameManager.Instance.gameActive) yield break;

        arrows.Add(Instantiate(GameManager.Instance.arrow, arrowPos));
        arrows[arrows.Count - 1].GetComponent<Rigidbody>().AddForce(new Vector3(-arrowSpeed, 0, 0), ForceMode.VelocityChange);

        yield return new WaitForSeconds(arrowVanishTime);

        if(arrows[0] != null)
        {
            Destroy(arrows[0]);
            arrows.RemoveAt(0);
        }
    }
}
