using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBManager : MonoBehaviour
{
    public bool potionsShopPurchased = false, barrelsShopPurchased = false, enchantsShopPurchased = false;
    public GameObject potionShop, barrelsShop;
    //pour sauvegarder ces valeurs il faudra probablement une liste

    // Start is called before the first frame update
    public void Start()
    {
        DontDestroyOnLoad(this);

        if (potionsShopPurchased == true)
        {
            potionShop.SetActive(true);
        }

        if(barrelsShopPurchased == true)
        {
            barrelsShop.SetActive(true);
        }
    }

    public void buyPotionsShop()
    {
        potionShop.SetActive(true);
        potionsShopPurchased = true;
    }

    public void buyBarrelsShop()
    {
        barrelsShop.SetActive(true);
        barrelsShopPurchased = true;
    }
}
