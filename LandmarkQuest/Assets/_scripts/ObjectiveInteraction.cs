using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveInteraction : Interactable
{
    public Animator animator;
    public string checkRadius = "WithinRadius";

    public float flySpeed = 10.0f;
    private bool isFlying = false;

    public override void Interact()
    {
        base.Interact();
        animator.SetBool(checkRadius, true);

        isFlying = true;
    }

    void FixedUpdate()
    {
        if (isFlying)
        {
            transform.Translate(Vector3.up * flySpeed * Time.deltaTime);
        }
    }
}
