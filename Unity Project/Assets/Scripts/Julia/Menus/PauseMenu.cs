using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public Controler controller;
    Compteur compteur;
    public GameObject pauseMenuUI, controlMenuUI, gameCompo;
    public AudioMixer audioMixer;
    public SaveandLoad saveandLoad;

    private void Start()
    {
        controller = new Controler();
        controller.Enable();
        pauseMenuUI.GetComponent<RectTransform>().localScale = Vector3.zero;
        controlMenuUI.GetComponent<RectTransform>().localScale = Vector3.zero;
        gameCompo = GameObject.Find("Game Components");
        compteur = gameCompo.GetComponentInChildren<Compteur>();
        saveandLoad = gameCompo.GetComponent<SaveandLoad>();
        controller.Keyboard.Pause.performed += ctx => MenuWork();
    }

    void MenuWork()
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.GetComponent<RectTransform>().localScale = Vector3.zero;
        controlMenuUI.GetComponent<RectTransform>().localScale = Vector3.zero;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.GetComponent<RectTransform>().localScale = Vector3.one;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitGame()
    {
        compteur.nbrePiecettes = 0;
        saveandLoad.SaveAll();
        Application.Quit();
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        compteur.nbrePiecettes = 0;
        saveandLoad.SaveAll();
        Destroy(gameCompo);
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ReturnHUB()
    {
        Time.timeScale = 1f;
        compteur.nbrePiecettes = 0;
        saveandLoad.SaveAll();
        Destroy(gameCompo);
        SceneManager.LoadScene("HUB");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void Controls()
    {
        pauseMenuUI.GetComponent<RectTransform>().localScale = Vector3.zero;
        controlMenuUI.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void BackFromControls()
    {
        controlMenuUI.GetComponent<RectTransform>().localScale = Vector3.zero;
        pauseMenuUI.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void PlaySoundClick()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }
}
