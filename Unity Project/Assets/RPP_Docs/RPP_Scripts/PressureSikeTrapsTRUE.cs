﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSikeTrapsTRUE : MonoBehaviour
{
    public BoxCollider pressurePlate;
    public BoxCollider spikes;
    public int spikesDamage;
    private Transform spikeLocation;
    public Animator spikePressure1, spikePressure2, spikePressure3, spikePressure4;

    //Player
    public GameObject player;
    public Player playerScript;

    private void Start()
    {
        spikes.enabled = false;
        spikePressure1.SetInteger("PressureSpikeInt", 4);
        spikePressure2.SetInteger("PressureSpikeInt", 4);
        spikePressure3.SetInteger("PressureSpikeInt", 4);
        spikePressure4.SetInteger("PressureSpikeInt", 4);
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        spikeLocation = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") || collision.GetComponent<EnemyDamage>())
        {
            Debug.Log("Someone's Ass is about to be EXPANDED");
            if (spikes.enabled == false)
            {
                Debug.Log("Pressure mechanism activarted");
                StartCoroutine(CountdownBeforeSpikes());
            }
            else
            {
                playerScript.PlayerDamage(spikesDamage);
                collision.GetComponent<EnemyDamage>().Damage(spikesDamage, 0, spikeLocation);
            }
        }
    }

    IEnumerator CountdownBeforeSpikes()
    {
        // Prep
        spikePressure1.SetInteger("PressureSpikeInt", 1);
        spikePressure2.SetInteger("PressureSpikeInt", 1);
        spikePressure3.SetInteger("PressureSpikeInt", 1);
        spikePressure4.SetInteger("PressureSpikeInt", 1);
        FindObjectOfType<AudioManager>().Play("Préparation des Piques");
        yield return new WaitForSeconds(1.5f);
        // Attack
        pressurePlate.enabled = false;
        spikes.enabled = true;
        spikePressure1.SetInteger("PressureSpikeInt", 2);
        spikePressure2.SetInteger("PressureSpikeInt", 2);
        spikePressure3.SetInteger("PressureSpikeInt", 2);
        spikePressure4.SetInteger("PressureSpikeInt", 2);
        FindObjectOfType<AudioManager>().Play("Sorties des Piques");
        yield return new WaitForSeconds(1f);
        // Retract
        spikes.enabled = false;
        spikePressure1.SetInteger("PressureSpikeInt", 3);
        spikePressure2.SetInteger("PressureSpikeInt", 3);
        spikePressure3.SetInteger("PressureSpikeInt", 3);
        spikePressure4.SetInteger("PressureSpikeInt", 3);
        FindObjectOfType<AudioManager>().Play("Rentrée des piques");
        yield return new WaitForSeconds(1f);
        // Idle
        spikePressure1.SetInteger("PressureSpikeInt", 4);
        spikePressure2.SetInteger("PressureSpikeInt", 4);
        spikePressure3.SetInteger("PressureSpikeInt", 4);
        spikePressure4.SetInteger("PressureSpikeInt", 4);
        pressurePlate.enabled = true;
    }
}
