using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public enum EnchantRarity { Simple, Special, Cursed }
    [CreateAssetMenu(fileName = "newEnchantement", menuName = "Pierre/Enchantment/Enchantment", order = 0)]
    public class Enchantment : ScriptableObject
    {
        public EnchantmentsEffect[] effects;
        public string enchantmentName, prefix, suffix, description;
        public EnchantRarity rarity;
        public Color color;
        public int price;

        public int rng;
        public float multiplierDamage = 1, multiplierBuildup = 1, multiplierKnockback = 1;
        public Vector3 multiplierReach = new Vector3(1,1,1);
        public Vector2 multiplierRecoil = new Vector2(1, 1);

        public void Initialize()
        {
            switch (rarity)
            {
                case EnchantRarity.Simple:
                    color = Color.green;
                    break;
                case EnchantRarity.Special:
                    color = Color.blue;
                    break;
                case EnchantRarity.Cursed:
                    color = Color.red;
                    break;
            }

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
                    effects[i].DoEffect();
                }
            }
        }
    }
}
