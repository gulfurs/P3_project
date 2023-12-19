using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationManager : MonoBehaviour
{
    public Animator animateTree;
    public GameObject tutorialParticle;
    public GameObject toggleObject;
    public NavMeshAgent agent;
    public float speedo;

    void Update()
    {
        if (agent != null)
        speedo = agent.velocity.magnitude / agent.speed;
    }

    void AnimateChop() {
        if (animateTree != null)
        {
            animateTree.SetBool("isTimber", true);
        }

        if (tutorialParticle != null)
            Destroy(tutorialParticle);
    }

    void ChopFinished() {
        PlayerManager.PlayerInstance.player.SetActive(true);
        gameObject.SetActive(false);
    }

    void ToggleObject()
    {
        if (toggleObject != null)
        {
            toggleObject.SetActive(!toggleObject.activeSelf);
        }
    }

    void Climbing()
    {
        PlayerManager.PlayerInstance.player.transform.position = transform.position;
        PlayerManager.PlayerInstance.player.SetActive(true);
        gameObject.SetActive(false);
    }

    void PlayFootsteps()
    {
        if (speedo > 0f)
        {
            AudioManager.instance.Playing("FootSteps");
        }
        else
        {
            AudioManager.instance.Stop("FootSteps");    
        }
    }
}
