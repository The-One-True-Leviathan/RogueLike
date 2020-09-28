using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSpikeTraps : MonoBehaviour
{
    public Systems systems;
    public BoxCollider pressurePlate;
    public BoxCollider spikes;
    public Material material;
    public int spikesDamage;

    private void Start()
    {
        spikes.enabled = false;
    }

    private void Update()
    {
        OnTriggerEnter(pressurePlate);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (spikes.enabled == false)
            {
                StartCoroutine(CountdownBeforeSpikes());
            }
            else
            {
                systems.TakeDamage(spikesDamage);
            }
            
        }
    }

    IEnumerator CountdownBeforeSpikes()
    {
        material.color = Color.yellow;
        yield return new WaitForSeconds(1.5f);
        spikes.enabled = true;
        OnTriggerEnter(spikes);
        material.color = Color.red;
        yield return new WaitForSeconds(1f);
        pressurePlate.enabled = false;
        spikes.enabled = false;
        material.color = new Color(106, 0, 46, 255);
        yield return new WaitForSeconds(1f);
        pressurePlate.enabled = true;
    }
}
