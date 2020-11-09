using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class SlowDownTime : MonoBehaviour
{
    bool TimeBeingSlowed;
    Controler controler;
    public AudioMixer audioMixer;
    public AudioMixerSnapshot normal, slow;
    

    private void Awake()
    {
        controler = new Controler();
        controler.Keyboard.Enable();
        controler.Keyboard.SlowDownTime.performed += ctx => SlowDownMan();
        controler.Keyboard.NormalizeTime.performed += ctx => LeaveTimeAlone();
    }

    void SlowDownMan()
    {
        Debug.Log("Time is Being Slowed");
        Time.timeScale = 0.5f;
        slow.TransitionTo(0.5f);
    }

    void LeaveTimeAlone()
    {
        Debug.Log("Time Is Normal Again");
        Time.timeScale = 1f;
        normal.TransitionTo(0.5f);
    }
}
