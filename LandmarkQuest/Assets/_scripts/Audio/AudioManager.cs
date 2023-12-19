using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;
    public bool randomizePitch;

    [HideInInspector]
    public AudioSource source;
}


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        //Singleton pattern
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        //We add the AudioSource component and apply the properties from the sound class to the component.
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public bool Playing(string name, float vol = 1.0f, bool isMusic = false)
    {
        //Find sounds of same name as the one defined in the parameter.
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.volume = Mathf.Clamp(vol, 0f, 1f);
            //We randomize the pitch before playing our sound. (If randomize pitch is active.)
            if (s.randomizePitch)
            {
                float randomPitch = UnityEngine.Random.Range(s.pitch - 0.1f, s.pitch + 0.1f);
                s.source.pitch = randomPitch;
            }
            else
            {
                s.source.pitch = s.pitch;
            }
            if (isMusic) {
                if (s.source.isPlaying) {
                    return false;
                }
            }

            s.source.Play();
            return true;
        }

        return false;
    }

    //Stops whatever sound is playing.
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null && s.source.isPlaying)
        {
            s.source.Stop();
        }
    }

    public void ChangeVolume(string soundName, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if (s != null)
        {
            s.source.volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }
}
