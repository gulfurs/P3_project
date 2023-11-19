using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerConB : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public float limit = 5f;


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -limit, limit);
        newPosition.z = Mathf.Clamp(newPosition.z, -limit, limit);

        transform.position = newPosition;
    }
}


//Backup
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    public CharacterController controller;
    public Rigidbody prb;
    public float moveSpeed = 5f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

 
    void Update()
    {

        HandleMovementInput();
    }
    void HandleMovementInput() { 

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(0,0,0);
        
        if (IsPrimaryInputMethodDetected())
        {
            //Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
             if (horizontal < 0){
             direction = new Vector3(horizontal, 0, -horizontal);
             }
             if (horizontal > 0){
                 direction = new Vector3(horizontal, 0, -horizontal);
             }
             if (vertical < 0){
                 direction = new Vector3(vertical, 0, vertical);
             }
             if (vertical > 0){
                 direction = new Vector3(vertical, 0, vertical);
             }

            if (direction.magnitude >= 0.1f)
            {
                UpdateRotation(direction);
                UpdateVelocity(direction);
                
                //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                //transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //controller.Move(direction * moveSpeed * Time.deltaTime);
                //prb.velocity = (direction * moveSpeed);// * Time.deltaTime);

            }
        }

    }

    void UpdateRotation(Vector3 direction){
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    void UpdateVelocity(Vector3 direction)
    {
        prb.velocity = (direction * moveSpeed);// * Time.deltaTime);
    }

    bool IsPrimaryInputMethodDetected()
    {
        // Check if the primary keys (W, A, S, D) are pressed
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }
}

*/