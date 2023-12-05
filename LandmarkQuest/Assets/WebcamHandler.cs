using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamHandler : MonoBehaviour
{
    private RawImage img;

    private WebCamTexture webCam;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<RawImage>();

        webCam = new WebCamTexture();

        if (webCam != null)
        {
            if (!webCam.isPlaying)
            {
                webCam.Play();
            }
            img.texture = webCam;
        }
    }
}
