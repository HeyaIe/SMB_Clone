using UnityEngine.Audio;
using UnityEngine;
using System;

public class M_AudioManager : MonoBehaviour
{
    public M_Sound[] sounds;
    

    // Start is called before the first frame update
    void Awake()
    {
        foreach(M_Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>(); // Add audio source to each sound
            sound.source.clip = sound.clip; // Insert sounds
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        Play("MainTheme");
    }

    public void Play(string name)
    {
        // Find sound associated with name and play it
        M_Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound == null) return;
        sound.source.Play();
    }
    public void Stop(string name)
    {
        M_Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound == null) return;
        sound.source.Stop();
    }
}
