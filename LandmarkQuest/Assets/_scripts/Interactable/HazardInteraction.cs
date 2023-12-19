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
            Renderer renderer = GetComponentInChildren<Renderer>();

            if (renderer != null)
            {
                Material material = renderer.material;

                if (material != null)
                {
                    material.EnableKeyword("_EMISSION");

                    circularMovement.enabled = false;

                    yield return new WaitForSeconds(delay);

                    material = renderer.material;

                    if (material != null)
                    {
                        material.DisableKeyword("_EMISSION");
                    }

                    circularMovement.enabled = true;
                }
            }
        }
    }

}
