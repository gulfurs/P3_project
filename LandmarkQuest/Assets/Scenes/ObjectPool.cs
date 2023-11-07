using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private int objectCount = 0;
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPool Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject obj2Spawn = poolDictionary[tag].Dequeue();

        //Spawns Object
        obj2Spawn.SetActive(true);
        obj2Spawn.transform.position = position;
        obj2Spawn.transform.rotation = rotation;


        // Set the name of the spawned object
        //obj2Spawn.name = tag + objectCount;
        //objectCount++;

        // Enqueue the object back into the pool so it can be reused
        poolDictionary[tag].Enqueue(obj2Spawn);

        return obj2Spawn;
    }
}
