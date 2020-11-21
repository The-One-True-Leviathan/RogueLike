using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDetect : MonoBehaviour
{
    public Player player;
    public Controler controller;
    public LayerMask MouseLayer;
    public Camera mainCamera;
    public Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        controller = new Controler();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, MouseLayer))
        {
            player.mousePosition = hit.point;
        }

    }
}
