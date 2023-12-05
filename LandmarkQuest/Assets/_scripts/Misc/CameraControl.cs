using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : SocketMessage
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
   
    private bool isRotating = false;

    public override void Update()
    {
        // Input zooms in camera
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        // Limits zoom
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        base.Update();
    }

    public override void TiltRight()
    {
        base.TiltRight();
        if (!isRotating)
        StartCoroutine(RotateCamera(yawSpeed));
    }

    public override void TiltLeft()
    {
        base.TiltLeft();
        if (!isRotating)
        StartCoroutine(RotateCamera(-yawSpeed));
    }

    public override void NoSocket()
    {
        base.NoSocket();
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        // Camera follows target
        transform.position = target.position - offset * currentZoom;
        // Camera looks at target
        transform.LookAt(target.position);// + Vector3.up * pitch);
        // Rotates around camera
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }

    IEnumerator RotateCamera(float rotationAmount)
    {
        isRotating = true;  // Checks if the coroutine is in motion

        float targetYaw = currentYaw + rotationAmount; //Current Yaw added with the yaw speed.
        float elapsedTime = 0f; 

        while (elapsedTime < 1f)  // Duration before the coroutine may start again
        {
            currentYaw = Mathf.Lerp(currentYaw, targetYaw, elapsedTime); //Moves yaw from currentyaw to the current yaw added with yawSpeed
            elapsedTime += Time.deltaTime;  //Time progress
            yield return null;  //Yields control to the rest of the update method, (for frame friendlyness)
        }

        currentYaw = targetYaw;  // Ensure we reach the exact target yaw
        isRotating = false;  // Coroutine is no more in motion
    }
}