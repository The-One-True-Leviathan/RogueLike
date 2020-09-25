﻿using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        //Mettre dans les scripts là où on veut jouer un son ou genre l'appeler FindObjectOfType<AudioManager>().Play("nomduson");

        if (PauseMenu.gameIsPaused)
        {
            s.source.pitch *= 5f;
        }
    }
}
