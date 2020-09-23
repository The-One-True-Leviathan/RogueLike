using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using B83.Unity.Attributes;
using UnityEditor;
using System.Reflection;

namespace Weapons
{

    [CreateAssetMenu(fileName = "newWeapon", menuName = "Pierre/Weapon/Weapon Profile", order = 0)]
    public class WeaponScriptableObject : ScriptableObject
    {
        /*[MonoScript]
        public string[] typeName;*/
        public AttackProfileScriptableObject[] atk = new AttackProfileScriptableObject[2];
        public EnchantmentsEffect[] enchantments;


        public Sprite weaponSprite;
        public string weaponBaseName;
        public string weaponRealName;
        public int weaponID;
        GameObject player;

        public void InitializeWeapon()
        {
            for(int i = 0; i >= enchantments.Length; i++)
            {
                enchantments[i].InitializeEnchantmentEffect();
            }
        }
        public void AttackAction()
        {
           
        }

    }

}

