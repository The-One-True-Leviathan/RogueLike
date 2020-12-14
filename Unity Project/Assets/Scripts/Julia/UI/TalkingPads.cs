using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingPads : MonoBehaviour
{
    public Text textBox;
    public InteractibleBehavior interactible;

    // Start is called before the first frame update
    void Start()
    {
        interactible = GetComponentInChildren<InteractibleBehavior>();
        textBox = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textBox.enabled = false;
        if (interactible.interactible)
        {
            textBox.enabled = true;
        }
    }
}
