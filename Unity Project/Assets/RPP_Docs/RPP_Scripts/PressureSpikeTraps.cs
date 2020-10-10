using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSpikeTraps : MonoBehaviour
{
    public BoxCollider pressurePlate;
    public BoxCollider spikes;
    public Material material;
    public int spikesDamage;

    //Player
    public GameObject player;
    public Player playerScript;

    private void Start()
    {
        spikes.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    private void Update()
    {
        OnTriggerEnter(pressurePlate);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("The player's Ass is about to be EXPANDED");
            if (spikes.enabled == false)
            {
                StartCoroutine(CountdownBeforeSpikes());
            }
            else
            {
                playerScript.PlayerDamage(spikesDamage);
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
