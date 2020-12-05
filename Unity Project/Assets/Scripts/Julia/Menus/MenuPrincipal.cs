using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject menu, optionMenu, pauseMenu;
    Vector3 optionScale;

    private void Start()
    {
        optionMenu.GetComponent<RectTransform>().localScale = Vector3.zero;
        pauseMenu.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    private void OnMouseEnter()
    {
        Debug.Log("niuh");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("HUB");
    }

    public void OptionOpen()
    {
        optionMenu.GetComponent<RectTransform>().localScale = Vector3.one;
        menu.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    public void OptionClose()
    {
        optionMenu.GetComponent<RectTransform>().localScale = Vector3.zero;
        menu.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void PlaySoundClick()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }

    public void Controls()
    {
        optionMenu.GetComponent<RectTransform>().localScale = Vector3.zero;
        pauseMenu.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void BackFromControls()
    {
        pauseMenu.GetComponent<RectTransform>().localScale = Vector3.zero;
        optionMenu.GetComponent<RectTransform>().localScale = Vector3.one;
    }
}
