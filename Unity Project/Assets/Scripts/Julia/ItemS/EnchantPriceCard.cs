using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnchantPriceCard : MonoBehaviour
{
    public ShopEnchantement shop;
    public Text priceText;
    // Start is called before the first frame update
    
    public void DoEnchantCard()
    {
        priceText = GetComponentInChildren<Text>();
        shop = GetComponentInParent<ShopEnchantement>();
        priceText.text = "Costs " + shop.enchantPrice + " Coins";
    }

    // Update is called once per frame
    public void UpdateCard()
    {
        priceText.text = "Now attack the book !";
    }
}
