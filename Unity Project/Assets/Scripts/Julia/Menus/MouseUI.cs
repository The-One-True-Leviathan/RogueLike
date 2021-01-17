using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseUI : MonoBehaviour
{
    public LayerMask UI;
    public Controler controller;
    public Camera mainCamera;
    public List<Collider> btns;

    // Start is called before the first frame update
    void Start()
    {
        controller = new Controler();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, UI))
        {
            hit.collider.GetComponent<BehaviorButtons>().Hover();
            hit.collider.GetComponent<BehaviorButtons>().mouseOnButton = true;
            Debug.Log("touch");
        }

        foreach (Collider button in btns)
        {
            if (button.GetComponent<Collider>() != hit.collider)
            {
                button.GetComponent<BehaviorButtons>().NotHover();
                button.GetComponent<BehaviorButtons>().soundWasPlayed = false;
            }
        }


    }

}
