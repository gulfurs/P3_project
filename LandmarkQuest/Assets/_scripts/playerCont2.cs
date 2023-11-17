using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCont2 : MonoBehaviour
{
    float speedMeUp;


    void FixedUpdate()
    {
        //reading the input:
        //float horizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        //float verticalAxis = CrossPlatformInputManager.GetAxis("Vertical");

        //assuming we only using the single camera:
        var camera = Camera.main;

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

        //now we can apply the movement:
        transform.Translate(desiredMoveDirection * speedMeUp * Time.deltaTime);
    }
}
