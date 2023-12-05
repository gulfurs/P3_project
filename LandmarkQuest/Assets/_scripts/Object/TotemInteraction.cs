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
    private float rotationSpeed = 50.0f;
    private float speedofRotation;
    private SocketManagement socketManager;

    void Start()
    {
        mainCamera = Camera.main;
        playerController = FindObjectOfType<PlayerController>();
        cameraControl = FindObjectOfType<CameraControl>();
        speedofRotation = rotationSpeed;

        GameObject socketObject = GameObject.Find("Socket");

        if (socketObject != null)
        {
            socketManager = socketObject.GetComponent<SocketManagement>();
        }
    }

    public override void Interact()
    {
        base.Interact();

        totemTop = transform.Find("TotemTop");
        totemView = totemTop.GetComponentInChildren<Camera>();

        if (totemView != null)
        {
            if (mainCamera != null)
            {
                mainCamera.enabled = false;
            }

            cameraControl.enabled = false;
            playerController.enabled = false;

            totemView.enabled = true;
        }
        else
        {
            Debug.LogError("Totem camera not found under TotemTop.");
        }
    }

    void FixedUpdate()
    {
        if (socketManager != null)
        {
            if (totemView != null && totemView.enabled)
            {
                // Get the current message
                int currentMessage = socketManager.GetCurrentMessage();
                Debug.Log(currentMessage);

                if (currentMessage == 1)
                {
                    rotationSpeed = -1;
                }
                else if (currentMessage == 2)
                {
                    rotationSpeed = 1;
                }
                else if (currentMessage == 0)
                {
                    rotationSpeed = 0;
                }

                totemTop.Rotate(Vector3.forward, rotationSpeed);

                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    ReturnToMainCamera();
                }
            }
        }
        else
        {

            if (totemView != null && totemView.enabled)
            {

                float zRotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

                totemTop.Rotate(Vector3.forward, zRotation);

                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    ReturnToMainCamera();
                }
            }
        }
    }

    void ReturnToMainCamera()
    {
        totemView.enabled = false;

        if (mainCamera != null)
        {
            mainCamera.enabled = true;
        }

        playerController.enabled = true;
        cameraControl.enabled = true;
    }
}