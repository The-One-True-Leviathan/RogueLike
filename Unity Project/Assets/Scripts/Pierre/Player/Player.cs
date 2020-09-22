using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    public LayerMask layerEnemies;


    Controler controller;
    bool A, B, Y, X;
    public Vector3 rStick, lStick, lastDirection;


    float currentSpeed, maxSpeed, accelerationTime;







    public bool dualWielding; //Is the character wielding two different weapons ?
    public WeaponScriptableObject weapon1, weapon2, weaponInHitSpan, switchSpace; //Weapon 1 and 2 are the two "hands" of the player, weaponInHitSpan is used for multi-frame attacks, and switchSpace is only used 
                                                                                  //when switching weapons in both hands
    public bool isInBuildup, isInCharge, isInAttack, isInRecover, isInCooldown, isInHitSpan;
    public Vector3 attackDirection;
    // Start is called before the first frame update
    void Start()
    {
        controller = new Controler();
        controller.Enable();
        lastDirection = Vector3.forward;

    }

    // Update is called once per frame
    void Update()
    {
        X = controller.Keyboard.Attack1.triggered;
        B = controller.Keyboard.Attack2.triggered;

        rStick = new Vector3(controller.Keyboard.LookAround.ReadValue<Vector2>().x, 0, controller.Keyboard.LookAround.ReadValue<Vector2>().y);
        lStick = new Vector3(controller.Keyboard.Movement.ReadValue<Vector2>().x, 0, controller.Keyboard.Movement.ReadValue<Vector2>().y);
        rStick.Normalize();
        lStick.Normalize();

        if (!isInAttack && !isInCooldown)
        {
            if (X)
            {
                Attack(weapon1);
            }
        }

        
        /*  if (!isInAttack && !isInCooldown) 
         *  {
         *      if(bouton attaquer 1)
         *      {
         *          Attack(weapon1);
         *      }
         *  
         *      if(bouton attaquer 2)
         *      {
         *          if (dualWielding)
         *          {
         *              Attack(weapon2);
         *          } 
         *          else 
         *          {
         *              Attack(weapon1);
         *          }
         *      }
         *  }
         *  
         *  if (isInHitSpan) 
         *  {
         *      HitSpan(weaponInHitSpan);
         *  }
         */
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
        attackDirection.Normalize();
        Debug.DrawRay(transform.position, attackDirection, Color.red);



        if (X)
        {
            HitSpan(weaponInHitSpan);
        }
    }

    public void Attack(WeaponScriptableObject weapon)
    {

    }

    IEnumerator ResolveAttack(WeaponScriptableObject weapon, bool main)
    {
        print("start attack");
        yield return new WaitForSeconds(weapon.atk1Buildup);
        print("attack");
        isInHitSpan = true;
        weaponInHitSpan = weapon;

        yield return new WaitForSeconds(weapon.atk1Recover);
        print("recover");
        yield return new WaitForSeconds(weapon.atk1Cooldown - weapon.atk1Recover);
        print("cooldown");
    }

    public void HitSpan(WeaponScriptableObject weapon)
    {
        if(!weapon.atk1Ranged)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, weapon.atk1Reach.z, layerEnemies);
            foreach (Collider enemy in hitEnemies)
            {
                Vector3 enemyDirection = enemy.transform.position - transform.position;
                Debug.DrawRay(transform.position, enemyDirection, Color.red);
                float enemyAngle = Vector3.Angle(attackDirection, enemyDirection);
                print(enemyAngle);
                if (enemyAngle <= weapon.atk1Reach.x)
                {
                    print("Enemy hit !");
                }

            }
        }
    }
}
