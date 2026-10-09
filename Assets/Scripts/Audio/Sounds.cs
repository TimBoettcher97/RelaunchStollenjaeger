using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds 
{
    public string name;

    public AudioClip clip;
    public AudioClip secondaryClip;

    [Range(0,1)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool randomPitch;
    public Vector2 pitchRange = new Vector2 (0.1f ,3f);
    [HideInInspector]
    public AudioSource source;

}
