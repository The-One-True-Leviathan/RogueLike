using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject next, prev;
    public EnchantShop shop;
    int activePage = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        pages[activePage].SetActive(true);
    }

    public void ResetPages()
    {
        activePage = 0;
        foreach(GameObject page in pages)
        {
            page.SetActive(false);
        }
        pages[activePage].SetActive(true);
        UpdateButtons();
    }

    public void Next()
    {
        if (activePage+1 < pages.Length)
        {
            pages[activePage].SetActive(false);
            activePage++;
            pages[activePage].SetActive(true);
        }
        UpdateButtons();
    }

    public void Prev()
    {
        if (activePage-1 >= 0)
        {
            pages[activePage].SetActive(false);
            activePage--;
            pages[activePage].SetActive(true);
        }
        UpdateButtons();
    }

    void UpdateButtons()
    {
        if (activePage == pages.Length - 1)
        {
            next.SetActive(false);
            shop.buttons[(4 * (activePage + 1)) - 1].GetComponent<Button>().Select();
        }
        else
        {
            next.SetActive(true);
            Navigation nav = next.GetComponent<Button>().navigation;
            nav.selectOnUp = shop.buttons[(4 * (activePage + 1)) - 1].GetComponent<Button>();
            next.GetComponent<Button>().navigation = nav;
        }
        if (activePage == 0)
        {
            prev.SetActive(false);
            shop.buttons[(1 * (activePage + 1)) - 1].GetComponent<Button>().Select();
        }
        else
        {
            prev.SetActive(true);
            Navigation nav = prev.GetComponent<Button>().navigation;
            nav.selectOnUp = shop.buttons[(2 * (activePage + 1)) - 1].GetComponent<Button>();
            prev.GetComponent<Button>().navigation = nav;
        }

    }
}
