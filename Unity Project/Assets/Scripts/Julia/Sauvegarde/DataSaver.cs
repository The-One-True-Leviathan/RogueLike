using Microsoft.SqlServer.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

[System.Serializable]
public class DataSaver 
{
    public bool[] shops;
    public bool barrelsBrought;
    public int boulons;
    public List<int> numeroBoughtEnchants;
    public bool wasLoadOnce;

    public DataSaver (Compteur compteur)
    {
        boulons = compteur.boulonsActuels;
    }

    public DataSaver (ShoppingManager shopping)
    {
        numeroBoughtEnchants = shopping.numeroEnchantsBough;
        barrelsBrought = shopping.BarrelsWereBought;
    }

    /*public DataSaver (SaveandLoad game)
    {
        wasLoadOnce = game.gameWasLaunchedOnce;
    }*/

  
}
