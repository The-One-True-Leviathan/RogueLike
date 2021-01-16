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
    [SerializeField] AudioClip slowMo;
    [SerializeField] AudioSource audioSource;



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
        }

        if (timeIsBeingSlowed)
        {
            StartCoroutine (SlowDownMan());
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

    IEnumerator SlowDownMan()
    {        
        if (canSlowDownTime && timeIsBeingSlowed)
        {
            Debug.Log("Time is Being Slowed");
            audioSource.clip = slowMo;
            audioSource.Play();
            Time.timeScale = 0.5f;
            slow.TransitionTo(0.5f);
            yield return new WaitForSeconds(maxSlowDownTimeCooldown);
            LeaveTimeAlone();
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
