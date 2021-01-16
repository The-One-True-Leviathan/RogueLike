using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class SlowDownTime : MonoBehaviour
{
    public bool timeIsBeingSlowed, canSlowDownTime;
    Controler controler;
    public AudioMixer audioMixer;
    public AudioMixerSnapshot normal, slow;
    public float currentSlowDownTimeCooldown, maxSlowDownTimeCooldown;



    private void Awake()
    {
        controler = new Controler();
        controler.Keyboard.Enable();
        controler.TimeControl.Enable();
        controler.TimeControl.SlowDownTime.started += ctx => timeIsBeingSlowed = true;
        controler.TimeControl.SlowDownTime.canceled += ctx => timeIsBeingSlowed = false;
        controler.Keyboard.NormalizeTime.performed += ctx => LeaveTimeAlone();
        currentSlowDownTimeCooldown = maxSlowDownTimeCooldown;
        timeIsBeingSlowed = false;
    }

    private void Update()
    {
        if (currentSlowDownTimeCooldown >= 0)
        {
            canSlowDownTime = true;

        }
        else
        {
            canSlowDownTime = false;
            timeIsBeingSlowed = false;
            controler.TimeControl.SlowDownTime.canceled += ctx => timeIsBeingSlowed = false;
        }

        if (timeIsBeingSlowed)
        {
            SlowDownMan();
            currentSlowDownTimeCooldown -= Time.deltaTime;
        }
        else
        {
            if (currentSlowDownTimeCooldown < maxSlowDownTimeCooldown)
            {
                currentSlowDownTimeCooldown += Time.deltaTime;
            }
            if (currentSlowDownTimeCooldown > maxSlowDownTimeCooldown)
            {
                currentSlowDownTimeCooldown = maxSlowDownTimeCooldown;
            }
        }
    }

    void SlowDownMan()
    {        
        if (canSlowDownTime && timeIsBeingSlowed)
        {
            Debug.Log("Time is Being Slowed");
            Time.timeScale = 0.5f;
            slow.TransitionTo(0.5f);
        }        
    }

    void LeaveTimeAlone()
    {
        Debug.Log("Time Is Normal Again");
        Time.timeScale = 1f;
        normal.TransitionTo(0.5f);
        timeIsBeingSlowed = false;
    }
}
