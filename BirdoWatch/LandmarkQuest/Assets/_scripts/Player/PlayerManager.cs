using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager PlayerInstance;

    void Awake()
    {
        PlayerInstance = this;
    }

    #endregion

    public GameObject player;
    /*public bool IsUsingSocket = true;

    void Start() {
        // Find all objects of type SocketManagement
        /*SocketManagement[] socketManagers = FindObjectsOfType<SocketManagement>();

        // Loop through each SocketManagement object
        foreach (SocketManagement socketManager in socketManagers)
        {
            if (!IsUsingSocket)
            {
                Destroy(socketManager);
            }
        }
    }*/
}
