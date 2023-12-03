using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardInteraction : Interactable
{
    public GameObject pauseObject;
    public float delay = 3f;

    public override void Interact()
    {
        base.Interact();

        StartCoroutine(PauseObject());
    }

    IEnumerator PauseObject()
    {
        ObjectMovementCircular circularMovement = pauseObject.GetComponent<ObjectMovementCircular>();

        if (circularMovement != null)
        {
            circularMovement.enabled = false;
           
            yield return new WaitForSeconds(delay);

            circularMovement.enabled = true;
        }
    }
}
