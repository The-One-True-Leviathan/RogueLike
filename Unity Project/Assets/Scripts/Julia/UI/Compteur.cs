using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compteur : MonoBehaviour
{
    public UnityEngine.UI.Text displayPiecettes, displayBoulon;
    public int nbrePiecettes, nbreBoulon;
    public int piecettesActuelles, boulonsActuels;

    // Update is called once per frame
    void Update()
    {
        piecettesActuelles = nbrePiecettes;
        boulonsActuels = nbreBoulon;
        displayPiecettes.text =  nbrePiecettes.ToString();
        displayBoulon.text = nbreBoulon.ToString();
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
        Debug.Log("Money is saved");
    }

    public void LoadMoney()
    {
        DataSaver moneyData = SystemSaver.LoadMoney();
        nbreBoulon = moneyData.boulons;
    }
}
