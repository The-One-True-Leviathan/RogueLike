using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindExplosiveBarrel : MonoBehaviour
{
    [SerializeField] GameObject barrelObj;
    [SerializeField] ExplosionCopy explosionScript;
    [SerializeField] ReverseParticles reverseParticles;

    private void Start()
    {
        reverseParticles.enabled = false;
    }

    public void BarrelRewind()
    {
        if(barrelObj.activeSelf == false)
        {
            StartCoroutine(StartReverse());
        }
    }

    IEnumerator StartReverse()
    {
        explosionScript.enemyDamageBarril.currentHP = explosionScript.enemyDamageBarril.maxHP;
        reverseParticles.enabled = true;
        yield return new WaitForSeconds(2f);
        barrelObj.SetActive(true);
        reverseParticles.enabled = false;
    }
}
