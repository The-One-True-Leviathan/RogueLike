using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealthBar : MonoBehaviour
{
    Controler controller;
    bool test;
    // Start is called before the first frame update
    void Start()
    {
        controller = new Controler();
        controller.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        test = controller.Keyboard.Test.triggered;
        if (test)
        {
            gameObject.SendMessage("ApplyDamage", 4f);
        }
    }
}
