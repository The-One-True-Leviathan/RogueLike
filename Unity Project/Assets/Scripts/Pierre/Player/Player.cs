using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    public LayerMask layerEnemies;
    Rigidbody rigidbody;


    Controler controller;
    public bool A, B, Y, X;
    public Vector3 rStick, lStick, lastDirection;


    public Vector3 currentSpeed;
    float xVelocity, zVelocity;
    public float maxSpeed = 10f, accelerationTime = 0.3f;







    public bool dualWielding; //Is the character wielding two different weapons ?
    public float switchTime;
    public WeaponScriptableObject weapon1, weapon2, weaponInHitSpan, switchSpace; //Weapon 1 and 2 are the two "hands" of the player, weaponInHitSpan is used for multi-frame attacks, and switchSpace is only used 
                                                                                  //when switching weapons in both hands
    public bool isInBuildup, isInCharge, isInAttack, isInRecover, isInCooldown, isInHitSpan;
    public int hitSpanDamage;
    public Vector3 attackDirection;
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
                Attack1(weapon1);
            }

            if (B)
            {
                if (dualWielding)
                {
                    Attack2(weapon2);
                } else
                {
                    Attack2(weapon1);
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
            HitSpan1(weaponInHitSpan, hitSpanDamage);
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

    void Xoff()
    {
        X = false;
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

    public void Attack1(WeaponScriptableObject weapon)
    {
        if (!weapon.atk1Charge)
        {
            StartCoroutine("ResolveAttack1", weapon);
        } else
        {
            StartCoroutine("ChargeAttack1", weapon);
        }
    }

    IEnumerator ResolveAttack1(WeaponScriptableObject weapon)
    {
        print("start attack");
        isInAttack = true;
        isInBuildup = true;
        yield return new WaitForSeconds(weapon.atk1Buildup);
        print("attack");
        isInHitSpan = true;
        weaponInHitSpan = weapon;
        hitSpanDamage = weapon.atk1Damage[0];
        isInBuildup = false;
        isInRecover = true;
        isInCooldown = true;
        yield return new WaitForSeconds(weapon.atk1HitSpan[0]);
        isInHitSpan = false;
        yield return new WaitForSeconds(weapon.atk1Recover[0] - weapon.atk1HitSpan[0]);
        print("recover");
        isInRecover = false;
        isInAttack = false;
        yield return new WaitForSeconds(weapon.atk1Cooldown[0] - weapon.atk1Recover[0]);
        print("cooldown");
        isInCooldown = false;
    }
    IEnumerator ChargeAttack1(WeaponScriptableObject weapon)
    {
        print("start attack");
        isInAttack = true;
        isInBuildup = true;
        int i;
        yield return new WaitForSeconds(weapon.atk1ChargeTime[0]);
        print("attack charge 0");
        if (!X)
        {
            i = 0;
            yield return new WaitForSeconds(weapon.atk1ChargeTime[1] - weapon.atk1ChargeTime[0]);

            
        } 
        else
        {

            yield return new WaitForSeconds(weapon.atk1ChargeTime[1] - weapon.atk1ChargeTime[0]);
            print("attack charge 1");
            if (!X)
            {
                i = 1;
            } 
            else
            {
                yield return new WaitForSeconds(weapon.atk1ChargeTime[2] - weapon.atk1ChargeTime[1] - weapon.atk1ChargeTime[0]);
                i = 2;
                print("attack charge 2");
                
            }
        }
        isInHitSpan = true;
        weaponInHitSpan = weapon;
        hitSpanDamage = weapon.atk1Damage[i];
        isInBuildup = false;
        isInRecover = true;
        isInCooldown = true;
        yield return new WaitForSeconds(weapon.atk1HitSpan[i]);
        isInHitSpan = false;
        yield return new WaitForSeconds(weapon.atk1Recover[i] - weapon.atk1HitSpan[i]);
        print("recover");
        isInRecover = false;
        isInAttack = false;
        yield return new WaitForSeconds(weapon.atk1Cooldown[i] - weapon.atk1Recover[i]);
        print("cooldown");
        isInCooldown = false;
        yield break;
    }
    public void Attack2(WeaponScriptableObject weapon)
    {
        if (!weapon.atk2Charge)
        {
            StartCoroutine("ResolveAttack1", weapon);
        }
        else
        {
            StartCoroutine("ChargeAttack1", weapon);
        }
    }

    IEnumerator ResolveAttack2(WeaponScriptableObject weapon)
    {
        print("start attack");
        isInAttack = true;
        isInBuildup = true;
        yield return new WaitForSeconds(weapon.atk2Buildup);
        print("attack");
        isInHitSpan = true;
        weaponInHitSpan = weapon;
        hitSpanDamage = weapon.atk2Damage[0];
        isInBuildup = false;
        isInRecover = true;
        isInCooldown = true;
        yield return new WaitForSeconds(weapon.atk2HitSpan[0]);
        isInHitSpan = false;
        yield return new WaitForSeconds(weapon.atk2Recover[0] - weapon.atk2HitSpan[0]);
        print("recover");
        isInRecover = false;
        isInAttack = false;
        yield return new WaitForSeconds(weapon.atk2Cooldown[0] - weapon.atk2Recover[0]);
        print("cooldown");
        isInCooldown = false;
    }
    IEnumerator ChargeAttack2(WeaponScriptableObject weapon)
    {
        print("start attack");
        isInAttack = true;
        isInBuildup = true;
        int i;
        yield return new WaitForSeconds(weapon.atk2ChargeTime[0]);
        print("attack charge 0");
        if (!X)
        {
            i = 0;
            yield return new WaitForSeconds(weapon.atk2ChargeTime[1] - weapon.atk2ChargeTime[0]);


        }
        else
        {

            yield return new WaitForSeconds(weapon.atk2ChargeTime[1] - weapon.atk2ChargeTime[0]);
            print("attack charge 1");
            if (!X)
            {
                i = 1;
            }
            else
            {
                yield return new WaitForSeconds(weapon.atk2ChargeTime[2] - weapon.atk2ChargeTime[1] - weapon.atk2ChargeTime[0]);
                i = 2;
                print("attack charge 2");

            }
        }
        isInHitSpan = true;
        weaponInHitSpan = weapon;
        hitSpanDamage = weapon.atk2Damage[i];
        isInBuildup = false;
        isInRecover = true;
        isInCooldown = true;
        yield return new WaitForSeconds(weapon.atk2HitSpan[i]);
        isInHitSpan = false;
        yield return new WaitForSeconds(weapon.atk2Recover[i] - weapon.atk2HitSpan[i]);
        print("recover");
        isInRecover = false;
        isInAttack = false;
        yield return new WaitForSeconds(weapon.atk2Cooldown[i] - weapon.atk2Recover[i]);
        print("cooldown");
        isInCooldown = false;
        yield break;
    }

    public void HitSpan1(WeaponScriptableObject weapon, int damage)
    {
        if(!weapon.atk1Ranged)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, weapon.atk1Reach.z, layerEnemies);
            foreach (Collider enemy in hitEnemies)
            {
                Vector3 enemyDirection = enemy.transform.position - transform.position;
                float enemyAngle = Vector3.Angle(attackDirection, enemyDirection);
                print(enemyAngle);
                if (enemyAngle <= weapon.atk1Reach.x)
                {
                    Debug.DrawRay(transform.position, enemyDirection, Color.red);
                    print("Enemy hit ! Inflicted " + damage + " damage !");
                }

            }
        }
    }
}
