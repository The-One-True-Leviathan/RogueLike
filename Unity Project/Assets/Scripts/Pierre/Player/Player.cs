﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    //Appels de composants nécessaires
    public LayerMask layerEnemies;
    Rigidbody rigidBody;
    public EnchantmentManager enchant;
    public HealthBar healthBar;

    public float damageImmunity; //Longeur (en secondes) de l'immunité après avoir prit des dégâts

    Controler controller; //Appel du controlleur
    public bool south, secondaryAtk, north, mainAtk;
    public Vector3 rStick, lStick, lastDirection, normalizedLStick;


    public Vector3 currentSpeed, targetSpeed;
    float xVelocity, zVelocity;
    public float maxSpeed = 10f, accelerationTime = 0.3f;


    public Collider[] allInteractibleInRange;
    public float interactRange, interactAngle;
    public LayerMask interactible;



    public bool isInRoll;
    public float rollLength, rollRecover, rollSpeed;
    public Vector3 rollDirection;




    public bool dualWielding; //Is the character wielding two different weapons ?
    public float switchTime;
    public WeaponScriptableObject weapon1, weapon2, weaponInAtk, weaponInHitSpan, switchSpace; //Weapon 1 and 2 are the two "hands" of the player, weaponInHitSpan is used for multi-frame attacks, and switchSpace is only used 
                                                                                               //when switching weapons in both hands
    public AttackProfileScriptableObject profileInUse;
    public bool isInBuildup, isInCharge, isInAttack, isInRecover, isInCooldown, isInHitSpan, isInImmunity, hasShot, isInHeavyAtk;
    public float hitSpanDamage;
    public int hitSpanAtkNumber, chargeLevel;
    public Vector3 attackDirection;
    public List<GameObject> enemiesHitLastAttack, enemiesHitLastAttackRanged;
    public GameObject closestEnemyHitLastAttack, latestEnemyKilled;
    public float clostestEnemyDistance;
    // Start is called before the first frame update
    void Start()
    {
        //healthBar = GameObject.FindGameObjectWithTag("HUD").GetComponent<HealthBar>();
        weapon1.InitializeWeapon();
        if (weapon2 != null)
        {
            weapon2.InitializeWeapon();
        }
        enchant = GetComponent<EnchantmentManager>();
        rigidBody = GetComponent<Rigidbody>();
        controller = new Controler();
        controller.Enable();
        lastDirection = Vector3.forward;
        controller.Keyboard.Attack1.started += ctx => mainAtk = true;
        controller.Keyboard.Attack1.canceled += ctx => mainAtk = false;


        controller.Keyboard.Attack2.started += ctx => secondaryAtk = true;
        controller.Keyboard.Attack2.canceled += ctx => secondaryAtk = false;

        controller.Keyboard.Switch.started += ctx => north = true;
        controller.Keyboard.Switch.canceled += ctx => north = false;

        controller.Keyboard.Roll.started += ctx => south = true;
        controller.Keyboard.Roll.canceled += ctx => south = false;

    }


    // Update is called once per frame
    void Update()
    {
        /*enchant.DoEnchants(weapon1, 0);
        if (dualWielding)
        {
        enchant.DoEnchants(weapon2, 0);
        }*/
        Inputs();
        if (south && !isInRoll && !isInRecover)
        {
            Roll();
        }


        if (!isInAttack && !isInCooldown)
        {
            if (mainAtk)
            {
                Attack(weapon1, 0);
            }

            if (secondaryAtk)
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

        if (dualWielding && north && !isInCooldown)
        {
            Switch();
        }

        InteractSphere();
        if (controller.Keyboard.Interact.triggered)
        {
            InteractAction();
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
        healthBar.ApplyDamage(-amount);
    }

    public void IncreaseMaxHealth(float amount)
    {
        healthBar.UpgradeLife(amount);
    }

    public void PlayerDamage(float amount)
    {
        if (!isInImmunity && !isInRoll)
        {
            healthBar.ApplyDamage(amount);
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
        //mainAtkLong = controller.Keyboard.Attack1.;
        rStick = new Vector3(controller.Keyboard.LookAround.ReadValue<Vector2>().x, 0, controller.Keyboard.LookAround.ReadValue<Vector2>().y);
        lStick = new Vector3(controller.Keyboard.Movement.ReadValue<Vector2>().x, 0, controller.Keyboard.Movement.ReadValue<Vector2>().y);
        rStick.Normalize();
        normalizedLStick = lStick.normalized;

        if (!(rStick == Vector3.zero) && !isInHeavyAtk)
        {
            lastDirection = rStick;
        }

        if (!isInHeavyAtk)
        {
            if (normalizedLStick != Vector3.zero)
            {
                lastDirection = normalizedLStick;
            }

            attackDirection = lastDirection;
            if (!(rStick == Vector3.zero))
            {
                attackDirection = rStick;
            }
        }
        Debug.DrawRay(transform.position, attackDirection, Color.red);

        if (!isInRoll)
        {

            rollDirection = lastDirection;
            targetSpeed = Vector3.ClampMagnitude(lStick, 1) * maxSpeed;
        }
        Debug.DrawRay(transform.position, targetSpeed, Color.blue);
    }

    public void Move()
    {

        currentSpeed.x = Mathf.SmoothDamp(currentSpeed.x, targetSpeed.x, ref xVelocity, accelerationTime);
        currentSpeed.z = Mathf.SmoothDamp(currentSpeed.z, targetSpeed.z, ref zVelocity, accelerationTime);
        if (isInHeavyAtk)
        {
            currentSpeed = Vector3.zero;
            targetSpeed = Vector3.zero;
            xVelocity = zVelocity = 0;
        }

        rigidBody.velocity = currentSpeed;
        if (isInCharge)
        {
            rigidBody.velocity = currentSpeed / 3;
        }
    }

    public void Attack(WeaponScriptableObject weapon, int atkNumber)
    {
        // (!weapon.atk1Charge)
        //{
        enemiesHitLastAttack.Clear();
        enemiesHitLastAttackRanged.Clear();
        clostestEnemyDistance = Mathf.Infinity;
        weaponInAtk = weapon;
        isInAttack = true;
        hasShot = false;
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
        isInBuildup = true;
        chargeLevel = 0;
        if (weapon.atk[atkNumber].isHeavy)
        {
            isInHeavyAtk = true;
        }
        if (!weapon.atk[atkNumber].isCharge)
        {
            yield return new WaitForSeconds(weapon.atk[atkNumber].buildup * weapon.totalBuildupMultiplier);
        } else
        {
            isInCharge = true;
            yield return new WaitForSeconds(weapon.atk[atkNumber].chargeTime[0] * weapon.totalBuildupMultiplier);
            print("attack charge 0");
            if (!mainAtk && !secondaryAtk)
            {
                chargeLevel = 0;
                yield return new WaitForSeconds((weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]) * weapon.totalBuildupMultiplier);
            }
            else
            {
                yield return new WaitForSeconds((weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]) * weapon.totalBuildupMultiplier);
                print("attack charge 1");
                if (!mainAtk && !secondaryAtk)
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
        enchant.DoEnchants(weapon, 4);
        weaponInHitSpan = weapon;
        hitSpanDamage = (weapon.atk[atkNumber].damage[chargeLevel]) * weapon.totalDamageMultiplier;
        isInBuildup = false;
        isInRecover = true;
        isInCooldown = true;
        yield return new WaitForSeconds((weapon.atk[atkNumber].hitSpan[chargeLevel]) * weapon.totalBuildupMultiplier);
        isInHitSpan = false;
        isInHeavyAtk = false;
        isInCharge = false;
        yield return new WaitForSeconds((weapon.atk[atkNumber].recover[chargeLevel] - weapon.atk[atkNumber].hitSpan[chargeLevel]) * weapon.totalBuildupMultiplier);
        print("recover");
        isInRecover = false;
        isInAttack = false;
        yield return new WaitForSeconds((weapon.atk[atkNumber].cooldown[chargeLevel] - weapon.atk[atkNumber].recover[chargeLevel]) * weapon.totalBuildupMultiplier);
        print("cooldown");
        isInCooldown = false;
    }

    public void HitSpan(WeaponScriptableObject weapon, float damage, int atkNumber)
    {
        if (weapon.atk[atkNumber].reach[chargeLevel] != Vector3.zero)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, weapon.atk[atkNumber].reach[chargeLevel].z * weapon.totalReachMultiplier.z, layerEnemies);
            foreach (Collider enemy in hitEnemies)
            {

                print("hitspan");
                if (!enemiesHitLastAttack.Contains(enemy.gameObject))
                {
                    Vector3 enemyDirection = enemy.transform.position - transform.position;

                    float enemyAngle = Vector3.Angle(attackDirection, enemyDirection);
                    print(enemyAngle);
                    if (enemyAngle <= weapon.atk[atkNumber].reach[chargeLevel].x * weapon.totalReachMultiplier.x)
                    {
                        if (enemyDirection.magnitude < clostestEnemyDistance)
                        {
                            clostestEnemyDistance = enemyDirection.magnitude;
                            closestEnemyHitLastAttack = enemy.gameObject;
                        }
                        enemiesHitLastAttack.Add(enemy.gameObject);
                        Debug.DrawRay(transform.position, enemyDirection, Color.red);
                        print("Enemy hit ! Inflicted " + damage + " damage !");
                        float finalKnockback = weapon.atk[atkNumber].knockBack[chargeLevel] * weapon.totalKnockbackMultiplier;
                        DoAttack(damage, finalKnockback, enemy.gameObject);
                    }
                }


            }
            AttackEnchant(weapon);
            Debug.LogError("Number of Enemies hit : " + enemiesHitLastAttack.Count);
        }

        if (weapon.atk[atkNumber].isRanged)
        {
            if (!hasShot)
            {
                Instantiate(weapon.atk[atkNumber].projectile[chargeLevel], transform.position, Quaternion.LookRotation(attackDirection, Vector3.up));
                hasShot = true;
            }
        }
    }
    
    public void KillEnchant ()
    {
        enchant.DoEnchants(weapon1, 2);
        if (dualWielding)
        {
            enchant.DoEnchants(weapon2, 2);
        }
    }

    public void DoAttack(float damage, float knockback, GameObject enemy)
    {
        EnemyDamage enemyDamage = enemy.GetComponent<EnemyDamage>();
        enemyDamage.Damage(damage, knockback, transform);
    }

    public void AttackEnchant(WeaponScriptableObject weapon)
    {
        for (int i = 0; i < enemiesHitLastAttack.Count; i++) 
        {
            enchant.DoEnchants(weapon, 1);
        }
    }
    
    public void InteractSphere()
    {
        allInteractibleInRange = Physics.OverlapSphere(transform.position, interactRange, interactible);
        foreach (Collider interactible in allInteractibleInRange)
        {
            interactible.GetComponent<InteractibleBehavior>().interactible = true;
        }
    }

    public void InteractAction()
    {
        if (!allInteractibleInRange.Count().Equals(0))
        {
            if (allInteractibleInRange.Count().Equals(1))
            {
                allInteractibleInRange[0].GetComponent<InteractibleBehavior>().interacted = true;
            } 
            else
            {
                float smallestAngle = Mathf.Infinity;
                Collider interactionTarget = allInteractibleInRange[0];
                foreach (Collider interactible in allInteractibleInRange)
                {
                    Vector3 playerToInteractible = interactible.transform.position - transform.position;
                    float interactibleAngle = Vector3.Angle(attackDirection, playerToInteractible);
                    if (interactibleAngle < smallestAngle)
                    {
                        interactionTarget = interactible;
                        smallestAngle = interactibleAngle;
                    }
                }
                interactionTarget.GetComponent<InteractibleBehavior>().interacted = true;
            }
        }
    }
}
