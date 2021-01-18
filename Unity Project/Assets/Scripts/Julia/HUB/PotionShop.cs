using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionShop : MonoBehaviour
{
    public static bool potionIsDrinked = false;
    public bool potionShopOpen = false;
    public Button str;
    public Player player;
    public HealthBar healthBar;
    public GameObject potionShopCanvas;
    public InteractibleBehavior interactible;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthBar = GameObject.FindGameObjectWithTag("HUD").GetComponentInChildren<HealthBar>();
        potionShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
        interactible = GetComponentInChildren<InteractibleBehavior>();


        resetPotionEffect();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactible.interacted && !potionShopOpen && !potionIsDrinked)
        {
            potionShopCanvas.GetComponent<RectTransform>().localScale = Vector3.one;
            potionShopOpen = true;
            Time.timeScale = 0f;
            interactible.interacted = false;
            str.Select();
        }
        else if(interactible.interacted && potionShopOpen)
        {
            potionShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
            potionShopOpen = false;
            Time.timeScale = 1f;
            interactible.interacted = false;
            GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>().Select();
        }
    }

    public void resetPotionEffect()
    {
        healthBar.vieTemp = healthBar.max1;
        healthBar.vieMax = healthBar.max1;

        if (potionIsDrinked)
        {
            player.speed = false;
            player.strenght = false;
            potionIsDrinked = false;
        }
    }
    public void speedPotion()
    {
        if(!potionIsDrinked)
        {
        player.speed = true;
        potionIsDrinked = true;
            potionShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
            Time.timeScale = 1f;
            potionShopOpen = false;
        }
        GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>().Select();
    }

    public void strenghtPotion()
    {
        if(!potionIsDrinked)
        {
        player.strenght = true;
        potionIsDrinked = true;
            potionShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
            Time.timeScale = 1f;
            potionShopOpen = false;
        }
        GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>().Select();


    }

    public void lifePotion()
    {
        if(!potionIsDrinked)
        {
        healthBar.UpgradeLife(5);
        potionIsDrinked = true;
            potionShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
            Time.timeScale = 1f;
            potionShopOpen = false;
        }
        GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>().Select();
    }
    //si interaction avec la boutique ouvrir le canvas de la boutique
    //dissimuler les potions en bouton
    //fpnctions pour chaque bouton qui modifie le player script
    //potionIsDrink
    //reset quand retour au hub 
    //les effets doivent rester dans le dnj
}
