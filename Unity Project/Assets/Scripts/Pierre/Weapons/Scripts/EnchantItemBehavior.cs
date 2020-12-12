using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class EnchantItemBehavior : MonoBehaviour
{
    public Enchantment enchant;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        enchant = Object.Instantiate(enchant) as Enchantment;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Attacked()
    {
        if (player.weaponInAtk.enchantments.Count < 4)
        {
            player.weaponInAtk.enchantments.Add(enchant);
            player.weaponInAtk.InitializeWeapon();
            GameObject.Destroy(gameObject);
        }
    }
}
