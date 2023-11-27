using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : Interactable
{
    public Animator playerAnimator;
    public float pushForce = 10f; // Adjust this value as needed
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on pushable object");
        }
    }

    public override void Interact()
    {
        base.Interact();

        if (playerAnimator != null)
        {
            playerAnimator.Play("Pushing");
        }

        ApplyPushForce();
    }

    private void ApplyPushForce()
    {
        if (rb != null)
        {
            // Get the direction from the player to the pushable object
            Vector3 pushDirection = transform.position - playerAnimator.transform.position;
            pushDirection.y = 0f; // Ensure the direction is horizontal

            // Normalize the direction and apply force
            rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
        }
    }
}
