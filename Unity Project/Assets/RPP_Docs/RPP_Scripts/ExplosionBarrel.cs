using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBarrel : MonoBehaviour
{
    //Reference to the collider
    public CapsuleCollider2D barrelCollider;
    public Transform barrelPosition;
    public GameObject barrelObject;
    public float explosionRange;
    public LayerMask currentLayer;

    //Reference to Systems Manager
    public Systems systems;

    //Damage
    [SerializeField] private int explosionDamage = 5;


    void Update()
    {
        OnTriggerEnter2D(barrelCollider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ExplosionCountdown());
        }
    }

    void Explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(barrelPosition.position, explosionRange, currentLayer);

        foreach(Collider2D obj in objects)
        {
            if (obj.CompareTag("Player"))
            {
                systems.TakeDamage(explosionDamage);
            }
        }

    }

    IEnumerator ExplosionCountdown()
    {
        yield return new WaitForSeconds(2f);
        Explosion();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(barrelPosition.position, explosionRange);
    }
}
