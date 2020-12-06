using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelShop : MonoBehaviour
{

    public ShoppingManager shoppingManager;
    public Compteur compteur;
    public int price;
    public bool wasBought = false;
    public bool barrelShopOpen = false;
    public GameObject barrelShopCanvas;
    public InteractibleBehavior interactible;

    // Start is called before the first frame update
    void Start()
    {
        compteur = GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>();
        interactible = GetComponentInChildren<InteractibleBehavior>();
        shoppingManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShoppingManager>();
        barrelShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        if (interactible.interacted && !barrelShopOpen)
        {
            barrelShopCanvas.GetComponent<RectTransform>().localScale = Vector3.one;
            barrelShopOpen = true;
            Time.timeScale = 0f;
            interactible.interacted = false;
        }
        else if (interactible.interacted && barrelShopOpen)
        {
            barrelShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
            barrelShopOpen = false;
            Time.timeScale = 1f;
            interactible.interacted = false;
        }
    }

    public void Bought()
    {
        if(!wasBought && compteur.boulonsActuels >= price)
        {
        shoppingManager.BarrelUpdate();
        compteur.HudBuy(price);
        wasBought = true;

        }
    }


    //ce script doit contenir le bool BarrelsBought
    //doit prélever des boulons depuis compteur
    //activer les barils s'il est true
    //changements dans le script de barils pour les activer
    //il récupère le booléan de son côté
}
