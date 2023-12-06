using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    bool isFocus = false;
    Transform player;
    public Transform interactionTransform;
    public string interactSound = "Interact1";

    bool hasInteracted = false;

    public virtual void Interact()
    {
        //This method will be overwritten (virtual void)
    }

    void Update()
    {
        //Make player close in and run Interact()
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                AudioManager.instance.Playing(interactSound);
                hasInteracted = true;
            }
        }
    }

    //If interacting with
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    //If not interacted with
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    //Visualize interaction radius Gizmo (Only visible in inspector)
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}