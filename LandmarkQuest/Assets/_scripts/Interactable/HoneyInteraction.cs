using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq; // Import the System.Linq namespace

public class HoneyInteraction : Itemize
{
    public GameObject[] fazBears;

    public override void Interact()
    {
        base.Interact();

        foreach (GameObject fazBear in fazBears)
        {
            NavMeshAgent navMeshAgent = fazBear.GetComponent<NavMeshAgent>();
            ObjectMovement objectMovement = fazBear.GetComponent<ObjectMovement>();
            EnemyController enemyController = fazBear.GetComponent<EnemyController>();
            AudioMaker soundMaker = fazBear.GetComponent<AudioMaker>();
            Animator anim = fazBear.GetComponent<Animator>();

            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = true;
            }

            if (objectMovement != null)
            {
                objectMovement.enabled = false;
            }

            if (enemyController != null)
            {
                enemyController.enabled = true;
            }

            if (soundMaker != null)
            {
                soundMaker.enabled = true;
            }

            if (anim != null)
            {
                if (anim.runtimeAnimatorController != null &&
                    anim.runtimeAnimatorController.animationClips.Any(clip => clip.name == "WalkBear"))
                {
                    anim.Play("WalkBear");
                }
            }
        }
    }
}
