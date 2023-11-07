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
}
