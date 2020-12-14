using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingPad : MonoBehaviour
{
    public InteractibleBehavior interactible;
    public GenerationDungeonMap generation;
    public GameObject map;
    public bool mapIsOpen;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        generation = map.GetComponent<GenerationDungeonMap>();
        interactible = GetComponentInChildren<InteractibleBehavior>();
        map.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactible.interacted && !mapIsOpen)
        {
            map.GetComponent<RectTransform>().localScale = Vector3.one;
            generation.MapUpdate();
            mapIsOpen = true;
            interactible.interacted = false;
            Time.timeScale = 0f;
        }
        else if (interactible.interacted && mapIsOpen)
        {
            map.GetComponent<RectTransform>().localScale = Vector3.zero;
            mapIsOpen = false;
            interactible.interacted = false;
            Time.timeScale = 1f;
        }
            
    }
}
