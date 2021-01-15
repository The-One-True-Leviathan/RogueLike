using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{

    [CreateAssetMenu(fileName = "newWeapon", menuName = "Pierre/Weapon/Weapon Profile", order = 0)]
    public class WeaponScriptableObject : ScriptableObject
    {
        /*[MonoScript]
        public string[] typeName;*/
        public AttackProfileScriptableObject[] atk = new AttackProfileScriptableObject[2];
        public List<Enchantment> enchantments;
        public AudioClip audioClip;

        public Sprite weaponSprite, weaponItemSprite, weaponIcon, weaponIconSecondary;
        public bool weaponUnique;
        public string weaponBaseName, weaponRealName, weaponDescription;
        public Color32 weaponColor;
        public int weaponID;

        public float totalDamageMultiplier = 1, totalKnockbackMultiplier = 1, totalBuildupMultiplier = 1;
        public Vector3 totalReachMultiplier = new Vector3(1,1,1);
        public Vector2 totalRecoilMultiplier = new Vector2(1, 1);
        GameObject player;

        public void InitializeWeapon()
        {
            totalBuildupMultiplier = totalDamageMultiplier = totalKnockbackMultiplier = 1f;
            totalReachMultiplier = Vector3.one;
            totalRecoilMultiplier = Vector2.one;
            for(int i = 0; i < enchantments.Count; i++)
            {
                enchantments[i].Initialize();
                totalBuildupMultiplier *= enchantments[i].multiplierBuildup;
                totalDamageMultiplier *= enchantments[i].multiplierDamage;
                totalKnockbackMultiplier *= enchantments[i].multiplierKnockback;
                totalReachMultiplier.x *= enchantments[i].multiplierReach.x;
                totalReachMultiplier.y *= enchantments[i].multiplierReach.y;
                totalReachMultiplier.z *= enchantments[i].multiplierReach.z;
                totalRecoilMultiplier.x *= enchantments[i].multiplierRecoil.x;
                totalRecoilMultiplier.y *= enchantments[i].multiplierRecoil.y;
            }

            Debug.Log("pouic");
            if (!weaponUnique)
            {
                if (enchantments.Count != 0)
                {
                    if (enchantments.Count == 1)
                    {
                        weaponColor = Color.green;
                        int rng = Random.Range(0, 2);
                        weaponRealName = rng == 0 ? (enchantments[0].prefix + " " + weaponBaseName) : (weaponBaseName + " of " + enchantments[0].suffix);
                    }
                    else if (enchantments.Count < 4)
                    {
                        weaponColor = Color.blue;
                        weaponRealName = enchantments.Count == 2 ? (enchantments[0].prefix + " " + weaponBaseName + " of " + enchantments[1].suffix) : (enchantments[0].prefix + " " + weaponBaseName + " of " + enchantments[1].suffix + " and " + enchantments[2].suffix);
                    }
                    else
                    {
                        weaponColor = Color.magenta;
                        weaponRealName = enchantments[0].prefix + " and " + enchantments[1].prefix + " " + weaponBaseName + " of " + enchantments[2].suffix + " and " + enchantments[3].suffix;
                    }
                } else
                {
                    weaponRealName = weaponBaseName;
                }
                for (int i = 0; i < enchantments.Count; i++)
                {
                    if (enchantments[i].rarity == EnchantRarity.Cursed)
                    {
                        weaponColor = Color.red;
                    }
                }
            } else
            {
                weaponRealName = weaponBaseName;
                weaponColor = Color.yellow;
            }

            Debug.Log("initialize weapon end");
        }


        public void AddEnchant(Enchantment enchant)
        {
            if (enchantments.Count < 4)
            {
                enchantments.Add(enchant);
                InitializeWeapon();
            }
        }

        public void DoSpecial(int specialType)
        {
            for (int i = 0; i < enchantments.Count; i++)
            {
                enchantments[i].DoSpecial(specialType);
            }

        }

    }

}

