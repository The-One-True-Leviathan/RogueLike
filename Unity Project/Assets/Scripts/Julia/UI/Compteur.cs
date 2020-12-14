using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compteur : MonoBehaviour
{
    public UnityEngine.UI.Text displayPiecettes, displayBoulon;
    static public int nbrePiecettes, nbreBoulon;
    public int piecettesActuelles, boulonsActuels;

    // Update is called once per frame
    void Update()
    {
        piecettesActuelles = nbrePiecettes;
        boulonsActuels = nbreBoulon;
        displayPiecettes.text = "Piecettes:" + nbrePiecettes.ToString();
        displayBoulon.text = "Boulons:" + nbreBoulon.ToString();
    }

    public void GainPiecettes(int value)
    {
        //son récupération
        nbrePiecettes += value;  
    }

    public void GainBoulon(int value)
    {
        //son récupération
        nbreBoulon += value;
    }

    public void Buy(int price)
    {
        nbrePiecettes -= price;
    }

    public void HudBuy(int price)
    {
        nbreBoulon -= price;
    }

    public void SaveMoney()
    {
        SystemSaver.SaveMoney(this);
    }

    public void LoadMoney()
    {
        DataSaver data = SystemSaver.LoadMoney();
        nbreBoulon = data.boulons;
    }
}
