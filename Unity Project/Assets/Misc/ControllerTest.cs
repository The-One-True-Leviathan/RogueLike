using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    Controler controller;
    Rigidbody rigidbody;
    public float maxSpeed;
    public float xAxis;
    public float yAxis;
    // Start is called before the first frame update

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        controller = new Controler();
        controller.Enable();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = controller.Keyboard.Movement.ReadValue<Vector2>().x*maxSpeed;
        yAxis = controller.Keyboard.Movement.ReadValue<Vector2>().y * maxSpeed;
        print("xAxis = " + xAxis + " / yAxis = " + yAxis);
        Vector3 speed = new Vector3(xAxis, 0f, yAxis);
        rigidbody.velocity = speed;
        
    }
}
