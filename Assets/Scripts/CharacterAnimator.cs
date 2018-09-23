using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    const float locoAnimSmoothTime = .1f; // Time allowed between animations for smoother transitions

    Animator animator;
    NavMeshAgent agent;

    // Initializes player's agent and animator
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
	}
	
	// Calculate player's speedPercent (curSpeed / maxSpeed)
	void Update () {
        float newSpeedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", newSpeedPercent, locoAnimSmoothTime, Time.deltaTime);
	}
}
