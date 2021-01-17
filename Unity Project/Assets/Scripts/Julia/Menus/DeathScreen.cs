using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public UnityEngine.UI.Text scoreCoins, scoreBoulons;
    public GameObject gameCompo;
    Compteur compteur;
    public SaveandLoad saveandLoad;

    private void Start()
    {
        gameCompo = GameObject.Find("Game Components");
        //compteur = gameCompo.GetComponentInChildren<Compteur>();
        //saveandLoad = gameCompo.GetComponent<SaveandLoad>();
    }

    // Update is called once per frame
    void Update()
    {
        /*scoreCoins.text = compteur.nbrePiecettes.ToString();
        scoreBoulons.text = compteur.nbreBoulon.ToString();*/
    }

    public void QuitGame()
    {
        //compteur.nbrePiecettes = 0;
        //saveandLoad.SaveAll();
        Application.Quit();

    }

    public void Menu()
    {
        //compteur.nbrePiecettes = 0;
        //saveandLoad.SaveAll();
        Destroy(gameCompo);
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ReturnHUB()
    {
        //compteur.nbrePiecettes = 0;
        //saveandLoad.SaveAll();
        Destroy(gameCompo);
        SceneManager.LoadScene("HUB");
    }

    public void PlaySoundClick()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }
}
