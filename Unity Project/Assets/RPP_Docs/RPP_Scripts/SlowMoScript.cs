using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMoScript : MonoBehaviour
{
    public Slider slider;
    public SlowDownTime slow;

    void Start()
    {
        slow = GameObject.FindGameObjectWithTag("Player").GetComponent<SlowDownTime>();        
    }

    private void Update()
    {
        slider.value = slow.currentSlowDownTimeCooldown;
    }
}
