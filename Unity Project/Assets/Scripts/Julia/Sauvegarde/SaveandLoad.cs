using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveandLoad : MonoBehaviour
{
    public Compteur compteur;
    public ShoppingManager shop;
    public int gameWasLaunchedOnce = 0;

    public void Start()
    {
        compteur = GetComponentInChildren<Compteur>();
        shop = GetComponentInChildren<ShoppingManager>();
        gameWasLaunchedOnce = PlayerPrefs.GetInt("State");
        if (gameWasLaunchedOnce == 0)
        {
            SaveAll();
            gameWasLaunchedOnce = 1;
            PlayerPrefs.SetInt("State", gameWasLaunchedOnce);
        }
        LoadAll();
    }
    public void SaveAll()
    {
        shop.SaveShop();
        compteur.SaveMoney();
    }

    public void LoadAll()
    {
        compteur.LoadMoney();
        shop.LoadingShop();
        Debug.Log("Entities were loaded");
    }
}
