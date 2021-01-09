using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class ShopWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public RandomWeaponGeneration weaponGeneration;
    public WeaponItemBehavior weapon;
    public PriceCard priceCard;
    public Quality quality = Quality.NA;
    public int weaponPrice;
    void Start()
    {
        weaponGeneration = GameObject.FindWithTag("GameManager").GetComponent<RandomWeaponGeneration>();
        weapon = GetComponent<WeaponItemBehavior>();
        priceCard = GetComponentInChildren<PriceCard>();
        Quality originalQuality = weaponGeneration.quality;
        if(quality!=Quality.NA)
        {
            weaponGeneration.quality = quality;
        }
        else
        {
            quality = weaponGeneration.quality;
        }
        weaponGeneration.Generate();
        weapon.weapon = weaponGeneration.weaponsGenerated[0];
        weaponGeneration.quality = originalQuality;
        weapon.Shop();
        weapon.isFromShop = true;
        Debug.Log(quality);
        switch (quality)
        {
            case Quality.Common:
                weaponPrice = 10;
                break;

            case Quality.Uncommon:
                weaponPrice = 25;
                break;

            case Quality.Rare:
                weaponPrice = 50;
                break;

            case Quality.VeryRare:
                weaponPrice = 75;
                break;

            case Quality.Legendary:
                weaponPrice = 100;
                break;
        }
        Debug.Log(weaponPrice);
        priceCard.DoPriceCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
