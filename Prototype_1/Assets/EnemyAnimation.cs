﻿using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {

	public float deadZone = 5f;					// The number of degrees for which the rotation isn't controlled by Mecanim.
	
	public float speedDampTime = 0.1f;				// Damping time for the Speed parameter.
	public float angularSpeedDampTime = 0.7f;		// Damping time for the AngularSpeed parameter
	public float angleResponseTime = 0.6f;			// Response time for turning an angle into angularSpeed.

	private UnityEngine.AI.NavMeshAgent nav;					// Reference to the nav mesh agent.
	private Animator anim;						// Reference to the Animator.

	public Transform target;					// Destination of the agent.

    public bool isDead = false;
    public bool isAttacking = false;
    public GameObject[] fists;

	void Awake ()
	{
		// Setting up the references.
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		anim = GetComponent<Animator>();
		
		// Making sure the rotation is controlled by Mecanim.
		nav.updateRotation = false;
		
		// Set the weights for the shooting and gun layers to 1.
		anim.SetLayerWeight(1, 1f);
		anim.SetLayerWeight(2, 1f);
		
		// We need to convert the angle for the deadzone from degrees to radians.
		deadZone *= Mathf.Deg2Rad;
	}
	
	
	void Update () 
	{
		if(target != null)
		{
            if (!isDead)
            {
                //TODO: Set the destination of the NavMesh agent to the position of the target.
                nav.SetDestination(target.position);

                // Create the parameters to pass to the helper function.
                float speed = 0;
                float angularSpeed = 0;
                DetermineAnimParameters(out speed, out angularSpeed);

                //Set the values of the parameters to the animator.
                anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
                //anim.SetFloat("Angular Speed", angularSpeed, angularSpeedDampTime, Time.deltaTime);
            }
		}
	}
	
	
	void OnAnimatorMove()
	{
		// Set the NavMeshAgent's velocity to the change in position since the last frame, by the time it took for the last frame.
		nav.velocity = anim.deltaPosition / Time.deltaTime;
		
		// The gameobject's rotation is driven by the animation's rotation.
		transform.rotation = anim.rootRotation;
	}
	
	
	void DetermineAnimParameters (out float speed, out float angularSpeed)
	{
		speed = Vector3.Magnitude(Vector3.Project(nav.desiredVelocity, transform.forward));

		float angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

        transform.Rotate(Vector3.up, angle);
		
		angularSpeed = (angle * Mathf.Deg2Rad) / angleResponseTime;
	}
	
	
	float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
	{
	
		float angle = 0;


		if(toVector == Vector3.zero)
			return 0f;

        angle = Vector3.SignedAngle(fromVector, toVector, Vector3.up);
        return angle;

	}
	
	public void setTarget(Transform target, float speed = 2.0f)
	{
		this.target = target;
		nav.speed = speed;
	}
	
	public void Stop()
	{
		nav.speed = 0f;
	}

    public void Attack()
    {
        if(!isDead)
            StartCoroutine("AttackAnim");
    }

    public void DeathAnimation()
    {
        if (!isDead && !isAttacking)
        {
            anim.SetTrigger("Death");
            isDead = true;
        }
    }

    private IEnumerator AttackAnim()
    {
        isAttacking = true;
        fists[0].GetComponent<Collider>().enabled = true;
        fists[1].GetComponent<Collider>().enabled = true;
        anim.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(1.15f);
        fists[0].GetComponent<Collider>().enabled = false;
        fists[1].GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }
}
