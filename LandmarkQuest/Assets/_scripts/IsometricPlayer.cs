using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayer : MonoBehaviour
{
    public float movementSpeed = 1f;
    IsometricPlayer isoRenderer;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isoRenderer = GetComponent<IsometricPlayer>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        //isoRenderer.SetDirection(movement);
        rb.MovePosition(newPos);
    }
}
