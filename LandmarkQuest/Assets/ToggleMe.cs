using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Color;

public class ToggleMe : MonoBehaviour
{
    public SocketManager socketManager;
    // Start is called before the first frame update
    void Start()
    {
        //light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (socketManager.day)
        {
            GetComponent<Light>().color = Color.white;
        }
        else
        {
            GetComponent<Light>().color = Color.black;
        }
    }
}
