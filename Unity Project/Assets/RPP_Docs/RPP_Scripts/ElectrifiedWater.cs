using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedWater : MonoBehaviour
{
    public Transform poolCenter;
    public Vector3 wetZone;
    public LayerMask currentLayer;
    public int electricDamage;
    bool canTakeDamage, canStartSound;

    //Player
    public GameObject player;
    public Player playerScript;

    private void Start()
    {
        wetZone = transform.localScale;
        canTakeDamage = true;
        canStartSound = true;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (canTakeDamage)
        {
            StartCoroutine(ElectricDamage());
        }        
    }

    void ConstantDamage()
    {
        Collider[] objects = Physics.OverlapBox(poolCenter.position, wetZone/2, Quaternion.identity, currentLayer);

        foreach (Collider obj in objects)
        {
            if (obj.CompareTag("Player"))
            {
                Debug.Log("Player is Taking Eletric Damage");
                playerScript.PlayerDamage(electricDamage);
            }
            if (obj.GetComponent<EnemyDamage>())
            {
                Debug.Log("Ennemy is Taking Eletric Damage");
                obj.GetComponent<EnemyDamage>().Damage(electricDamage, 0, poolCenter);
            }
        }        
    }

    IEnumerator ElectricDamage()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1f);
        ConstantDamage();
        canTakeDamage = true;
    }
   private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(poolCenter.position, wetZone);
    }
}
