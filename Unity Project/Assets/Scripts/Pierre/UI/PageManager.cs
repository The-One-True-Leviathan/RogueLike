using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject next, prev;
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
        }
        else
        {
            next.SetActive(true);
        }
        if (activePage == 0)
        {
            prev.SetActive(false);
        }
        else
        {
            prev.SetActive(true);
        }

    }
}
