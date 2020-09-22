using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{

    [CreateAssetMenu(fileName = "newWeapon", menuName = "Pierre/Weapon", order = 0)]
    public class WeaponScriptableObject : ScriptableObject
    {
        public Sprite weaponSprite;
        public string weaponName;
        public int weaponID;
        GameObject player;

        //Attack 1
        public bool atk1Heavy; //Is the attack heavy ? IE, can the player turn, roll or move during the buildup ?

        public bool atk1Ranged; //Is the attack a projectile ?
        public GameObject atk1Projectile; //What does the attack fire exactly ?

        public Vector3 atk1Reach; //Informs the size of the attack in 3 dimensions, generally set to 0 in ranged attacks (but not necessarily). X is equal to the angle of "leeway", Z is equal to the length of the attack

        public bool atk1Charge; //Can the attack be charged ?
        public float atk1Charge0, atk1Charge1, atk1Charge2; //Charge 0 is the time after which the attack can be used, charge 1 and 2 are two steps of more powerful attacks

        public float atk1Buildup, atk1HitSpan, atk1Recover, atk1Cooldown; //Hitspan is the time during which the hitbox persists, or the lifetime of the projectile, if any
                                                                          //Recover is the time after the buildup during which the character cannot roll or interact with the environment
                                                                          //Cooldown is the time after the buildup during which the character cannot attack again
        public int atk1Damage; //The number of HP reduced from the enemy health
        public float atk1KnockBack; //How forcefully is the enemy thrown back

    }

}

