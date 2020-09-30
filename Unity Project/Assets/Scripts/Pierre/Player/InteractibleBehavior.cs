﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleBehavior : MonoBehaviour
{
    public bool interactible, interacted;
    public GameObject player;
    public Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactible)
        {
            Vector3 thisToPlayer = transform.position - player.transform.position;
            float distanceToPlayer = thisToPlayer.magnitude;
            if (distanceToPlayer > playerScript.interactRange)
            {
                interactible = false;
            }
        }
    }
}
