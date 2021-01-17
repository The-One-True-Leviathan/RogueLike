using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            /*foreach (Button but in map.GetComponentsInChildren<Button>())
            {
                but.enabled = true;
            }*/
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
            foreach (Button but in map.GetComponentsInChildren<Button>())
            {
                but.enabled = false;
            }
        }
            
    }
}
