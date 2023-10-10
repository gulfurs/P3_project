using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    const float locomationAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float spd = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedFrequency", spd, 1f, Time.deltaTime);
    }
}