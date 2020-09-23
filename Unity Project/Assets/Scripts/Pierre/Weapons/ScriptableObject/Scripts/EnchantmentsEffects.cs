using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace Weapons
{
    public enum EffectEnchantmentType { None, Heal, Damage, ImmunityChance, Teleport }
    [CreateAssetMenu(fileName = "newEffect", menuName = "Pierre/Enchantment/Effect", order = 0)]
    public class EnchantmentsEffect : ScriptableObject
    {
        GameObject player;
        Player playerScript;

        public EffectEnchantmentType effectType;
        [Tooltip("Type : 0 = Idle, 1 = Attack, 2 = Kill, 3 = Damage")]
        public int type; //0 = Idle, 1 = Attack, 2 = Kill, 3 = Damage
        [Tooltip("Should the effect use the rng given by the whole enchantment or roll its own ?")]
        public bool useNativeRNG; //Should the effect use the rng given by the whole enchantment or roll his own ?
        public int nativeRNG; //Contains the rng given by the whole enchantment

        bool isInEffect;
        [Tooltip("Minimum time between each activation of the effect")]
        public float effectTicLength;
        public float effectDuration;
        public Vector3 effectReach;
        public bool useWeaponReach;
        public bool centerEffectOnAllEnemies, centerEffectOnClosestEnemy;
        List<GameObject> target;
        public bool invertDirection;
        public float effectStrength;
        [Tooltip("Int Number to inform the % of chance of the effect happening each time its activated")]
        public int effectChance;
        public LayerMask effectAffectedLayers;
        // Start is called before the first frame update

        public void InitializeEnchantmentEffect()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerScript = player.GetComponent<Player>();
        }

        public void EffectAction()
        {
            if (effectType != EffectEnchantmentType.None)
            {
                if (!isInEffect)
                {
                    EffectCoroutine();
                }
            }
        }

        public IEnumerator EffectCoroutine()
        {
            isInEffect = true;
            DoEffect();
            yield return new WaitForSeconds(effectTicLength);
            isInEffect = false;
        }

        public void DoEffect()
        {
            if (!useNativeRNG)
            {
                nativeRNG = UnityEngine.Random.Range(1, 100);
            }
            if (nativeRNG <= effectChance)
            {
                if (effectType == EffectEnchantmentType.Damage)
                {
                    ResolveDamage();
                }
                else
                if (effectType == EffectEnchantmentType.Heal)
                {
                    playerScript.Heal(effectStrength);
                }
                else
                if (effectType == EffectEnchantmentType.ImmunityChance)
                {
                    playerScript.Immunity(effectDuration);
                }
                else
                if (effectType == EffectEnchantmentType.Teleport)
                {
                    ResolveTeleport();
                }
            }
        }

        public void ResolveDamage()
        {
            Vector3 reach;
            float direction;
            if (useWeaponReach)
            {
                reach = playerScript.profileInUse.reach[playerScript.chargeLevel];
            } else
            {
                reach = effectReach;
            }

            if (centerEffectOnAllEnemies)
            {
                foreach(GameObject enemy in playerScript.enemiesHitLastAttack)
                {
                    target.Clear();
                    target.Add(enemy);
                }
            } else if (centerEffectOnClosestEnemy)
            {
                target.Clear();
                target.Add(playerScript.closestEnemyHitLastAttack);
            } else
            {
                target.Clear();
                target.Add(player);
            }

            if (invertDirection)
            {
                direction = -1f;
            } else
            {
                direction = 1f;
            }

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
                        Debug.LogWarning("Enemy hit ! Inflicted " + effectStrength + " damage !");
                    }
                }
            }
        }

        public void ResolveTeleport()
        {
            //do teleport lmao
        }
    }
}
