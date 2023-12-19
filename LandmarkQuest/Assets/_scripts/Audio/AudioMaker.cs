using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaker : MonoBehaviour
{
    public string[] sounds;
    public bool shouldLoop = false;

    void Start()
    {
        if (!shouldLoop)
        {
            StartCoroutine(DelayAudio());
        }
        else
        {
            AudioManager.instance.Playing(sounds[0]);
        }
    }

    IEnumerator DelayAudio()
    {
        while (true)
        {
            PlayRandomAudio();
            float randomInterval = Random.Range(3f, 7f);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    void PlayRandomAudio()
    {
        if (sounds != null && sounds.Length > 0)
        {
            string randomSound = sounds[Random.Range(0, sounds.Length)];
            AudioManager.instance.Playing(randomSound);
        }
    }
}
