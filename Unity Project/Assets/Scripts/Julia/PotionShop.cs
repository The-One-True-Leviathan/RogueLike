﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PotionShop : MonoBehaviour
{
    public static bool potionIsDrinked = false;
    public bool potionShopOpen = false;
    public Player player;
    public HealthBar healthBar;
    public GameObject potionShopCanvas;
    public Collider potionShopCollider;
    public InteractibleBehavior interactible;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthBar = GameObject.FindGameObjectWithTag("HUD").GetComponentInChildren<HealthBar>();
        potionShopCanvas = GameObject.Find("Canvas potions");
        potionShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
        interactible = GetComponentInChildren<InteractibleBehavior>();


        resetPotionEffect();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactible.interacted && !potionShopOpen)
        {
            potionShopCanvas.GetComponent<RectTransform>().localScale = Vector3.one;
            potionShopOpen = true;
        }
        else if(interactible.interacted && potionShopOpen)
        {
            potionShopCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
            potionShopOpen = false;
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

        }
    }

    public void strenghtPotion()
    {
        if(!potionIsDrinked)
        {
        player.strenght = true;
        potionIsDrinked = true;

        }
    }

    public void lifePotion()
    {
        if(!potionIsDrinked)
        {
        healthBar.UpgradeLife(5);
        potionIsDrinked = true;

        }
    }
    //si interaction avec la boutique ouvrir le canvas de la boutique
    //dissimuler les potions en bouton
    //fpnctions pour chaque bouton qui modifie le player script
    //potionIsDrink
    //reset quand retour au hub 
    //les effets doivent rester dans le dnj
}
