using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingSpikeTraps : MonoBehaviour
{
    public BoxCollider spikes;
    public Material material;
    public int spikesDamage;
    bool canStartLoop;
    private Transform spikeLocation;

    //Player
    public GameObject player;
    public Player playerScript;

    private void Start()
    {
        material.color = Color.yellow;
        spikes.enabled = false;
        canStartLoop = true;
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
        canStartLoop = false;
        material.color = Color.red;
        spikes.enabled = true;
        OnTriggerEnter(spikes);
        yield return new WaitForSeconds(0.3f);
        spikes.enabled = false;
        material.color = Color.yellow;
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
