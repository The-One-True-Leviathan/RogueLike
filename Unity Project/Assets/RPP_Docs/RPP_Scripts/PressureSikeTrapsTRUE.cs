using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSikeTrapsTRUE : MonoBehaviour
{
    public BoxCollider pressurePlate;
    public BoxCollider spikes;
    public int spikesDamage;
    private Transform spikeLocation;
    public Animator spikePressure1, spikePressure2, spikePressure3, spikePressure4;
    [SerializeField] AudioClip spikesPrepare, spikesAttack, spikesRetract;
    [SerializeField] AudioSource audioSource;

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
        if (collision.CompareTag("Player"))
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
            }
        }
        if (collision.GetComponent<EnemyDamage>())
        {
            Debug.Log("Someone's Ass is about to be EXPANDED");
            if (spikes.enabled == false)
            {
                Debug.Log("Pressure mechanism activarted");
                StartCoroutine(CountdownBeforeSpikes());
            }
            else
            {
                collision.GetComponent<EnemyDamage>().Damage(spikesDamage, 0, spikeLocation);
            }
        }
    }

    IEnumerator CountdownBeforeSpikes()
    {
        // Prep
        audioSource.clip = spikesPrepare;
        audioSource.Play();
        spikePressure1.SetInteger("PressureSpikeInt", 1);
        spikePressure2.SetInteger("PressureSpikeInt", 1);
        spikePressure3.SetInteger("PressureSpikeInt", 1);
        spikePressure4.SetInteger("PressureSpikeInt", 1);
        yield return new WaitForSeconds(1.5f);
        // Attack
        pressurePlate.enabled = false;
        spikes.enabled = true;
        audioSource.clip = spikesAttack;
        audioSource.Play();
        spikePressure1.SetInteger("PressureSpikeInt", 2);
        spikePressure2.SetInteger("PressureSpikeInt", 2);
        spikePressure3.SetInteger("PressureSpikeInt", 2);
        spikePressure4.SetInteger("PressureSpikeInt", 2);
        yield return new WaitForSeconds(1f);
        // Retract
        spikes.enabled = false;
        audioSource.clip = spikesRetract;
        audioSource.Play();
        spikePressure1.SetInteger("PressureSpikeInt", 3);
        spikePressure2.SetInteger("PressureSpikeInt", 3);
        spikePressure3.SetInteger("PressureSpikeInt", 3);
        spikePressure4.SetInteger("PressureSpikeInt", 3);        
        yield return new WaitForSeconds(1f);
        // Idle
        spikePressure1.SetInteger("PressureSpikeInt", 4);
        spikePressure2.SetInteger("PressureSpikeInt", 4);
        spikePressure3.SetInteger("PressureSpikeInt", 4);
        spikePressure4.SetInteger("PressureSpikeInt", 4);
        pressurePlate.enabled = true;
    }
}
