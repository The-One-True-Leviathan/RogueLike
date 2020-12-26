using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public UnityEngine.UI.Text scoreCoins, scoreBoulons;
    public GameObject gameCompo;
    public SaveandLoad saveandLoad;

    private void Start()
    {
        gameCompo = GameObject.Find("Game Components");
        saveandLoad = gameCompo.GetComponent<SaveandLoad>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreCoins.text = "Piecettes : " + Compteur.nbrePiecettes.ToString();
        scoreBoulons.text = "Boulons : " + Compteur.nbreBoulon.ToString();
    }

    public void QuitGame()
    {
        Compteur.nbrePiecettes = 0;
        saveandLoad.SaveAll();
        Application.Quit();

    }

    public void Menu()
    {
        Compteur.nbrePiecettes = 0;
        saveandLoad.SaveAll();
        Destroy(gameCompo);
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ReturnHUB()
    {
        Compteur.nbrePiecettes = 0;
        saveandLoad.SaveAll();
        Destroy(gameCompo);
        SceneManager.LoadScene("HUB");
    }

    public void PlaySoundClick()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }
}
