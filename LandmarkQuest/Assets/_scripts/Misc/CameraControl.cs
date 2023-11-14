using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float pitch = 2f;

    private float currentZoom = 10f;
    public float currentYaw = 0f;

    public float yawSpeed = 100f;
    private float speedofYaw;
    private SocketManagement socketManager;

    void Start()
    {
        socketManager = GetComponent<SocketManagement>();
        speedofYaw = yawSpeed;
    }

    void Update()
    {
        // Input zooms in camera
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        // Limits zoom
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        if (socketManager != null)
        {
            int currentMessage = socketManager.GetCurrentMessage();
            currentYaw += yawSpeed;

            // Adjust yaw based on the received message
            if (currentMessage == 1)
            {
                yawSpeed = speedofYaw;
                yawSpeed = Mathf.Abs(yawSpeed/100);
            }
            else if (currentMessage == 2)
            {
                yawSpeed = speedofYaw;
                yawSpeed = -Mathf.Abs(yawSpeed/100);
            }
            else if (currentMessage == 0)
            {
                yawSpeed = 0;
            }
        }
        else
        {
            currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        // Camera follows target
        transform.position = target.position - offset * currentZoom;
        // Camera looks at target
        transform.LookAt(target.position + Vector3.up * pitch);
        // Rotates around camera
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}