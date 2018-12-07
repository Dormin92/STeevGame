using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STeevPlayerController : MonoBehaviour
{
    public int HealthPoints = 100;
    public bool Invincible, CooldownCheck, Attacking, Blocking;
    Animator Animator;
    IEnumerator coroutine;
    private AudioSource Oof;

    private void Start()
    {
        Invincible = false;
        Animator = GetComponent<Animator>();
        Oof = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //currently have two colliders on brute's fists, and brute's fists tagged as 'enemy'. Pretty ugly spaghetti. Find cleaner way.
        if (other.gameObject.tag == "Enemy" && !Invincible)
        {
            if (Blocking)
            {
                GetHit(5);
            }
            else
            {
                GetHit(10);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Dodge"))
        {
            DodgeRoll();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack_1();
        }

        Blocking = Input.GetButton("Block");
    }

    //function activates trigger to play the animation for player taking damage. Also activates invincibility frames.
    void GetHit(int damage)
    {
        HealthPoints -= damage;

        //activate trigger for animation of player taking damage
        Animator.SetTrigger("IsHit");
        Oof.Play();

        //player becomes invincible to further damage for a short amount of time after they take damage
        coroutine = InvincibilityFrames(1f);
        StartCoroutine(coroutine);
    }

    //function activates trigger for playing the dodge roll animation and invincibility frames, but not consecutively. There is a cooldown between rolls.
    void DodgeRoll()
    {
        if (!CooldownCheck)
        {
            Animator.SetTrigger("Roll");
            coroutine = InvincibilityFrames(0.5f, 0.3f);
            StartCoroutine(coroutine);
        }
            
    }

    void Attack_1()
    {
        if (!Attacking)
        {
            Animator.SetTrigger("Attack");
            coroutine = AttackFrames(0.75f);
            StartCoroutine(coroutine);
        }
        
    }

    //triggers invincibilityframes so player cannot be further damaged
    IEnumerator InvincibilityFrames(float time)
    {
        Invincible = true;
        yield return new WaitForSeconds(time);
        Invincible = false;
    }

    //triggers invincibilityframes but has a cooldown afterwards
    IEnumerator InvincibilityFrames(float time, float cooldownTime)
    {
        Invincible = true;
        CooldownCheck = true;
        yield return new WaitForSeconds(time);
        Invincible = false;
        yield return new WaitForSeconds(cooldownTime);
        CooldownCheck = false;
    }

    IEnumerator AttackFrames(float time)
    {
        Attacking = true;
        yield return new WaitForSeconds(time);
        Attacking = false;
    }

}
