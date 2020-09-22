using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    public LayerMask layerEnemies;


    Controler controller;












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

    }

    // Update is called once per frame
    void Update()
    {
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
        attackDirection = new Vector3(controller.Keyboard.Movement.ReadValue<Vector2>().x, 0, controller.Keyboard.Movement.ReadValue<Vector2>().y);
        Debug.DrawRay(transform.position, attackDirection, Color.red);
        if (isInHitSpan)
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
