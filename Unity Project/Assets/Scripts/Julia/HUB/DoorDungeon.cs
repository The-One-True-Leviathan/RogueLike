using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorDungeon : MonoBehaviour
{
    public InteractibleBehavior interactible;
    // Start is called before the first frame update
    void Start()
    {
        interactible = GetComponentInChildren<InteractibleBehavior>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(interactible.interacted)
        {
            interactible.interacted = false;
            SceneManager.LoadScene("Procedural Gen");
        }
    }
}
