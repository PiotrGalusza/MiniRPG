using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

    Transform curTarget;    // Stores player's current target
    NavMeshAgent agent;     // Reference to player's agent

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}

    private void Update()
    {
        if(curTarget) //curTarget != null
        {
            agent.SetDestination(curTarget.position);
            FaceTarget();
        }
    }

    // Simple implementation  - moves the player to wherever you click, if it's valid
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
	
    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;

        curTarget = newTarget.interactTransf;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        curTarget = null;
    }

    // Calculates direction towards out current Target, calculates how we should rotate towards it (without changing y),
    // and then smoothly interpolate towards our Target's direction
    void FaceTarget()
    {
        Vector3 direction = (curTarget.position - transform.position).normalized;
        // New Vector passed into rotation prevents up-down changes
        Quaternion curRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
                                                                            // Dictates tracking rotation speed
        transform.rotation = Quaternion.Slerp(transform.rotation, curRotation, Time.deltaTime * 5f);
    }

}
