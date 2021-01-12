﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    [SerializeField] BoxCollider boxCollider;
    public ArrowScript arrowScript;
    [SerializeField] AudioClip flecheLancé;
    [SerializeField] AudioSource audioSource;

    private void Update()
    {
        OnTriggerEnter(boxCollider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<EnemyDamage>())
        {
            arrowScript.canShoot = true;
            audioSource.clip = flecheLancé;
            audioSource.Play();
        }
    }
}
