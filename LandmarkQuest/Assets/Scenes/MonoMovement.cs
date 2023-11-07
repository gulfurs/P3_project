using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoMovement : MonoBehaviour
{
    public float speed = 10.0f;  // Bullet speed
    public Vector3 direction = Vector3.forward;  // Bullet direction
    public float lifetime = 2.0f;  // Time before the bullet is destroyed

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial velocity of the bullet
        GetComponent<Rigidbody>().velocity = direction.normalized * speed;

        // Destroy the bullet after the specified lifetime
        //Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        // You can add additional behavior or checks here if needed
    }
}
