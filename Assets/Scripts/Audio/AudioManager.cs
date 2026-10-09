using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (Sounds s in sounds)
        {
            
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume= s.volume;
            
        }
    }

    public void Play(string name) 
    { 
        Sounds s = Array.Find(sounds, s => s.name == name);
        if (s == null)
        {
            Debug.LogWarning("Wrong SoundName");
            return;
        }
        else 
        {
            RandomPitch(s);
            RandomClip(s);
            s.source.Play();
        }
    }

    private void RandomClip(Sounds s)
    {
        if (s.secondaryClip != null)
        {
            int chosenClip = UnityEngine.Random.Range(0, 2);
            if (chosenClip == 1)
            {
                s.source.clip = s.secondaryClip;
            }
            else
            {
                s.source.clip = s.clip;
            }
        }
    }

    private void RandomPitch(Sounds s)
    {
        if (s.randomPitch)
        {
            s.source.pitch = UnityEngine.Random.Range(s.pitchRange.x, s.pitchRange.y);
        }
        else
        {
            s.source.pitch = s.pitch;
        }
    }
}
