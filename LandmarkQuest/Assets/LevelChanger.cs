using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
