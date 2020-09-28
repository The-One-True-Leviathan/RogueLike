using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float maxHP, currentHP;
    public Player player;
    public Rigidbody rigidbody;
    public bool isInKnockback;
    public float knockbackSpeed, knockbackResistance;
    public Vector3 knockbackDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInKnockback)
        {
            rigidbody.velocity = knockbackDirection * knockbackSpeed / knockbackResistance;
        }
    }

    public IEnumerator Knockback()
    {
        yield return new WaitForSeconds(0.2f);
        isInKnockback = false;
    }

    public void Damage(float damage, float knockback, Transform knockbackOrigin)
    {
        currentHP -= damage;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        if (currentHP <= 0)
        {
            player.latestEnemyKilled = this.gameObject;
            player.KillEnchant();
        }
    }
}
