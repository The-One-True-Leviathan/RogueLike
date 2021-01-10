using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class EnchantBookRandomGen : MonoBehaviour
{
    public List<Enchantment> simpleEnchants, specialEnchants, availableSimpleEnchants, availableSpecialEnchants, cursedEnchants, enchantGenerated;
    public Quality quality;
    int rng;
    public ShoppingManager shoppingManager;
    // Start is called before the first frame update
    void Start()
    {
        shoppingManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShoppingManager>();
    }

    public List<Enchantment> Generate()
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
        enchantGenerated.Clear();

        switch (quality)
        {
            case Quality.Common:
                CommonGen();
                break;
            case Quality.Uncommon:
                CommonGen();
                UncommonGen();
                break;
            case Quality.Rare:
                UncommonGen();
                RareGen();
                break;
            case Quality.VeryRare:
                RareGen();
                rng = Random.Range(0, 100);
                if (rng < 50)
                {
                    UncommonGen();
                } else
                {
                    RareGen();
                }
                break;
            case Quality.Legendary:
                RareGen();
                RareGen();
                break;
            case Quality.Artifact:
                break;
        }

        return enchantGenerated;
    }

    private void CommonGen()
    {
        rng = Random.Range(0, availableSimpleEnchants.Count);
        Enchantment enchant = Object.Instantiate(availableSimpleEnchants[rng]) as Enchantment;
        enchantGenerated.Add(enchant);
    }

    private void UncommonGen()
    {
        rng = Random.Range(0, 100);
        if (rng < 50)
        {
            CommonGen();
        } else
        {
            rng = Random.Range(0, availableSpecialEnchants.Count);
            Enchantment enchant = Object.Instantiate(availableSpecialEnchants[rng]) as Enchantment;
            enchantGenerated.Add(enchant);
        }
    }

    private void RareGen()
    {
        rng = Random.Range(0, 100);
        rng = Random.Range(0, availableSpecialEnchants.Count);
        Enchantment enchant = Object.Instantiate(availableSpecialEnchants[rng]) as Enchantment;
        enchantGenerated.Add(enchant);

    }
}
