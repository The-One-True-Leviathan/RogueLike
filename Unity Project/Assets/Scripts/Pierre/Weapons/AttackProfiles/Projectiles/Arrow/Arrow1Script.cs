using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Arrow1Script : MonoBehaviour
{

    public GameObject playerObject;
    public Player playerScript;
    public WeaponScriptableObject weaponParent;
    public LayerMask enemy, enchant;
    public LayerMask environment;
    public float size, speed, damage, knockback, lifeTime;
    public int pierceMax;
    int pierce;
    public AudioClip spikesUp, spikesDown;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponent<Player>();
        weaponParent = playerScript.weaponInAtk;
        StartCoroutine("LifeCycle");
        pierce = pierceMax;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Collider[] hitenemy =  Physics.OverlapSphere(transform.position, size, enemy);
        foreach (Collider enemy in hitenemy)
        {
            if (!playerScript.enemiesHitLastAttackRanged.Contains(enemy.gameObject))
            {
                Vector3 enemyDirection = enemy.transform.position - playerObject.transform.position;
                if (!playerScript.enemiesHitLastAttack.Contains(enemy.gameObject))
                {
                    playerScript.enemiesHitLastAttack.Add(enemy.gameObject);
                }
                playerScript.enemiesHitLastAttackRanged.Add(enemy.gameObject);
                pierce--;
                enemy.GetComponent<EnemyDamage>().Damage(damage * weaponParent.totalDamageMultiplier, knockback * weaponParent.totalKnockbackMultiplier, transform);
              //  Debug.LogError("Enemy Hit with ranged, dealt " + damage*weaponParent.totalDamageMultiplier);
                playerScript.closestEnemyHitLastAttack = enemy.gameObject;
            }
            playerScript.AttackEnchant(weaponParent);
        }
        Collider[] hitenchant = Physics.OverlapSphere(transform.position, size, enchant);
        foreach (Collider enchant in hitenchant)
        {
            enchant.GetComponent<EnchantItemBehavior>().Attacked(weaponParent);
        }
        if (pierce <= 0)
        {
            Object.Destroy(this.gameObject);
        }

    }

    public IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifeTime);
        Object.Destroy(this.gameObject);
    }
}
