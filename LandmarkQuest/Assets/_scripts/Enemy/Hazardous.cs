using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hazardous : MonoBehaviour
{
    private Animator retryButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Playing("Death");
            Animator playerAnimator = other.GetComponentInChildren<Animator>();
            PlayerController playerController = other.GetComponentInChildren<PlayerController>();
            CapsuleCollider collider = other.GetComponentInChildren<CapsuleCollider>();
            PlayerMotor playerMotor = other.GetComponentInChildren<PlayerMotor>();
            Camera deathCamera = other.GetComponentInChildren<Camera>();
            NavMeshAgent navMeshAgent = other.GetComponentInChildren<NavMeshAgent>();
            retryButton = GameObject.Find("RetryText").GetComponent<Animator>();

            if (playerAnimator != null)
            {
                playerAnimator.SetBool("isDead", true);
            }

            if (playerController != null)
            {
                playerController.enabled = false;
            }

            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }

            if (deathCamera != null)
            {
                deathCamera.enabled = true;
            }

            if (playerMotor != null)
            {
                playerMotor.enabled = false;
            }

            if (collider != null)
            {
                collider.enabled = false;
            }

            if (retryButton != null)
                retryButton.Play("BlinkingLights");
        }
    }
}
