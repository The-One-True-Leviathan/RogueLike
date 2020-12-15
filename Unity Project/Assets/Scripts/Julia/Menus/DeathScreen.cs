using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public UnityEngine.UI.Text scoreCoins, scoreBoulons;
    public GameObject gameCompo, map;

    private void Start()
    {
        gameCompo = GameObject.Find("Game Components");
        map = GameObject.FindGameObjectWithTag("Map");
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
        Application.Quit();

    }

    public void Menu()
    {
        Compteur.nbrePiecettes = 0;
        Destroy(gameCompo);
        Destroy(map);
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ReturnHUB()
    {
        Compteur.nbrePiecettes = 0;
        Destroy(gameCompo);
        Destroy(map);
        SceneManager.LoadScene("HUB");
    }

    public void PlaySoundClick()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }
}
