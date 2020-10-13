using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSaver 
{
    public bool[] shops;
    public bool barrelsBrought;
    public int boulons;

    public DataSaver (Compteur compteur)
    {
        boulons = compteur.boulonsActuels;
    }
  
}
