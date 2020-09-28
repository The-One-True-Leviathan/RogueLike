using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace Weapons
{
    public enum EffectEnchantmentType { None, Heal, Damage, ImmunityChance, Teleport }
    public enum CenterOn { Player, ClosestEnemy, AllEnemies }
    [CreateAssetMenu(fileName = "newEffect", menuName = "Pierre/Enchantment/Effect", order = 0)]
    public class EnchantmentsEffect : ScriptableObject
    {
        GameObject player;
        Player playerScript;

        public GameObject particles;

        public EffectEnchantmentType effectType;
        [Tooltip("Type : 0 = Idle, 1 = Attack, 2 = Kill, 3 = Damage")]
        public int type; //0 = Idle, 1 = Attack, 2 = Kill, 3 = Damage
        [Tooltip("Should the effect use the rng given by the whole enchantment or roll its own ?")]
        public bool useNativeRNG; //Should the effect use the rng given by the whole enchantment or roll his own ?
        public int nativeRNG; //Contains the rng given by the whole enchantment

        public bool isInEffect = false;
        [Tooltip("Minimum time between each activation of the effect")]
        public float effectTicLength;
        public float effectDuration;
        public Vector3 effectReach;
        public bool useWeaponReach;
        public CenterOn centerOn;
        public bool centerEffectOnAllEnemies, centerEffectOnClosestEnemy;
        List<GameObject> target;
        public bool invertDirection;
        public float effectStrength;
        [Tooltip("Int Number to inform the % of chance of the effect happening each time its activated")]
        public int effectChanceLower, effectChanceUpper;
        public LayerMask effectAffectedLayers;
        float direction;
        // Start is called before the first frame update

        public void InitializeEnchantmentEffect()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerScript = player.GetComponent<Player>();
        }
        public void DoEffect()
        {
            if (invertDirection)
            {
                direction = -1f;
            }
            else
            {
                direction = 1f;
            }
            Debug.LogWarning("Spécial 1");
            if (!useNativeRNG)
            {
                Debug.LogWarning("Spécial 2");
                nativeRNG = UnityEngine.Random.Range(1, 100);
            }
            if (nativeRNG >= effectChanceLower && nativeRNG <= effectChanceUpper)
            {
                Debug.LogWarning("Spécial 3");
                if (effectType == EffectEnchantmentType.Damage)
                {
                    ResolveDamage();
                }
                else
                if (effectType == EffectEnchantmentType.Heal)
                {
                    playerScript.Heal(effectStrength);
                    Instantiate(particles, player.transform.position, Quaternion.LookRotation((playerScript.attackDirection) * direction, Vector3.up));
                }
                else
                if (effectType == EffectEnchantmentType.ImmunityChance)
                {
                    playerScript.Immunity(effectDuration);
                    Debug.LogWarning("Immunity !");
                    Instantiate(particles, player.transform.position, Quaternion.LookRotation((playerScript.attackDirection) * direction, Vector3.up), player.transform);
                }
                else
                if (effectType == EffectEnchantmentType.Teleport)
                {
                    ResolveTeleport();
                    Instantiate(particles, player.transform.position, Quaternion.LookRotation((playerScript.attackDirection) * direction, Vector3.up));
                }
            }
        }

        public void ResolveDamage()
        {
            target.Clear();
            Debug.LogWarning("Attack 1");
            Vector3 reach;
            if (useWeaponReach)
            {
                reach = playerScript.profileInUse.reach[playerScript.chargeLevel];
            } else
            {
                reach = effectReach;
            }
            Debug.LogWarning("Attack 2");

            if (centerOn == CenterOn.AllEnemies)
            {
                Debug.LogWarning("Attack 3");
                for (int i = 0; i < playerScript.enemiesHitLastAttack.Count; i++)
                {
                    target.Add(playerScript.enemiesHitLastAttack[i]);
                    Debug.LogWarning("Attack 4");
                }
            } else if (centerOn == CenterOn.ClosestEnemy)
            {
                target.Add(playerScript.closestEnemyHitLastAttack);
            } else if (centerOn == CenterOn.Player)
            {
                target.Add(player);
            }
            Debug.LogWarning("Attack 5 " + target.Count);

            foreach (GameObject center in target)
            {
                Collider[] hitEnemies = Physics.OverlapSphere(center.transform.position, reach.z, effectAffectedLayers);
                foreach (Collider enemy in hitEnemies)
                {
                    Vector3 enemyDirection = enemy.transform.position - center.transform.position;
                    float enemyAngle = Vector3.Angle(playerScript.attackDirection*direction, enemyDirection);
                    if (enemyAngle <= reach.x)
                    {
                        Debug.DrawRay(center.transform.position, enemyDirection, Color.red);
                        Debug.LogError("Enemy hit ! Inflicted " + effectStrength + " damage !");
                    }
                }
                Instantiate(particles, center.transform.position, Quaternion.LookRotation((center.transform.position - player.transform.position) * direction, Vector3.up));
            }
        }

        public void ResolveTeleport()
        {
            //do teleport lmao
        }
    }
}
