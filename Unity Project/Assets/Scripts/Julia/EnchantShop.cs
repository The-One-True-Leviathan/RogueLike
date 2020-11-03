using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class EnchantShop : MonoBehaviour
{
    public Compteur compteur;
    public List<Enchantment> enchantments;
    public ShoppingManager shopping;
    public InteractibleBehavior interactible;
    public GameObject enchantShopCanvas;
    public Text[] nameTexts;
    public Text[] descriptionTexts;
    public bool enchantShopOpen = false;
    public GameObject confirmationCanvas;

    // Start is called before the first frame update
    void Start()
    {
        compteur = GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>();
        interactible = GetComponentInChildren<InteractibleBehavior>();
        shopping = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShoppingManager>();
        enchantShopCanvas = GameObject.Find("Canvas enchants");
        enchantShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
        confirmationCanvas = GameObject.Find("Confirmation");
        confirmationCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
        for(int i = 0; i < enchantments.Count; i++)
        {
            nameTexts[i].text = enchantments[i].enchantmentName;
            descriptionTexts[i].text = enchantments[i].description;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactible.interacted && !enchantShopOpen)
        {
            enchantShopCanvas.GetComponent<RectTransform>().localScale = Vector3.one;
            enchantShopOpen = true;
        }
        else if (interactible.interacted && enchantShopOpen)
        {
            enchantShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
            enchantShopOpen = false;
        }
        
    }

    public void Enchant()
    {

    }

    public void Confirm()
    {

    }
    
    public void No()
    {

    }

    //contenir 8 slots d'enchants
    //doit afficher le titre et les effets de l'enchant
    //1 fonction pour chaque achat 
    //1 fonction pour chaque confirmation
    //doit afficher un texte de confirmation avec le prix de l'enchant
    //envoyer les infos au shopping manager qui lui va conserver quel enchant ont été achetés

}
