using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    public GameObject ToggleObject;
    public Material green;
    public Material red;

    private Renderer switchRender;
    private bool isToggled = false;

    private void Start()
    {
        switchRender = GetComponent<Renderer>();

        if (switchRender != null && red != null)
        {
            switchRender.material = red;
        }
    }

    public override void Interact()
    {
        base.Interact();

        if (ToggleObject != null)
        {
            ToggleObject.SetActive(!ToggleObject.activeSelf);

            // Toggle the lever color
            ToggleColor();
        }
    }

    private void ToggleColor()
    {
        if (switchRender != null)
        {
            isToggled = !isToggled;

            if (isToggled && green != null)
            {
                switchRender.material = green;
            }
            else if (!isToggled && red != null)
            {
                switchRender.material = red;
            }
        }
    }
}
