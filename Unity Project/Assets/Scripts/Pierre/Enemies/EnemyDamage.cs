using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float maxHP, currentHP, currentDamage;
    public Player player;
    public Rigidbody rigidBody;
    public bool isInKnockback;
    float refKnockbackx, refKnockbacky, refKnockbackz;
    public float knockbackSpeed, knockbackResistance = 1;
    public Vector3 knockbackDirection, currentVelocity, targetVelocity;
    public bool isTrap = false, isEnvironment = false, isTable = false, isBoss = false;
    public Rewind rewind;
    public Material dmgShader, trueMaterial;
    public SpriteRenderer objectSprite;

    //Gestion du loot
    public bool hasLoot;
    public List<GameObject> possibleLoots;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody>();
        rewind = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Rewind>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInKnockback)
        {
            currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, Vector3.zero.x, ref refKnockbackx, 0.2f);
            currentVelocity.z = Mathf.SmoothDamp(currentVelocity.z, Vector3.zero.z, ref refKnockbackz, 0.2f);
            rigidBody.velocity = currentVelocity;
        }
    }

    public IEnumerator Knockback()
    {
        currentVelocity = knockbackDirection * knockbackSpeed / knockbackResistance;
        isInKnockback = true;
        yield return new WaitForSeconds(0.3f);
        isInKnockback = false;
    }

    public IEnumerator DamageFeedback()
    {
        objectSprite.material = dmgShader;
        yield return new WaitForSeconds(0.1f);
        objectSprite.material = trueMaterial;
    }

    public void Damage(float damage, float knockback, Transform knockbackOrigin)
    {
        Damage(damage, knockback, knockbackOrigin, false);
    }
    public void Damage(float damage, float knockback, Transform knockbackOrigin, bool barrel)
    {
        if (barrel)
        {
            currentDamage = damage;
            knockbackDirection = transform.position - knockbackOrigin.position;
            knockbackSpeed = knockback;
            currentHP -= damage;
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
            if (currentHP <= 0 && !isTrap && !isEnvironment)
            {
                player.latestEnemyKilled = this.gameObject;
                player.KillEnchant();
                rewind.EnnemyIsKilled(); //Augmente le RewindCounter
                if (hasLoot)
                {
                    int index = Random.Range(0, possibleLoots.Count - 1);
                    Instantiate(possibleLoots[index], transform.position, Quaternion.identity);
                }
                Object.Destroy(this.gameObject);
                Time.timeScale = 1;
            }
            //StopAllCoroutines();
            StartCoroutine("Knockback");
        }
        else
        {
            if (isBoss)
            {
                Debug.Log("Bounced on Boss's armor");
            }
            else
            {
                Damage(damage, knockback, knockbackOrigin, true);
                if (!isTrap)
                {
                    StartCoroutine(DamageFeedback());
                }
            }
        }
    }


}
