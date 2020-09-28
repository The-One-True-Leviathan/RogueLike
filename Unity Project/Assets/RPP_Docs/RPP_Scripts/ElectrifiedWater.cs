﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedWater : MonoBehaviour
{
    public Transform poolCenter;
    public Systems systems;
    public Vector2 wetZone;
    public LayerMask currentLayer;
    public int electricDamage;
    bool canTakeDamage;

    private void Start()
    {
        canTakeDamage = true;
    }

    private void FixedUpdate()
    {
        if (canTakeDamage == true)
        {
            StartCoroutine(ElectricDamage());
        }        
    }

    void ConstantDamage()
    {
        Collider2D[] objects = Physics2D.OverlapBoxAll(poolCenter.position, wetZone, currentLayer);

        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag("Player"))
            {
                systems.TakeDamage(electricDamage);
            }
        }

        canTakeDamage = true;
    }

    IEnumerator ElectricDamage()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1f);
        ConstantDamage();
    }
   private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(poolCenter.position, wetZone);
    }
}
