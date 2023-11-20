using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public List<Transform> waypoints; // List of waypoints
    public float speed = 5.0f; // Movement speed
    public float waitTimeBetweenPoints = 5.0f; // Time the object waits at a point
    public bool loopPattern = true;
    public bool faceWaypoint = true;

    private int currentWaypointIndex = 0;
    private float waitTime;

    private void Start()
    {
        // Initialize any variables or setup here
        waitTime = waitTimeBetweenPoints;

        // Set the initial position of the object to the position of the first waypoint
        if (waypoints.Count > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    private void Update()
    {
        //Debug.Log(waitTime);
        if (currentWaypointIndex < waypoints.Count)
        {
            if (waitTime <= 0)
            {
                Transform targetWaypoint = waypoints[currentWaypointIndex];

                // Calculate the direction to the current waypoint
                Vector3 targetDirection = targetWaypoint.position - transform.position;

                // Check if the object has reached the current waypoint
                if (targetDirection.magnitude < 0.1f)
                {
                    // If reached, update currentWaypointIndex
                    currentWaypointIndex++;
                    waitTime = waitTimeBetweenPoints;

                    // Check if we should loop
                    if (currentWaypointIndex >= waypoints.Count)
                    {
                        if (loopPattern)
                        {
                            currentWaypointIndex = 0; // Reset to the first waypoint
                        }
                        else
                        {
                            Debug.Log("Hello There");
                        }
                    }
                }
                else
                {
                    // Move the object towards the current waypoint using the specified speed
                    transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

                    // Face the next waypoint
                    if (faceWaypoint)
                    {
                        FaceWaypoint();
                    }
                }
            }
            else
            {
                // Implement waiting logic at waypoints
                waitTime -= Time.deltaTime;
            }
        }
    }

    // Method to make the object face the next waypoint
    void FaceWaypoint()
    {
        // Find direction towards the next waypoint
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Face the direction while not changing y-rotation
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        // Smooth out rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
