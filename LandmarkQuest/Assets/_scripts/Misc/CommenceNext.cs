using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommenceNext : MonoBehaviour
{
    public Transform platform;
    public float triggerY = 50f;
    public bool allowCommencing = true;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; 
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * triggerY);
    }

    void Update()
    {
        if (allowCommencing && transform.position.y > platform.position.y + triggerY)
        {
            SceneHandler.sceneInstance.LoadNextLevel();
        }
    }
}
