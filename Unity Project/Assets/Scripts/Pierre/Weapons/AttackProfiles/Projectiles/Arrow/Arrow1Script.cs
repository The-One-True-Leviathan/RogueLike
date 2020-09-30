﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Arrow1Script : MonoBehaviour
{

    public GameObject playerObject;
    public Player playerScript;
    public WeaponScriptableObject weaponParent;
    public LayerMask enemy;
    public LayerMask environment;
    public float size, speed, damage, knockback, lifeTime;
    public int pierceMax;
    int pierce;
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
                enemy.GetComponent<EnemyDamage>().Damage(damage, knockback, transform);
                playerScript.closestEnemyHitLastAttack = enemy.gameObject;
            }
            playerScript.AttackEnchant(weaponParent);
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
