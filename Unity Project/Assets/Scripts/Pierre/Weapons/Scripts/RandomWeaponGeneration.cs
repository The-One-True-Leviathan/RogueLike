using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public enum Quality { Common, Uncommon, Rare, VeryRare, Legendary, Artifact, NA}
    public class RandomWeaponGeneration : MonoBehaviour
    {
        public List<WeaponScriptableObject> baseWeapons, uniqueWeapons, weaponsGenerated = new List<WeaponScriptableObject>();
        public List<Enchantment> simpleEnchants, specialEnchants, availableSimpleEnchants, availableSpecialEnchants, cursedEnchants;
        public ShoppingManager shoppingManager;
        public Quality quality;
        int rng;
        // Start is called before the first frame update
        private void Awake()
        {
            shoppingManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShoppingManager>();
        }
        public List<WeaponScriptableObject> Generate()
        {
            availableSimpleEnchants.Clear();
            availableSpecialEnchants.Clear();
            foreach (Enchantment enchant in simpleEnchants)
            {
                if (shoppingManager.boughtEnchantements.Contains(enchant))
                {
                    availableSimpleEnchants.Add(enchant);
                }
            }
            foreach (Enchantment enchant in specialEnchants)
            {
                if (shoppingManager.boughtEnchantements.Contains(enchant))
                {
                    availableSpecialEnchants.Add(enchant);
                }
            }
            weaponsGenerated.Clear();
            switch(quality)
            {
                case Quality.Common:
                    CommonGen(0);
                    break;
                case Quality.Uncommon:
                    rng = Random.Range(0, 100);
                    if (rng <= 75)
                    {
                        UncommonGen(0);
                    } else
                    {
                        UncommonGen(0);
                        UncommonGen(1);
                    }
                    break;
                case Quality.Rare:
                    rng = Random.Range(0, 100);
                    if (rng <= 15)
                    {
                        UncommonGen(0);
                        UncommonGen(1);
                    }
                    else if (rng <= 85)
                    {
                        RareGen(0);
                    } else
                    {
                        RareGen(0);
                        RareGen(1);
                    }
                    break;
                case Quality.VeryRare:
                    rng = Random.Range(0, 100);
                    if (rng <= 15)
                    {
                        RareGen(0);
                        RareGen(1);
                    }
                    else if (rng <= 85)
                    {
                        VeryRareGen(0);
                    }
                    else
                    {
                        VeryRareGen(0);
                        VeryRareGen(1);
                    }
                    break;
                case Quality.Legendary:
                    rng = Random.Range(0, 100);
                    if (rng <= 15)
                    {
                        VeryRareGen(0);
                        VeryRareGen(1);
                    }
                    else if (rng <= 85)
                    {
                        LegendaryGen(0);
                    }
                    break;
                case Quality.Artifact:
                    ArtifactGen();
                    break;
            }
            return weaponsGenerated;
        }

        void CommonGen(int numberWeapon)//Génère une arme sans enchantements
        {

            rng = Random.Range(0, baseWeapons.Count);
            WeaponScriptableObject weapon = Object.Instantiate(baseWeapons[rng]) as WeaponScriptableObject;
            weaponsGenerated.Add(weapon);
        }
        void UncommonGen(int numberWeapon)//Génère une arme avec 1 "point d'enchantement"
        {
            rng = Random.Range(0, baseWeapons.Count);
            WeaponScriptableObject weapon = Object.Instantiate(baseWeapons[rng]) as WeaponScriptableObject;
            weaponsGenerated.Add(weapon);
            rng = Random.Range(0, 100);
            if (rng <= 60)
            {
                rng = Random.Range(0, availableSimpleEnchants.Count);
                weaponsGenerated[weaponsGenerated.Count - 1].enchantments.Add(simpleEnchants[rng]);
            }
            else
            {
                rng = Random.Range(0, availableSpecialEnchants.Count);
                weaponsGenerated[weaponsGenerated.Count - 1].enchantments.Add(specialEnchants[rng]);
            }
            
            weaponsGenerated[weaponsGenerated.Count - 1].InitializeWeapon();
        }
        void RareGen(int numberWeapon)//Génère une arme avec 2 "points d'enchantement"
        {
            rng = Random.Range(0, baseWeapons.Count);
            WeaponScriptableObject weapon = Object.Instantiate(baseWeapons[rng]) as WeaponScriptableObject;
            weaponsGenerated.Add(weapon);
            for (int i = 2; i > 0; i--)
            {
                rng = Random.Range(0, 100);
                if (rng <= 50)
                {
                    rng = Random.Range(0, availableSimpleEnchants.Count);
                    weaponsGenerated[numberWeapon].enchantments.Add(simpleEnchants[rng]);
                }
                else if (rng <= 90)
                {
                    i--;
                    rng = Random.Range(0, availableSpecialEnchants.Count);
                    weaponsGenerated[numberWeapon].enchantments.Add(specialEnchants[rng]);
                }
                else
                {
                    i--; i--;
                    rng = Random.Range(0, cursedEnchants.Count);
                    weaponsGenerated[numberWeapon].enchantments.Add(cursedEnchants[rng]);
                }
            }
            weaponsGenerated[numberWeapon].InitializeWeapon();
        }
        void VeryRareGen(int numberWeapon)//Génère une arme avec 3 "points d'enchantement"
        {
            rng = Random.Range(0, baseWeapons.Count);
            WeaponScriptableObject weapon = Object.Instantiate(baseWeapons[rng]) as WeaponScriptableObject;
            weaponsGenerated.Add(weapon);
            for (int i = 3; i > 0; i--)
            {
                rng = Random.Range(0, 100);
                if (rng <= 50)
                {
                    rng = Random.Range(0, availableSimpleEnchants.Count);
                    weaponsGenerated[numberWeapon].enchantments.Add(simpleEnchants[rng]);
                }
                else if (rng <= 90)
                {
                    i--;
                    rng = Random.Range(0, availableSpecialEnchants.Count);
                    weaponsGenerated[numberWeapon].enchantments.Add(specialEnchants[rng]);
                }
                else
                {
                    i--; i--;
                    rng = Random.Range(0, cursedEnchants.Count);
                    weaponsGenerated[numberWeapon].enchantments.Add(cursedEnchants[rng]);
                }
            }
            weaponsGenerated[numberWeapon].InitializeWeapon();

        }
        void LegendaryGen(int numberWeapon)//Génère une arme avec 4 "points d'enchantement"
        {
            rng = Random.Range(0, baseWeapons.Count);
            WeaponScriptableObject weapon = Object.Instantiate(baseWeapons[rng]) as WeaponScriptableObject;
            weaponsGenerated.Add(weapon);
            for (int i = 4; i>0; i--)
            {
                rng = Random.Range(0, 100);
                if (rng <= 30)
                {
                    rng = Random.Range(0, availableSimpleEnchants.Count);
                    weaponsGenerated[numberWeapon].enchantments.Add(simpleEnchants[rng]);
                } else if (rng <= 80)
                {
                    i--;
                    rng = Random.Range(0, availableSpecialEnchants.Count);
                    weaponsGenerated[numberWeapon].enchantments.Add(specialEnchants[rng]);
                } else
                {
                    i--; i--;
                    rng = Random.Range(0, cursedEnchants.Count);
                    weaponsGenerated[numberWeapon].enchantments.Add(cursedEnchants[rng]);
                }
            }
            weaponsGenerated[numberWeapon].InitializeWeapon();
        }
        void ArtifactGen()
        {
            /*La génération "Artéfact" ne produit qu'une seule arme, 
             * prise dans une liste d'armes créées à la main avec des 
             * enchantements uniques*/
            rng = Random.Range(0, uniqueWeapons.Count);
            WeaponScriptableObject weapon = Object.Instantiate(uniqueWeapons[rng]) as WeaponScriptableObject;
            weaponsGenerated.Add(weapon);
        }
    }
}
