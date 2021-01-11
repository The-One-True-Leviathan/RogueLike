using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class ShoppingManager : MonoBehaviour
{
    // Start is called before the first frame update
    //ce script va conserver les données des choses achetées dans les boutiques

    public bool BarrelsWereBought = false;
    public List<Enchantment> boughtEnchantements;
    public List<int> numeroEnchantsBough;

    public void BarrelUpdate()
    {
        BarrelsWereBought = true;
    }

    public void SaveShop()
    {
        SystemSaver.SaveShop(this);
    }

    public void LoadingShop()
    {
        DataSaver shopData = SystemSaver.LoadShop();
        //boughtEnchantements = shopData.boughtEnchants;
        numeroEnchantsBough = shopData.numeroBoughtEnchants;
        BarrelsWereBought = shopData.barrelsBrought;
    }
}
