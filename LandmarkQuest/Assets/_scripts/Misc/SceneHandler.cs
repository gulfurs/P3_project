using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler sceneInstance;
    public int currentScene;

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
        DontDestroyOnLoad(gameObject);
        // Get the current scene index
        currentScene = SceneManager.GetActiveScene().buildIndex;

    }

    void Start()
    {
      
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
        OptimizeSounds();
        // Reload the current scene
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevel()
    {
        OptimizeSounds();
        //Go to next scene and makes sure to loop back to the beginning once we reach the end to prevent compile errors.
        int nextScene = (currentScene + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the next scene
        SceneManager.LoadScene(nextScene);

        // Updates the current scene index
        currentScene = nextScene;
    }

    void OptimizeSounds() {
        AudioManager.instance.Stop("Bee");
        AudioManager.instance.Stop("Extinguish");
    }
}
