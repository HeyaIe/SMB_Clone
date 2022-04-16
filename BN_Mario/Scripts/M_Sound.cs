using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class M_Sound
{
    [HideInInspector]
    public AudioSource source;

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
}
