﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCopy : MonoBehaviour
{
    //Reference to the collider
    public CapsuleCollider barrelCollider;
    public Transform barrelPosition;
    public GameObject barrelObject;
    public float explosionRange;
    [SerializeField] LayerMask affectedLayers;

    //Script with the barrel's health
    public EnemyDamage enemyDamageBarril;

    //Player
    public GameObject player;
    public Player playerScript;

    // Particle Effects
    public ParticleSystem explosionParticles;

    //Barrel shop
    public ShoppingManager shoppingManager;
    public bool isActive;

    //Damage
    [SerializeField] int explosionDamage = 5, explosionKnockBack = 10;

    //Sound Design
    [SerializeField] AudioClip préparationExplosion, explosion;
    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        // Script Shoping Manager Julia
        shoppingManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShoppingManager>();
        if (shoppingManager.BarrelsWereBought || isActive)
        {
            gameObject.GetComponent<Transform>().localScale = Vector3.one;
        }
        else
        {
            gameObject.GetComponent<Transform>().localScale = Vector3.zero;
        }

        // Mes Scripts d'explosion
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        enemyDamageBarril = GetComponent<EnemyDamage>();
        enemyDamageBarril.isTrap = true;
        explosionParticles.Stop();
    }


    void Update()
    { 
        if (enemyDamageBarril.currentHP <= 0)
        {
            Debug.Log("Called Explosion");
            StartCoroutine(ExplosionCountdown());
        }
    }

    void Explosion()
    {
        explosionParticles.Play();

        Collider[] objects = Physics.OverlapSphere(barrelPosition.position, explosionRange, affectedLayers);

        foreach (Collider obj in objects)
        {
            if (obj.CompareTag("Player"))
            {
                playerScript.PlayerDamage(explosionDamage, transform.position, -10, 0.1f);
            }
            if (obj.GetComponent<EnemyDamage>())
            {
                obj.GetComponent<EnemyDamage>().Damage(explosionDamage, explosionKnockBack, barrelPosition);
            }
        }
        barrelObject.SetActive(false);
    }

    IEnumerator ExplosionCountdown()
    {
        FindObjectOfType<AudioManager>().Play("Préparation Explosion");
        audioSource.clip = préparationExplosion;
        audioSource.Play();
        Debug.Log("I AM ABOUT TO EXPLODE!!!");
        yield return new WaitForSeconds(2f);
        FindObjectOfType<AudioManager>().Play("Explosion Barril");
        audioSource.clip = explosion;
        audioSource.Play();
        Explosion();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(barrelPosition.position, explosionRange);
    }
}
