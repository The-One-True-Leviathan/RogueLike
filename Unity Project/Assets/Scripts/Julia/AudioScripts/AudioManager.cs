using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] juliaSounds, combatSounds, enemies, musiques, joueur, PNJs, thailand;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sounds s in juliaSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        foreach (Sounds s in combatSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        foreach (Sounds s in enemies)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        foreach (Sounds s in musiques)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        foreach (Sounds s in joueur)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        foreach (Sounds s in PNJs)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        foreach (Sounds s in thailand)
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
        //Sons de Julia
        Sounds s = Array.Find(juliaSounds, sound => sound.name == name);
        s.source.Play();
        //Sons de combat
        Sounds s1 = Array.Find(combatSounds, sound => sound.name == name);
        s1.source.Play();
        //Sons des ennemis
        Sounds s2 = Array.Find(combatSounds, sound => sound.name == name);
        s2.source.Play();
        //Musiques
        Sounds s3 = Array.Find(combatSounds, sound => sound.name == name);
        s3.source.Play();
        //Cris du joueur
        Sounds s4 = Array.Find(combatSounds, sound => sound.name == name);
        s4.source.Play();
        //Sons des PNJs
        Sounds s5 = Array.Find(combatSounds, sound => sound.name == name);
        s5.source.Play();
        //Sons des Traps 
        Sounds s6 = Array.Find(combatSounds, sound => sound.name == name);
        s6.source.Play();
        //Mettre dans les scripts là où on veut jouer un son ou genre l'appeler FindObjectOfType<AudioManager>().Play("nomduson");

        if (PauseMenu.gameIsPaused)
        {
            s.source.pitch *= 5f;
            s1.source.pitch *= 5f;
            s2.source.pitch *= 5f;
            s3.source.pitch *= 5f;
            s4.source.pitch *= 5f;
            s5.source.pitch *= 5f;
            s6.source.pitch *= 5f;
        }
    }
}
