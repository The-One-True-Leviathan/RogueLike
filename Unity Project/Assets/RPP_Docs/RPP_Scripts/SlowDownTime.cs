using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTime : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
