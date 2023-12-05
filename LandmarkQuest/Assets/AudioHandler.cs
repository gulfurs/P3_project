using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        switch (SceneHandler.sceneInstance.currentScene)
        {
            case 1:
                AudioManager.instance.Stop("AmbientNoise");
                AudioManager.instance.Stop("NightTime");
                AudioManager.instance.Playing("MainMenu", 1f);
                AudioManager.instance.Playing("MainMenu2", 0f);
                break;
            case 6:
                AudioManager.instance.Stop("MainMenu");
                AudioManager.instance.Stop("MainMenu2");
                AudioManager.instance.Stop("AmbientNoise");
                AudioManager.instance.Playing("NightTime", 1f, true);
                break;
            case 7:
                AudioManager.instance.Stop("MainMenu");
                AudioManager.instance.Stop("MainMenu2");
                AudioManager.instance.Stop("AmbientNoise");
                AudioManager.instance.Playing("NightTime", 1f, true);
                break;
            default:
                AudioManager.instance.Stop("MainMenu");
                AudioManager.instance.Stop("MainMenu2");
                AudioManager.instance.Stop("NightTime");
                AudioManager.instance.Playing("AmbientNoise", 1f, true);
                break;
        }
    }

    public void StartingGame()
    {
        AudioManager.instance.ChangeVolume("MainMenu", 0f);
        AudioManager.instance.ChangeVolume("MainMenu2", 1f);
    }

    public void StartingMenu()
    {
        AudioManager.instance.ChangeVolume("MainMenu", 1f);
        AudioManager.instance.ChangeVolume("MainMenu2", 0f);
    }
}
