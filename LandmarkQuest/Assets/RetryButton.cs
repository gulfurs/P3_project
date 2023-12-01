using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    //private SceneHandler Sc
    // Start is called before the first frame update
    void Start()
    {
        Button retryButton = GetComponent<Button>();
        retryButton.onClick.AddListener(SceneHandler.sceneInstance.RetryLevel);
    }
}
