using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace Weapons
{
    [CreateAssetMenu(fileName = "newEffect", menuName = "Pierre/Enchantment/Effect", order = 0)]
    public class EnchantmentsEffect : ScriptableObject
    {
        public GameObject player;
        public Player playerScript;

        public int type; //0 = Idle, 1 = Attack, 2 = Kill, 3 = Damage
        public bool useNativeRNG; //Should the effect use the rng given by the whole enchantment or roll his own ?
        public int nativeRNG; //Contains the rng given by the whole enchantment

        public enum EffectEnchantmentType { None, Heal, Damage, ImmunityChance, Teleport }
        public bool isInEffect;
        public float effectTicLength, effectDuration;
        public Vector3 effectReach;
        public bool useWeaponReach;
        public bool CenterEffectOnEnemy;
        public int effectStrength, effectChance;
        public LayerMask effectAffectedLayers;
        // Start is called before the first frame update

        public void InitializeEnchantmentEffect()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerScript = player.GetComponent<Player>();
        }

        public void EffectAction(EffectEnchantmentType type)
        {
            if (type != EffectEnchantmentType.None)
            {
                if (!isInEffect)
                {
                    EffectCoroutine(type);
                }
            }
        }

        public IEnumerator EffectCoroutine(EffectEnchantmentType type)
        {
            isInEffect = true;
            DoEffect(type);
            yield return new WaitForSeconds(effectTicLength);
            isInEffect = false;
        }

        public void DoEffect(EffectEnchantmentType type)
        {
            if (!useNativeRNG)
            {
                nativeRNG = UnityEngine.Random.Range(1, 100);
            }
            if (nativeRNG <= effectChance)
            {
                if (type == EffectEnchantmentType.Damage)
                {
                    ResolveDamage();
                }
                else
                if (type == EffectEnchantmentType.Heal)
                {
                    playerScript.Heal(effectStrength);
                }
                else
                if (type == EffectEnchantmentType.ImmunityChance)
                {
                    playerScript.Immunity(effectDuration);
                }
                else
                if (type == EffectEnchantmentType.Teleport)
                {
                    ResolveTeleport();
                }
            }
        }

        public void ResolveDamage()
        {
            Collider[] hitEnemies = Physics.OverlapSphere(player.transform.position, effectReach.z, effectAffectedLayers);
            foreach (Collider enemy in hitEnemies)
            {
                Vector3 enemyDirection = enemy.transform.position - player.transform.position;
                float enemyAngle = Vector3.Angle(playerScript.attackDirection, enemyDirection);
                if (enemyAngle <= effectReach.x)
                {
                    Debug.DrawRay(player.transform.position, enemyDirection, Color.red);
                    Debug.LogWarning("Enemy hit ! Inflicted " + effectStrength + " damage !");
                }
            }
        }

        public void ResolveTeleport()
        {
            //do teleport lmao
        }
    }
}
