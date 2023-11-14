using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 5f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(0,0,0);
        
        if (IsPrimaryInputMethodDetected())
        {
            if (horizontal < 0)
            {
                direction = new Vector3(horizontal, 0, -horizontal);
            }
            if (horizontal > 0)
            {
                direction = new Vector3(horizontal, 0, -horizontal);
            }
            if (vertical < 0)
            {
                direction = new Vector3(vertical, 0, vertical);
            }
            if (vertical > 0)
            {
                direction = new Vector3(vertical, 0, vertical);
            }
            //Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                controller.Move(direction * moveSpeed * Time.deltaTime);
            }
        }

        
        
    }
    bool IsPrimaryInputMethodDetected()
    {
        // Check if the primary keys (W, A, S, D) are pressed
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }
}
