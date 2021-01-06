using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingPads : MonoBehaviour
{
    public Text textBox;
    public Collider colliderTalk;
    public InteractibleBehavior InteractibleBehavior;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponentInChildren<Text>();
        textBox.enabled = false;
        InteractibleBehavior = GetComponentInChildren<InteractibleBehavior>();
    }

    private void Update()
    {
        if (InteractibleBehavior.interactible)
        {
            textBox.enabled = true;
        }
        else
        {
            textBox.enabled = false;
        }
    }

    // Update is called once per frame
    /*private void OnCollisionExit(Collision collision)
    {
        textBox.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("NIQUE");
        textBox.enabled = true;
    }*/
}
