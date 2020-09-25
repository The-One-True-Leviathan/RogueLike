using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Compteur compteur;
    public UnityEngine.UI.Text scoreCoins, scoreBoulons;


    // Update is called once per frame
    void Update()
    {
        scoreCoins.text = "Piecettes : " + Compteur.nbrePiecettes.ToString();
        scoreBoulons.text = "Boulons : " + Compteur.nbreBoulon.ToString();
    }

    public void QuitGame()
    {
        Compteur.nbrePiecettes = 0;
        Application.Quit();
    }

    public void Menu()
    {
        Compteur.nbrePiecettes = 0;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ReturnHUB()
    {
        Compteur.nbrePiecettes = 0;
        SceneManager.LoadScene("HUB");
    }
}
