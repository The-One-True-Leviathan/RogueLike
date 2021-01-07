using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingSpikeTrapsTRUE : MonoBehaviour
{
    [SerializeField] BoxCollider spikes;
    public Animator spikesLoop1, spikesLoop2, spikesLoop3, spikesLoop4;
    public int spikesDamage;
    bool canStartLoop;
    private Transform spikeLocation;

    //Player
    public GameObject player;
    public Player playerScript;

    private void Start()
    {
        spikes = GetComponent<BoxCollider>();
        spikes.enabled = false;
        canStartLoop = true;
        spikesLoop1.SetInteger("LoopSpikesInt", 1);
        spikesLoop2.SetInteger("LoopSpikesInt", 1);
        spikesLoop3.SetInteger("LoopSpikesInt", 1);
        spikesLoop4.SetInteger("LoopSpikesInt", 1);
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        spikeLocation = GetComponent<Transform>();
    }


    private void FixedUpdate()
    {
        if (canStartLoop == true)
        {
            StartCoroutine(SpikesLoop());
        }
    }

    IEnumerator SpikesLoop()
    {
        //Attack
        canStartLoop = false;        
        spikes.enabled = true;
        spikesLoop1.SetInteger("LoopSpikesInt", 3);
        spikesLoop2.SetInteger("LoopSpikesInt", 3);
        spikesLoop3.SetInteger("LoopSpikesInt", 3);
        spikesLoop4.SetInteger("LoopSpikesInt", 3);
        FindObjectOfType<AudioManager>().Play("Sorties des Piques");
        yield return new WaitForSeconds(0.3f);
        // Retract
        spikes.enabled = false;
        spikesLoop1.SetInteger("LoopSpikesInt", 2);
        spikesLoop2.SetInteger("LoopSpikesInt", 2);
        spikesLoop3.SetInteger("LoopSpikesInt", 2);
        spikesLoop4.SetInteger("LoopSpikesInt", 2);
        FindObjectOfType<AudioManager>().Play("Rentrée des piques");
        yield return new WaitForSeconds(2f);
        canStartLoop = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerScript.PlayerDamage(spikesDamage);
        }
        if (collision.GetComponent<EnemyDamage>())
        {
            collision.GetComponent<EnemyDamage>().Damage(spikesDamage, 0, spikeLocation);
        }
    }
}
