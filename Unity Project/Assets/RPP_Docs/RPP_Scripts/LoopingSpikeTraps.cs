using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingSpikeTraps : MonoBehaviour
{
    public Systems systems;
    public BoxCollider2D spikes;
    public Material material;
    public int spikesDamage;
    bool canStartLoop;

    private void Start()
    {
        material.color = Color.yellow;
        spikes.enabled = false;
        canStartLoop = true;
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
        OnTriggerEnter2D(spikes);
        yield return new WaitForSeconds(2f);
        spikes.enabled = false;
        material.color = Color.yellow;
        yield return new WaitForSeconds(2f);
        canStartLoop = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            systems.TakeDamage(spikesDamage);
        }
    }
}
