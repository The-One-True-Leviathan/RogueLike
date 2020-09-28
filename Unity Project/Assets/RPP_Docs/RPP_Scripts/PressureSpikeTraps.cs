using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSpikeTraps : MonoBehaviour
{
    public Systems systems;
    public BoxCollider2D pressurePlate;
    public BoxCollider2D spikes;
    public Material material;
    public int spikesDamage;

    private void Start()
    {
        spikes.enabled = false;
    }

    private void Update()
    {
        OnTriggerEnter2D(pressurePlate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        OnTriggerEnter2D(spikes);
        material.color = Color.red;
        yield return new WaitForSeconds(1f);
        pressurePlate.enabled = false;
        spikes.enabled = false;
        material.color = new Color(106, 0, 46, 255);
        yield return new WaitForSeconds(1f);
        pressurePlate.enabled = true;
    }
}
