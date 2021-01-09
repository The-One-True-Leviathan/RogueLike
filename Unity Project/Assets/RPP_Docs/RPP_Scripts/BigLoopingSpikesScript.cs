using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLoopingSpikesScript : MonoBehaviour
{
    [SerializeField] BoxCollider spikes;
    public Animator spikesLoop;
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
        spikesLoop.SetInteger("LoopSpikesAnimInt", 1);
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
        spikesLoop.SetInteger("LoopSpikesAnimInt", 2);
        Debug.Log("Looping Spike has Attacked");
        FindObjectOfType<AudioManager>().Play("Sorties des Piques");
        yield return new WaitForSeconds(0.3f);
        // Retract
        spikes.enabled = false;
        spikesLoop.SetInteger("LoopSpikesAnimInt", 3);
        Debug.Log("Looping Spike has Retracted");
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
