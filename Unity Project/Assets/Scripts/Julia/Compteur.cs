using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compteur : MonoBehaviour
{
    public UnityEngine.UI.Text displayPiecettes, displayBoulon;
    static public int nbrePiecettes, nbreBoulon;
    public int piecettesActuelles;

    // Update is called once per frame
    void Update()
    {
        piecettesActuelles = nbrePiecettes;
        displayPiecettes.text = "Piecettes:" + nbrePiecettes.ToString();
        displayBoulon.text = "Boulons:" + nbreBoulon.ToString();
    }

    public void GainPiecettes()
    {
        //son récupération
        nbrePiecettes++;  
    }

    public void GainBoulon()
    {
        //son récupération
        nbreBoulon++;
    }

    public void Buy(int price)
    {
        nbrePiecettes -= price;
    }
}
