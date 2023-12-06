using System.Collections;
using System.Collections.Generic;
using System.IO; // Added for StreamWriter
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler sceneInstance;
    public int currentScene;
    private float startTime;

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
        // Record the start time when the script starts
        startTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RetryLevel();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    public void RetryLevel()
    {
        OptimizeSounds();
        // Record and save the time spent on the current scene
        SaveTime(currentScene, Time.time - startTime);

        // Reload the current scene
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevel()
    {
        OptimizeSounds();
        // Record and save the time spent on the current scene
        SaveTime(currentScene, Time.time - startTime);

        // Go to next scene and makes sure to loop back to the beginning once we reach the end to prevent compile errors.
        int nextScene = (currentScene + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the next scene
        SceneManager.LoadScene(nextScene);

        // Updates the current scene index and reset the start time
        currentScene = nextScene;
        startTime = Time.time;
    }

    void OptimizeSounds()
    {
        AudioManager.instance.Stop("Bee");
        AudioManager.instance.Stop("Extinguish");
    }

    void SaveTime(int sceneIndex, float time)
    {
        // Specify the path to your text file
        string filePath = "Assets/TimeRecords.txt";

        // Open the file for appending
        using (StreamWriter writer = File.AppendText(filePath))
        {
            // Write the scene index and time spent to the file
            writer.WriteLine($"Scene {sceneIndex}: {time} seconds");
        }
    }
}
