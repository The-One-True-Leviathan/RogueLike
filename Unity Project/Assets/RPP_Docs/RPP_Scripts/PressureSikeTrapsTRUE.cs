using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSikeTrapsTRUE : MonoBehaviour
{
    public BoxCollider pressurePlate;
    public BoxCollider spikes;
    public int spikesDamage;
    private Transform spikeLocation;
    Animator spikePressure;

    //Player
    public GameObject player;
    public Player playerScript;

    private void Start()
    {
        spikes.enabled = false;
        spikePressure = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        spikeLocation = GetComponent<Transform>();
    }

    private void Update()
    {
        OnTriggerEnter(pressurePlate);
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
        spikePressure.SetInteger("PressureSpikeInt", 1);
        yield return new WaitForSeconds(1.5f);
        spikes.enabled = true;
        OnTriggerEnter(spikes);
        // Attack
        spikePressure.SetInteger("PressureSpikeInt", 2);
        yield return new WaitForSeconds(1f);
        pressurePlate.enabled = false;
        spikes.enabled = false;
        // Retract
        spikePressure.SetInteger("PressureSpikeInt", 3);
        yield return new WaitForSeconds(1f);
        // Idle
        spikePressure.SetInteger("PressureSpikeInt", 4);
        pressurePlate.enabled = true;
    }
}
