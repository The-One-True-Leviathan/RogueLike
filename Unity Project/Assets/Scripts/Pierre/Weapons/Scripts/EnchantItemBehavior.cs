using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class EnchantItemBehavior : MonoBehaviour
{
    public Enchantment enchant;
    public Player player;
    public Text text;
    public bool isFromShop = false, isBought = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (isFromShop)
        {
            EnchantBookRandomGen enchantBookRandomGen = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnchantBookRandomGen>();
            enchant = Object.Instantiate(enchantBookRandomGen.Generate()[0]) as Enchantment;
            Debug.Log("1" + enchant.enchantmentName);
        }
        else
        {
            enchant = Object.Instantiate(enchant) as Enchantment;
            Debug.Log("2" + enchant.enchantmentName);
        }
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.1f);
        text.text = enchant.enchantmentName;
    }

    public void Attacked()
    {
        if (player.weaponInAtk.enchantments.Count < 4 && (isFromShop == false || isBought == true))
        {
            player.weaponInAtk.enchantments.Add(enchant);
            player.weaponInAtk.InitializeWeapon();
            GameObject.Destroy(gameObject);
        }
    }

    public void Attacked(WeaponScriptableObject weaponToEnchant)
    {
        if (weaponToEnchant.enchantments.Count < 4 && (isFromShop == false || isBought == true))
        {
            weaponToEnchant.enchantments.Add(enchant);
            weaponToEnchant.InitializeWeapon();
            GameObject.Destroy(gameObject);
        }

    }
}
