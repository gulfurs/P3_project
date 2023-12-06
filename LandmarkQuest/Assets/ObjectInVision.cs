using UnityEngine;

public class ObjectInVision : MonoBehaviour
{
    public Renderer objectRenderer;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        // Check if the object is visible to the camera
        if (objectRenderer.isVisible)
        {
            // Object is visible, perform actions or print debug messages here
            Debug.Log("Object is visible to the camera.");
        }
        else
        {
            // Object is not visible
            Debug.Log("Object is not visible to the camera.");
        }
    }
}
