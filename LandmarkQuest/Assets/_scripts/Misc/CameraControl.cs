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

    public float yawSpeed = 100f;

    private float currentZoom = 10f;
    private float currentYaw = 0f;

    void Update()
    {
        //Input zooms in camera
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        //Limits zooms
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        //Input rotates camera around
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        //Camera follows target
        transform.position = target.position - offset * currentZoom;
        //Camera looks at target
        transform.LookAt(target.position + Vector3.up * pitch);

        //Rotates around camera
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }

}