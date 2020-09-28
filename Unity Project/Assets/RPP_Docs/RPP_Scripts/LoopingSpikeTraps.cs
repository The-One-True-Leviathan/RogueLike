using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingSpikeTraps : MonoBehaviour
{
    public Systems systems;
    public BoxCollider spikes;
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
            systems.TakeDamage(spikesDamage);
        }
    }
}
