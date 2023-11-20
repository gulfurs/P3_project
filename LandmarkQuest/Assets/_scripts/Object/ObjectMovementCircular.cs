using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementCircular : MonoBehaviour
{
    public float speedO = 5.0f; // Rotation speed
    public float radius = 5.0f; // Radius of the circular path
    public bool clockwise = true; // Direction of rotation (clockwise or counterclockwise)
    public bool verticalPattern = false; // Move vertically or horizontally
    public bool loopPattern = true;
    public Transform rotationCenter; // Assign a public transform to set the center of rotation
    public bool pickInitialPosition = false;
    public float angle = 0.0f;
    public bool faceAngle = true;

    private float currentXAngle = 0.0f;
    private float currentYAngle = 0.0f;
    private Vector3 initialPosition;
    private bool rotationComplete = false;  

    private void Start()
    {
        initialPosition = transform.position;
        if (!pickInitialPosition)
        {
            angle = clockwise ? 360f : 0.0f;
        }
    }

    private void Update()
    {
        if (rotationCenter == null || rotationCenter == transform)
        {
            RotateAroundSelf();
        }
        else
        {
            RotateAroundCenter();
        }

        if (faceAngle)
        {
            FaceAngles();
        }
    }

    private void RotateAroundCenter()
    {
        if (loopPattern)
        {
            rotationComplete = false;
            angle += (clockwise ? -1 : 1) * speedO * Time.deltaTime;
            if (angle >= 360)
            {
                angle -= 360;
            }
            else if (angle < 0)
            {
                angle += 360;
            }
        } else 
        {
            if (!rotationComplete) {
                angle += (clockwise ? -1 : 1) * speedO * Time.deltaTime;
                if (angle >= 360)
                {
                    if (!loopPattern)
                    {
                        rotationComplete = true;
                    }
                }
                else if (angle < 0)
                {
                    if (!loopPattern)
                    {
                        rotationComplete = true;
                    }
                }
            }
        }

        Vector3 offset = (verticalPattern == false) ?
            Quaternion.Euler(0, angle, 0) * Vector3.forward * radius :
            Quaternion.Euler(angle, 0, 0) * Vector3.up * radius;

        transform.position = rotationCenter.position + offset;
    }

    private void RotateAroundSelf()
    {
        currentXAngle = transform.rotation.eulerAngles.x;
        currentYAngle = transform.rotation.eulerAngles.y;

        if (loopPattern)
        {
            rotationComplete = false;
            float rotationAngle = (clockwise ? -1 : 1) * speedO * Time.deltaTime;

            if (verticalPattern)
            {
                transform.Rotate(rotationAngle, 0, 0);
            }
            else
            {
                transform.Rotate(0, rotationAngle, 0);
            }
        } else {
            if (!rotationComplete)
            {
                float rotationAngle = (clockwise ? -1 : 1) * speedO * Time.deltaTime;

                if (verticalPattern)
                {
                    transform.Rotate(rotationAngle, 0, 0);
                    if (!loopPattern && (currentXAngle < 0.5f || currentXAngle > 359.5f))
                    {
                        rotationComplete = true;
                    }
                }
                else
                {
                    transform.Rotate(0, rotationAngle, 0);
                    if (!loopPattern && (currentYAngle < 0.5f || currentYAngle > 359.5f))
                    {
                        rotationComplete = true;
                    }
                }
            }
        }
    }

    private void FaceAngles()
    {
        // Find direction towards the next angle
        Vector3 direction = (rotationCenter.position - transform.position).normalized;

        // Calculate the rotation to face the next angle (We make sure that it faces angles instead of the center by using Euler)
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)) * Quaternion.Euler(0f, 90f, 0f);

        // Smooth out rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


}
