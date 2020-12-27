using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingPads : MonoBehaviour
{
    public Text textBox;
    public Collider colliderTalk;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponentInChildren<Text>();
        colliderTalk = GetComponent<Collider>();
        textBox.enabled = false;
    }

    // Update is called once per frame
    private void OnCollisionExit(Collision collision)
    {
        textBox.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        textBox.enabled = true;
    }
}
