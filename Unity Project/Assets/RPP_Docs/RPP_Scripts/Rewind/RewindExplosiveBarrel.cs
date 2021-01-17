using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindExplosiveBarrel : MonoBehaviour
{
    [SerializeField] GameObject barrelObj;
    [SerializeField] ExplosionCopy explosionScript;
    [SerializeField] ReverseParticles reverseParticles;
    [SerializeField] AudioClip reverseExplosion;
    [SerializeField] AudioSource audioSource;


    private void Start()
    {
        reverseParticles.enabled = false;
    }

    public void BarrelRewind()
    {
        Debug.Log("Attempted Rewind Barrel");
        if(barrelObj.activeSelf == false)
        {
            Debug.Log("Successfully Called Rewind Barrel");
            StartCoroutine(StartReverse());
        }
    }

    IEnumerator StartReverse()
    {
        explosionScript.enemyDamageBarril.currentHP = explosionScript.enemyDamageBarril.maxHP;
        explosionScript.canExplose = true;
        reverseParticles.enabled = true;
        audioSource.clip = reverseExplosion;
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        barrelObj.SetActive(true);        
        reverseParticles.enabled = false;
    }
}
