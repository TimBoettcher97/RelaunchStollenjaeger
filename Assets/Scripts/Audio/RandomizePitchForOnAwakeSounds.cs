using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePitchForOnAwakeSounds : MonoBehaviour
{
    private AudioSource[] audioSources;
    [SerializeField] Vector2[] pitchRange;
    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].pitch= Random.Range(pitchRange[i].x, pitchRange[i].y);
        }
    }
}
