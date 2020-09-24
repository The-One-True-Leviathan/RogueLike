using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "newEnchantement", menuName = "Pierre/Enchantment/Enchantment", order = 0)]

    public class Enchantment : ScriptableObject
    {
        public EnchantmentsEffect[] effects = new EnchantmentsEffect[1];
        public string enchantmentName, prefix, suffix, description;
        public Color color;

        public int rng;
        public float multiplierDamage = 1, multiplierBuildup = 1, multiplierKnockback = 1;
        public Vector3 multiplierReach = new Vector3(1,1,1);

        public void Initialize()
        {
            for (int i = 0; i < effects.Length; i++)
            {
                effects[i].InitializeEnchantmentEffect();
            }
        }

        public void DoSpecial(int specialType)
        {
            //Debug.LogWarning("Special !");
            rng = Random.Range(1, 100);
            for (int i = 0; i < effects.Length; i++)
            {
                if(effects[i].type == specialType)
                {
                    effects[i].nativeRNG = rng;
                    effects[i].EffectAction();
                }
            }
        }
    }
}
