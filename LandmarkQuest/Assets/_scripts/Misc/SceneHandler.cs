using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler sceneInstance;
    private int currentScene;

    void Awake()
    {
        if (sceneInstance == null)
        {
            sceneInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Get the current scene index
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RetryLevel();
        }
    }

    public void RetryLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevel()
    {
        //Go to next scene and makes sure to loop back to the beginning once we reach the end to prevent compile errors.
        int nextScene = (currentScene + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the next scene
        SceneManager.LoadScene(nextScene);

        // Updates the current scene index
        currentScene = nextScene;
    }
}