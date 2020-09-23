using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    public LayerMask layerEnemies;
    Rigidbody rigidbody;


    public int absoluteMaxHealth;
    public int maxHealth;
    public float health;

    Controler controller;
    public bool A, B, Y, X;
    public Vector3 rStick, lStick, lastDirection;


    public Vector3 currentSpeed;
    float xVelocity, zVelocity;
    public float maxSpeed = 10f, accelerationTime = 0.3f;







    public bool dualWielding; //Is the character wielding two different weapons ?
    public float switchTime;
    public WeaponScriptableObject weapon1, weapon2, weaponInAtk, weaponInHitSpan, switchSpace; //Weapon 1 and 2 are the two "hands" of the player, weaponInHitSpan is used for multi-frame attacks, and switchSpace is only used 
                                                                                               //when switching weapons in both hands
    public AttackProfileScriptableObject profileInUse;
    public bool isInBuildup, isInCharge, isInAttack, isInRecover, isInCooldown, isInHitSpan, isInImmunity;
    public float hitSpanDamage;
    public int hitSpanAtkNumber, chargeLevel;
    public Vector3 attackDirection;
    public List<GameObject> enemiesHitLastAttack;
    public GameObject closestEnemyHitLastAttack;
    public float clostestEnemyDistance;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        controller = new Controler();
        controller.Enable();
        lastDirection = Vector3.forward;
        controller.Keyboard.Attack1.started += ctx => X = true;
        controller.Keyboard.Attack1.canceled += ctx => X = false;


        controller.Keyboard.Attack2.started += ctx => B = true;
        controller.Keyboard.Attack2.canceled += ctx => B = false;

        controller.Keyboard.Switch.started += ctx => Y = true;
        controller.Keyboard.Switch.canceled += ctx => Y = false;

        controller.Keyboard.Roll.started += ctx => A = true;
        controller.Keyboard.Roll.canceled += ctx => A = false;

    }
    

    // Update is called once per frame
    void Update()
    {
        Inputs();
        
        

        if (!isInAttack && !isInCooldown)
        {
            if (X)
            {
                Attack(weapon1, 0);
                weapon1.AttackAction();
            }

            if (B)
            {
                if (dualWielding)
                {
                    Attack(weapon2, 1);
                } else
                {
                    Attack(weapon1, 1);
                }
            }
        }

        if (dualWielding && Y &&!isInCooldown)
        {
            Switch();
        }
        

        Move();
        

        if (isInHitSpan)
        {
            HitSpan(weaponInHitSpan, hitSpanDamage, hitSpanAtkNumber);
        }
    }

    void Switch()
    {
        switchSpace = weapon1;
        weapon1 = weapon2;
        weapon2 = switchSpace;
        StartCoroutine("SwitchTime");
    }

    IEnumerator SwitchTime()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(switchTime);
        isInCooldown = false;
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        if (maxHealth > absoluteMaxHealth)
        {
            maxHealth = absoluteMaxHealth;
        }
    }

    public void Immunity(float duration)
    {
        StartCoroutine("ImmunityCoroutine", duration);
    }

    public IEnumerator ImmunityCoroutine(float duration)
    {
        isInImmunity = true;
        yield return new WaitForSeconds(duration);
        isInImmunity = false;
    }

    public void Inputs()
    {
        //XLong = controller.Keyboard.Attack1.;
        rStick = new Vector3(controller.Keyboard.LookAround.ReadValue<Vector2>().x, 0, controller.Keyboard.LookAround.ReadValue<Vector2>().y);
        lStick = new Vector3(controller.Keyboard.Movement.ReadValue<Vector2>().x, 0, controller.Keyboard.Movement.ReadValue<Vector2>().y);
        rStick.Normalize();
        lStick.Normalize(); 
        
        if (!(rStick == Vector3.zero))
        {
            lastDirection = rStick;
        }

        if (!(lStick == Vector3.zero))
        {
            lastDirection = lStick;
        }

        attackDirection = lastDirection;
        if (!(rStick == Vector3.zero))
        {
            attackDirection = rStick;
        }
        Debug.DrawRay(transform.position, attackDirection, Color.red);
    }

    public void Move()
    {
        Vector3 targetSpeed = lStick * maxSpeed;

        currentSpeed.x = Mathf.SmoothDamp(currentSpeed.x, targetSpeed.x, ref xVelocity, accelerationTime);
        currentSpeed.z = Mathf.SmoothDamp(currentSpeed.z, targetSpeed.z, ref zVelocity, accelerationTime);

        rigidbody.velocity = currentSpeed;
    }

    public void Attack(WeaponScriptableObject weapon, int atkNumber)
    {
        // (!weapon.atk1Charge)
        //{
        enemiesHitLastAttack.Clear();
        clostestEnemyDistance = Mathf.Infinity;
        weaponInAtk = weapon;
        hitSpanAtkNumber = atkNumber;
        StartCoroutine("ResolveAttack", atkNumber);
        //{
            //StartCoroutine("ChargeAttack1", weapon);
        //}
    }

    IEnumerator ResolveAttack(int atkNumber)
    {
        WeaponScriptableObject weapon = weaponInAtk;
        print("start attack");
        isInAttack = true;
        isInBuildup = true;
        chargeLevel = 0;
        if (!weapon.atk[atkNumber].isCharge)
        {
            yield return new WaitForSeconds(weapon.atk[atkNumber].buildup);
        } else
        {
            yield return new WaitForSeconds(weapon.atk[atkNumber].chargeTime[0]);
            print("attack charge 0");
            if (!X)
            {
                chargeLevel = 0;
                yield return new WaitForSeconds(weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]);
            }
            else
            {
                yield return new WaitForSeconds(weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]);
                print("attack charge 1");
                if (!X)
                {
                    chargeLevel = 1;
                }
                else
                {
                    yield return new WaitForSeconds(weapon.atk[atkNumber].chargeTime[2] - weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]);
                    chargeLevel = 2;
                    print("attack charge 2");
                }
            }
        }
        print("attack");
        isInHitSpan = true;
        weaponInHitSpan = weapon;
        hitSpanDamage = weapon.atk[atkNumber].damage[chargeLevel];
        isInBuildup = false;
        isInRecover = true;
        isInCooldown = true;
        yield return new WaitForSeconds(weapon.atk[atkNumber].hitSpan[chargeLevel]);
        isInHitSpan = false;
        yield return new WaitForSeconds(weapon.atk[atkNumber].recover[chargeLevel] - weapon.atk[atkNumber].hitSpan[chargeLevel]);
        print("recover");
        isInRecover = false;
        isInAttack = false;
        yield return new WaitForSeconds(weapon.atk[atkNumber].cooldown[chargeLevel] - weapon.atk[atkNumber].recover[chargeLevel]);
        print("cooldown");
        isInCooldown = false;
    }

    public void HitSpan(WeaponScriptableObject weapon, float damage, int atkNumber)
    {
        if(weapon.atk[atkNumber].reach[chargeLevel] != Vector3.zero)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, weapon.atk[atkNumber].reach[chargeLevel].z, layerEnemies);
            foreach (Collider enemy in hitEnemies)
            {
                enemiesHitLastAttack.Add(enemy.gameObject);
                if (!enemiesHitLastAttack.Contains(enemy.gameObject))
                {
                    Vector3 enemyDirection = enemy.transform.position - transform.position;
                    if (enemyDirection.magnitude < clostestEnemyDistance)
                    {
                        closestEnemyHitLastAttack = enemy.gameObject;
                    }
                    float enemyAngle = Vector3.Angle(attackDirection, enemyDirection);
                    print(enemyAngle);
                    if (enemyAngle <= weapon.atk[atkNumber].reach[chargeLevel].x)
                    {
                        Debug.DrawRay(transform.position, enemyDirection, Color.red);
                        print("Enemy hit ! Inflicted " + damage + " damage !");
                    }
                }

            }

        }

        if (weapon.atk[atkNumber].isRanged)
        {
            Instantiate(weapon.atk[atkNumber].projectile[chargeLevel]);
        }
    }
}
