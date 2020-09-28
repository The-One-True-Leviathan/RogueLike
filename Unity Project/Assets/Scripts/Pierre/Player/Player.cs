using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    public LayerMask layerEnemies;
    Rigidbody rigidbody;
    public EnchantmentManager enchant;


    public int absoluteMaxHealth;
    public int maxHealth;
    public float health;
    public float damageImmunity;

    Controler controller;
    public bool A, B, Y, X;
    public Vector3 rStick, lStick, lastDirection, normalizedLStick;


    public Vector3 currentSpeed, targetSpeed;
    float xVelocity, zVelocity;
    public float maxSpeed = 10f, accelerationTime = 0.3f;





    public bool isInRoll;
    public float rollLength, rollRecover, rollSpeed;
    public Vector3 rollDirection;




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
        weapon1.InitializeWeapon();
        if (weapon2 != null)
        {
            weapon2.InitializeWeapon();
        }
        enchant = GetComponent<EnchantmentManager>();
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
        /*enchant.DoEnchants(weapon1, 0);
        if (dualWielding)
        {
        enchant.DoEnchants(weapon2, 0);
        }*/
        //weapon1.DoSpecial(0);
        //weapon2.DoSpecial(0);
        Inputs();
        if (A && !isInRoll && !isInRecover)
        {
            Roll();
        }
        

        if (!isInAttack && !isInCooldown)
        {
            if (X)
            {
                Attack(weapon1, 0);
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

    public void Roll()
    {
        isInRoll = true;
        isInRecover = true;
        StartCoroutine("RollCoroutine");

    }

    public IEnumerator RollCoroutine()
    {
        targetSpeed = rollDirection * rollSpeed;
        yield return new WaitForSeconds(rollLength);
        isInRoll = false;
        yield return new WaitForSeconds(rollRecover);
        isInRecover = false;
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

    public void PlayerDamage(int amount)
    {
        if (!isInImmunity && !isInRoll)
        {
            health -= amount;
            enchant.DoEnchants(weapon1, 3);
            if (dualWielding) { enchant.DoEnchants(weapon2, 3); }
            Immunity(damageImmunity);
        }
    }

    public void Immunity(float duration)
    {
        if (!isInImmunity)
        {
            StartCoroutine("ImmunityCoroutine", duration);
        }
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
        normalizedLStick = lStick.normalized; 

        if (!(rStick == Vector3.zero))
        {
            lastDirection = rStick;
        }

        if (normalizedLStick != Vector3.zero)
        {
            lastDirection = normalizedLStick;
        }


        attackDirection = lastDirection;
        if (!(rStick == Vector3.zero))
        {
            attackDirection = rStick;
        }
        Debug.DrawRay(transform.position, attackDirection, Color.red);

        if (!isInRoll)
        {
            rollDirection = lastDirection;
            targetSpeed = lStick * maxSpeed;
        }
        Debug.DrawRay(transform.position, targetSpeed, Color.blue);
    }

    public void Move()
    {

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
            yield return new WaitForSeconds(weapon.atk[atkNumber].buildup*weapon.totalBuildupMultiplier);
        } else
        {
            yield return new WaitForSeconds(weapon.atk[atkNumber].chargeTime[0]*weapon.totalBuildupMultiplier);
            print("attack charge 0");
            if (!X)
            {
                chargeLevel = 0;
                yield return new WaitForSeconds((weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]) * weapon.totalBuildupMultiplier);
            }
            else
            {
                yield return new WaitForSeconds((weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]) * weapon.totalBuildupMultiplier);
                print("attack charge 1");
                if (!X)
                {
                    chargeLevel = 1;
                }
                else
                {
                    yield return new WaitForSeconds((weapon.atk[atkNumber].chargeTime[2] - weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]) * weapon.totalBuildupMultiplier);
                    chargeLevel = 2;
                    print("attack charge 2");
                }
            }
        }
        print("attack");
        isInHitSpan = true;
        weaponInHitSpan = weapon;
        hitSpanDamage = (weapon.atk[atkNumber].damage[chargeLevel]) * weapon.totalDamageMultiplier;
        isInBuildup = false;
        isInRecover = true;
        isInCooldown = true;
        yield return new WaitForSeconds((weapon.atk[atkNumber].hitSpan[chargeLevel])*weapon.totalBuildupMultiplier);
        isInHitSpan = false;
        yield return new WaitForSeconds((weapon.atk[atkNumber].recover[chargeLevel] - weapon.atk[atkNumber].hitSpan[chargeLevel])* weapon.totalBuildupMultiplier);
        print("recover");
        isInRecover = false;
        isInAttack = false;
        yield return new WaitForSeconds((weapon.atk[atkNumber].cooldown[chargeLevel] - weapon.atk[atkNumber].recover[chargeLevel])*weapon.totalBuildupMultiplier);
        print("cooldown");
        isInCooldown = false;
    }

    public void HitSpan(WeaponScriptableObject weapon, float damage, int atkNumber)
    {
        if(weapon.atk[atkNumber].reach[chargeLevel] != Vector3.zero)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, weapon.atk[atkNumber].reach[chargeLevel].z * weapon.totalReachMultiplier.z, layerEnemies);
            foreach (Collider enemy in hitEnemies)
            {
                print("hitspan");
                if (!enemiesHitLastAttack.Contains(enemy.gameObject))
                {
                    Vector3 enemyDirection = enemy.transform.position - transform.position;
                    if (enemyDirection.magnitude < clostestEnemyDistance)
                    {
                        closestEnemyHitLastAttack = enemy.gameObject;
                    }
                    float enemyAngle = Vector3.Angle(attackDirection, enemyDirection);
                    print(enemyAngle);
                    if (enemyAngle <= weapon.atk[atkNumber].reach[chargeLevel].x*weapon.totalReachMultiplier.x)
                    {
                        enchant.DoEnchants(weapon, 1);
                        Debug.DrawRay(transform.position, enemyDirection, Color.red);
                        print("Enemy hit ! Inflicted " + damage + " damage !");
                    }
                }
                enemiesHitLastAttack.Add(enemy.gameObject);

            }

        }

        if (weapon.atk[atkNumber].isRanged)
        {
            Instantiate(weapon.atk[atkNumber].projectile[chargeLevel]);
        }
    }
}
