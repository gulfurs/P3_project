using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEmission : MonoBehaviour
{
    public SocketManager socketManager;

    private Material material;
    private Light lightComponent;
    private bool emissionEnabled = false;

    void Start()
    {
        // Get a reference to the Material and Light
        material = GetComponent<Renderer>().material;
        lightComponent = GetComponent<Light>();
    }

    void Update()
    {
        if (socketManager == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Toggle the emission on/off
                emissionEnabled = !emissionEnabled;

                // Update the material's emission property
                if (emissionEnabled)
                {
                    // Enable emission and light
                    material.EnableKeyword("_EMISSION");
                    lightComponent.enabled = true;
                }
                else
                {
                    // Disable emission and light
                    material.DisableKeyword("_EMISSION");
                    lightComponent.enabled = false;
                }
            }
        }
        else
        {
            if (socketManager.day)
            {
                // Enable emission and light
                material.EnableKeyword("_EMISSION");
                lightComponent.enabled = true;
            }
            else
            {
                // Disable emission and light
                material.DisableKeyword("_EMISSION");
                lightComponent.enabled = false;
            }
        }
    }
}
