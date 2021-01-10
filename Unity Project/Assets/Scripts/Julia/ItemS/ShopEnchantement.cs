using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class ShopEnchantement : MonoBehaviour
{
    public EnchantBookRandomGen enchantBook;
    public EnchantItemBehavior enchant;
    public InteractibleBehavior interactible;
    public EnchantPriceCard card;
    public int enchantPrice;
    // Start is called before the first frame update
    void Start()
    {
        enchantBook = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnchantBookRandomGen>();
        card = GetComponentInChildren<EnchantPriceCard>();
        enchant = GetComponent<EnchantItemBehavior>();
        interactible = GetComponentInChildren<InteractibleBehavior>();
        enchantBook.Generate();
        enchant.enchant = enchantBook.enchantGenerated[0];
        switch(enchant.enchant.rarity)
        {
            case EnchantRarity.Simple:
                enchantPrice = 50;
                break;

            case EnchantRarity.Special:
                enchantPrice = 75;
                break;

            case EnchantRarity.Cursed:
                enchantPrice = 75;
                break;

        }
        card.DoEnchantCard();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactible.interactible)
        {
            if (interactible.interacted)
            {
                if (!enchant.isBought)
                {
                    if (enchantPrice <= GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>().piecettesActuelles)
                    {
                        GameObject.Find("Merchant").GetComponent<MerchantScript>().BuyingDialogue();
                        GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>().Buy(enchantPrice);
                        enchant.isBought = true;
                        card.UpdateCard();
                    }
                    else
                    {
                        GameObject.Find("Merchant").GetComponent<MerchantScript>().NoMoneyDialogue();
                    }
                    interactible.interacted = false;
                }
            }
        }
    }
}
