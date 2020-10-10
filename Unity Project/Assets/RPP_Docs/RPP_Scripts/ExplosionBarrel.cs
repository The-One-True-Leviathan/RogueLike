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
    public LayerMask currentLayer;

    //Player
    public GameObject player;
    public Player playerScript;

    //Damage
    [SerializeField] private int explosionDamage = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }


    void Update()
    {
        OnTriggerEnter(barrelCollider);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ExplosionCountdown());
        }
    }

    void Explosion()
    {
        Collider[] objects = Physics.OverlapSphere(barrelPosition.position, explosionRange, currentLayer);

        foreach(Collider obj in objects)
        {
            if (obj.CompareTag("Player"))
            {
                playerScript.PlayerDamage(explosionDamage);
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
