using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "newAttackProfile", menuName = "Pierre/Weapon/Attack Profile", order = 0)]
    public class AttackProfileScriptableObject : ScriptableObject
    {
        public bool[] isHeavy = new bool[2]; //Is the attack heavy ? IE, can the player turn, roll or move during the buildup ?

        public bool isRanged; //Is the attack a projectile ?
        public GameObject[] projectile = new GameObject[1]; //What does the attack fire exactly ?

        public Vector3[] reach = new Vector3[1]; //Informs the size of the attack in 3 dimensions, generally set to 0 in ranged attacks (but not necessarily). X is equal to the angle of "leeway", Z is equal to the length of the attack

        public float buildup;

        public bool isCharge; //Can the attack be charged ?
        public float[] chargeTime = new float[1]; //Charge 0 is the time after which the attack can be used, charge 1 and 2 are two steps of more powerful attacks

        public float[] hitSpan = new float[1], recover = new float[1], cooldown = new float[1]; //Hitspan is the time during which the hitbox persists, or the lifetime of the projectile, if any
                                                   //Recover is the time after the buildup during which the character cannot roll or interact with the environment
                                                   //Cooldown is the time after the buildup during which the character cannot attack again
        public float[] damage = new float[1]; //The number of HP reduced from the enemy health
        public float[] knockBack = new float[1]; //How forcefully is the enemy thrown back
        public int[] animationIndex = new int[1];
        public Vector2[] recoil = new Vector2[1]; //How forcefully the player is knocked back (or forward) by the attack
    }
}
