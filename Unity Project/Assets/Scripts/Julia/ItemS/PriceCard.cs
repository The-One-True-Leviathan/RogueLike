using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceCard : MonoBehaviour
{
    public ShopWeapon shopWeapon;
    public Text priceText;

    // Start is called before the first frame update
    public void DoPriceCard()
    {
        shopWeapon = GetComponentInParent<ShopWeapon>();
        priceText = GetComponentInChildren<Text>();
        Debug.Log(shopWeapon.weaponPrice);
        priceText.text = "Costs " + shopWeapon.weaponPrice + " Coins";
    }

}
