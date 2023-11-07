using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateEye : MonoBehaviour
{
    public SocketManager socketManager;
    public Sprite EyeOpen, EyeClosed;
    private Image eyeImage;

    // Start is called before the first frame update
    void Start()
    {
        eyeImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (socketManager.isBlinking)
        {
            // Use the EyeClosed sprite when isBlinking is true
            eyeImage.sprite = EyeClosed;
        }
        else
        {
            eyeImage.sprite = EyeOpen;
            /*
            // Move the eye GameObject based on eye coordinates
            Vector2 eyeCoordinates = socketManager.rightEyeCoords; // Use leftEyeCoords if needed
            MoveEye(eyeCoordinates);*/
        }
    }

    // Function to move the eye GameObject
    void MoveEye(Vector2 eyeCoordinates)
    {
        // Adjust the eye position based on eyeCoordinates
        // You can use this function to control the eye's position in your UI
        // Example: transform.position = new Vector3(eyeCoordinates.x, eyeCoordinates.y, transform.position.z);
    }
}
