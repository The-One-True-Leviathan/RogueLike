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

    //Script with the barrel's health
    public EnemyDamage enemyDamage;

    //Player
    public GameObject player;
    public Player playerScript;

    //Barrel shop
    public ShoppingManager shoppingManager;

    //Damage
    [SerializeField] private int explosionDamage = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        shoppingManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShoppingManager>();
        enemyDamage = GetComponent<EnemyDamage>();
        enemyDamage.isTrap = true;
        if (shoppingManager.BarrelsWereBought)
        {
            gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
        }
        else
        {
            gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }


    void Update()
    {
        if (enemyDamage.currentHP <= 0)
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
        Debug.Log("I AM ABOUT TO EXPLODE!!!");
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
