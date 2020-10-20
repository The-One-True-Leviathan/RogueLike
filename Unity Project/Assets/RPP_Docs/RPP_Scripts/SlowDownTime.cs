using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlowDownTime : MonoBehaviour
{
    bool TimeBeingSlowed;
    Controler controler;

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
    }

    void LeaveTimeAlone()
    {
        Debug.Log("Time Is Normal Again");
        Time.timeScale = 1f;
    }
}
