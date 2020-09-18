using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    Controler controller;
    public float xAxis;
    public float yAxis;
    // Start is called before the first frame update

    private void OnEnable()
    {
        controller = new Controler();
        controller.Enable();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = controller.Keyboard.Movement.ReadValue<Vector2>().x;
        yAxis = controller.Keyboard.Movement.ReadValue<Vector2>().y;
        print("xAxis = " + xAxis + " / yAxis = " + yAxis);
    }
}
