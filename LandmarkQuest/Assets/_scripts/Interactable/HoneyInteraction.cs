using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoneyInteraction : Itemize
{
    public GameObject fazBear;

    public override void Interact()
    {
        base.Interact();
        fazBear.GetComponent<NavMeshAgent>().enabled = true;
        fazBear.GetComponent<ObjectMovement>().enabled = false;
        fazBear.GetComponent<EnemyController>().enabled = true;
    }

}
