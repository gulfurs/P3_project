using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEmitter : MonoBehaviour
{
    ObjectPool objectPool;
    public string objectSpawn;

    public float emitRate = 5.0f;
    private float nextEmit = 0f;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = ObjectPool.Instance;
    }

    // FixedUpdate is called 
    void FixedUpdate()
    {
        // Check if it's time to spawn another bullet
        if (Time.time >= nextEmit)
        {
            objectPool.SpawnFromPool(objectSpawn, transform.position, Quaternion.identity);

            // Set the next fire time based on the fire rate
            nextEmit = Time.time + 1f / emitRate;
        }
    }
}
