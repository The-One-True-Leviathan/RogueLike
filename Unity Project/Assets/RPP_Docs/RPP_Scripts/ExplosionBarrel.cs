using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBarrel : MonoBehaviour
{
    //Reference to the collider
    public CapsuleCollider barrelCollider;
    public Transform barrelPosition;
    public GameObject barrelObject;
    public float explosionRange;
    bool barrelIsIntact;
    [SerializeField] LayerMask affectedLayers;

    //Script with the barrel's health
    [SerializeField] EnemyDamage enemyDamage;
    [SerializeField] float barrelHP;

    //Player
    public GameObject player;
    public Player playerScript;

    // Particle Effects
    public GameObject explosionParticles;

    //Barrel shop
    public ShoppingManager shoppingManager;

    //Damage
    [SerializeField] int explosionDamage = 5, explosionKnockBack = 10;

    void Awake()
    {
        // Script Shoping Manager Julia
        shoppingManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShoppingManager>();

        if(shoppingManager.BarrelsWereBought == true)
        {
            explosionDamage = explosionDamage * 2;
        }

        // Mes Scripts d'explosion
        if (enemyDamage.currentHP > 0)
        {
            barrelIsIntact = true;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        enemyDamage = GetComponent<EnemyDamage>();
        enemyDamage.isTrap = true;
        explosionParticles.SetActive(false);      
    }


    void Update()
    {
        barrelHP = enemyDamage.currentHP;  

        if (barrelHP <= 0 && barrelIsIntact == true)
        {
            Debug.Log("Called Explosion");
            StartCoroutine(ExplosionCountdown());
            barrelIsIntact = false;
        }
    }

    void Explosion()
    {
        explosionParticles.SetActive(true);

        Collider[] objects = Physics.OverlapSphere(barrelPosition.position, explosionRange, affectedLayers);

        foreach(Collider obj in objects)
        {
            if (obj.CompareTag("Player"))
            {
                playerScript.PlayerDamage(explosionDamage);      
            }
            if (obj.GetComponent<EnemyDamage>())
            {
                obj.GetComponent<EnemyDamage>().Damage(explosionDamage, explosionKnockBack, barrelPosition, true);
            }
        }
        barrelObject.SetActive(false);
    }

    IEnumerator ExplosionCountdown()
    {
        Debug.Log("I AM ABOUT TO EXPLODE!!!");
        yield return new WaitForSeconds(2f);
        Explosion();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(barrelPosition.position, explosionRange);
    }
}
