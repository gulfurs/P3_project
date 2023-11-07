using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemInteraction : Interactable
{
    public Camera mainCamera;
    public PlayerController playerController;
    public CameraControl cameraControl;

    private Transform totemTop;
    private Camera totemView;

    void Start()
    {
        mainCamera = Camera.main;
        playerController = FindObjectOfType<PlayerController>();
        cameraControl = FindObjectOfType<CameraControl>();
    }

    public override void Interact()
    {
        base.Interact();

        // Find the "TotemTop" child object of the "WaterTotem" and get the camera under it.
        totemTop = transform.Find("TotemTop");
        totemView = totemTop.GetComponentInChildren<Camera>();

        // Check if the camera was found.
        if (totemView != null)
        {
            // Disable the current main camera.
            if (mainCamera != null)
            {
                mainCamera.enabled = false;
            }

            cameraControl.enabled = false;
            playerController.enabled = false;

            // Enable the totem camera as the new main camera.
            totemView.enabled = true;
        }
        else
        {
            Debug.LogError("Totem camera not found under TotemTop.");
        }
    }

    void FixedUpdate()
    {
        if (totemView != null && totemView.enabled)
        {
            // Input to rotate the totemTop left and right along the Z-axis.
            float rotationSpeed = 50.0f; // Adjust the rotation speed as needed.
            float zRotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

            // Rotate the TotemTop around its forward (Z) axis.
            totemTop.Rotate(Vector3.forward, zRotation);

            // Check for mouse click (left or right button) to return to the main camera.
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                ReturnToMainCamera();
            }
        }
    }

    void ReturnToMainCamera()
    {
        // Disable the totem camera.
        totemView.enabled = false;

        // Enable the main camera.
        if (mainCamera != null)
        {
            mainCamera.enabled = true;
        }

        playerController.enabled = true;
        cameraControl.enabled = true;
    }
}
