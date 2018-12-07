using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for controlling behaviour and animations of the brute
public class BruteController : MonoBehaviour
{
    private Transform playerTransform;
    Animator Animator;
    public float AttackCooldown = 2f;
    private bool AttackCooldownCheck;
    private IEnumerator coroutine;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Animator = GetComponent<Animator>();

    }
    private void Update()
    {
        //if player is closer than 8 units, aggro the brute
        if (Vector3.Distance(transform.position, playerTransform.position) < 8f)
        {
            //brute directs attention to player. Need to find better way to do this
            transform.LookAt(playerTransform);
            Animator.SetBool("Aggro", true);

            //once brute gets close, deactivate parameter for run animation and trigger parameter for attack animation
            if(Vector3.Distance(transform.position, playerTransform.position) < 3.5f)
            {
                Animator.SetBool("Aggro", false);
                if (!AttackCooldownCheck)
                {
                    Animator.SetTrigger("WithinAttackRange");
                    coroutine = AttackCooldownFrames(AttackCooldown);
                    StartCoroutine(coroutine);
                }
            }
        }
    }

    IEnumerator AttackCooldownFrames(float AtkCD)
    {
        AttackCooldownCheck = true;
        yield return new WaitForSeconds(AtkCD);
        AttackCooldownCheck = false;
    }
}
