using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float maxHP, currentHP;
    public Player player;
    public Rigidbody rigidbody;
    public bool isInKnockback;
    float refKnockbackx, refKnockbacky, refKnockbackz;
    public float knockbackSpeed, knockbackResistance = 1;
    public Vector3 knockbackDirection, currentVelocity, targetVelocity;
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
            currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, Vector3.zero.x, ref refKnockbackx, 0.2f);
            currentVelocity.z = Mathf.SmoothDamp(currentVelocity.z, Vector3.zero.z, ref refKnockbackz, 0.2f);
            rigidbody.velocity = currentVelocity;
        }
    }

    public IEnumerator Knockback()
    {
        currentVelocity = knockbackDirection * knockbackSpeed / knockbackResistance;
        isInKnockback = true;
        yield return new WaitForSeconds(0.3f);
        isInKnockback = false;
    }

    public void Damage(float damage, float knockback, Transform knockbackOrigin)
    {
        knockbackDirection = transform.position - knockbackOrigin.position;
        knockbackSpeed = knockback;
        currentHP -= damage;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        if (currentHP <= 0)
        {
            player.latestEnemyKilled = this.gameObject;
            player.KillEnchant();
            Object.Destroy(this.gameObject);
        }
        StopAllCoroutines();
        StartCoroutine("Knockback");
    }
}
