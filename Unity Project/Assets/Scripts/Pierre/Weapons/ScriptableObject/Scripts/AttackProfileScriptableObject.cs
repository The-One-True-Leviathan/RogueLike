using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAttackProfile", menuName = "Pierre/Weapon/Attack Profile", order = 0)]
public class AttackProfileScriptableObject : ScriptableObject
{
    public bool isHeavy; //Is the attack heavy ? IE, can the player turn, roll or move during the buildup ?

    public bool isRanged; //Is the attack a projectile ?
    public GameObject[] projectile; //What does the attack fire exactly ?

    public Vector3[] reach; //Informs the size of the attack in 3 dimensions, generally set to 0 in ranged attacks (but not necessarily). X is equal to the angle of "leeway", Z is equal to the length of the attack

    public float buildup;

    public bool isCharge; //Can the attack be charged ?
    public float[] chargeTime; //Charge 0 is the time after which the attack can be used, charge 1 and 2 are two steps of more powerful attacks

    public float[] hitSpan, recover, cooldown; //Hitspan is the time during which the hitbox persists, or the lifetime of the projectile, if any
                                                           //Recover is the time after the buildup during which the character cannot roll or interact with the environment
                                                           //Cooldown is the time after the buildup during which the character cannot attack again
    public float[] damage; //The number of HP reduced from the enemy health
    public float[] knockBack; //How forcefully is the enemy thrown back
}
