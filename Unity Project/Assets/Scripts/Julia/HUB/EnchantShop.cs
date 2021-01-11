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
    public GameObject[] buttons;
    public bool enchantShopOpen = false;
    public GameObject confirmationCanvas;
    public Text confirmationText;
    public int numeroEnchant;
    public List<bool> isEnchantementBough;

    // Start is called before the first frame update
    void Start()
    {
        compteur = GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>();
        interactible = GetComponentInChildren<InteractibleBehavior>();
        shopping = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShoppingManager>();
        enchantShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
        confirmationCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
        
        for(int i = 0; i < enchantments.Count; i++)
        {
            nameTexts[i].text = enchantments[i].enchantmentName;
            descriptionTexts[i].text = enchantments[i].description;
            isEnchantementBough.Add(false);
        }

        for(int i = 0; i < shopping.numeroEnchantsBough.Count; i++)
        {
            buttons[shopping.numeroEnchantsBough[i]].GetComponent<Button>().enabled = false;
            buttons[shopping.numeroEnchantsBough[i]].GetComponentInChildren<Text>().text = "BOUGHT";
            shopping.boughtEnchantements.Add(enchantments[shopping.numeroEnchantsBough[i]]);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (interactible.interacted && !enchantShopOpen)
        {
            enchantShopCanvas.GetComponent<RectTransform>().localScale = Vector3.one;
            enchantShopOpen = true;
            Time.timeScale = 0f;
            interactible.interacted = false;
        }
        else if (interactible.interacted && enchantShopOpen)
        {
            enchantShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
            enchantShopOpen = false;
            Time.timeScale = 1f;
            interactible.interacted = false;
        }
        
    }

    public void Enchant(int wantedEnchant)
    {
        confirmationText.text = "This enchantement costs" + enchantments[wantedEnchant].price;
        confirmationText.color = Color.white;
        numeroEnchant = wantedEnchant;
        confirmationCanvas.GetComponent<RectTransform>().localScale = Vector3.one;
        //Debug.LogError(enchantments[numeroEnchant].enchantmentName);
    }

    
    public void Confirm()
    {
        if(compteur.boulonsActuels >= enchantments[numeroEnchant].price)
        {
            compteur.HudBuy(enchantments[numeroEnchant].price);
            shopping.boughtEnchantements.Add(enchantments[numeroEnchant]);
            shopping.numeroEnchantsBough.Add(numeroEnchant);
            buttons[numeroEnchant].GetComponent<Button>().enabled = false;
            buttons[numeroEnchant].GetComponentInChildren<Text>().text = "BOUGHT";
            confirmationCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
            enchantShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
            Time.timeScale = 1f;
            enchantShopOpen = false;

        }
        else
        {
            confirmationText.color = Color.red;
        }
    }
    
    public void No()
    {
        confirmationCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    //contenir 8 slots d'enchants
    //doit afficher le titre et les effets de l'enchant
    //1 fonction pour chaque achat 
    //1 fonction pour chaque confirmation
    //doit afficher un texte de confirmation avec le prix de l'enchant
    //envoyer les infos au shopping manager qui lui va conserver quel enchant ont été achetés
    //faire en sorte de ne pas pouvoir acheter un objet deux fois
    //FAUDRA PENSER A SAVE ENCHANTEMENTS

}
