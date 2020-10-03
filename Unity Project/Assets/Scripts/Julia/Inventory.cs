using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Controler controller;
    public GameObject inventoryItem;
    public Player player;
    public Image[] weaponImages;
    public Text[] texts;
    public bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = new Controler();
        controller.Enable();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        texts = GetComponentsInChildren<Text>();
        weaponImages = GetComponentsInChildren<Image>();
        inventoryItem = gameObject;
    }

    void UpdateInventory()
    {
        string[] displayEnchantInventory1 = new string[4];
        string[] displayDescriptionEnchant1 = new string[4];
        for (int i = 0; i < 4; i++)
        {
            displayEnchantInventory1[i] = "<enchantment slot>";
            displayDescriptionEnchant1[i] = "<enchant this by attacking a book>";
        }
        for (int i = 0; i < player.weapon1.enchantments.Count; i++)
        {
            if (player.weapon1.enchantments[i])
            {
                displayEnchantInventory1[i] = player.weapon1.enchantments[i].name;
                displayDescriptionEnchant1[i] = player.weapon1.enchantments[i].description;
            }
        }
        for (int i = 0; i < 4; i++)
        {
                texts[i + 2].text = displayEnchantInventory1[i];
                texts[i + 2].color = player.weapon1.enchantments[i].color;
                texts[i + 12].text = displayDescriptionEnchant1[i];
        }
        texts[0].text = player.weapon1.weaponRealName;
        texts[0].color = player.weapon1.weaponColor;
        texts[1].text = player.weapon1.weaponDescription;
        weaponImages[1].sprite = player.weapon1.weaponItemSprite;
        if (player.dualWielding)
        {
            string[] displayEnchantInventory2 = new string[4];
            string[] displayDescriptionEnchant2 = new string[4];
            for (int i = 0; i < 4; i++)
            {
                displayEnchantInventory2[i] = "<enchantment slot>";
                displayDescriptionEnchant2[i] = "<enchant this by attacking a book>";
            }
            for (int i = 0; i < player.weapon2.enchantments.Count; i++)
            {
                if (player.weapon2.enchantments[1])
                {
                    displayEnchantInventory2[i] = player.weapon2.enchantments[i].name;
                    displayDescriptionEnchant2[i] = player.weapon2.enchantments[i].description;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                    texts[i + 8].text = displayEnchantInventory2[i];
                    texts[i + 8].color = player.weapon2.enchantments[i].color;
                    texts[i + 16].text = displayDescriptionEnchant2[i];
            }
            texts[6].text = player.weapon2.weaponRealName;
            texts[6].color = player.weapon2.weaponColor;
            texts[7].text = player.weapon2.weaponDescription;
            weaponImages[2].sprite = player.weapon2.weaponItemSprite;
        }
        else if (player.dualWielding == false)
        {
            texts[6].text = "<empty>";
            texts[7].text = "<no weapon>";
            string[] displayEnchantInventory2 = new string[4];
            string[] displayDescriptionEnchant2 = new string[4];
            for (int i = 0; i < 4; i++)
            {
                displayEnchantInventory2[i] = "<enchantment slot>";
                displayDescriptionEnchant2[i] = "<no weapon>";
                texts[i + 8].text = displayEnchantInventory2[i];
                texts[i + 16].text = displayDescriptionEnchant2[i];
            }
        }
    }

    private void Update()
    {
        if (controller.Keyboard.Inventory.triggered && isOpen == false)
        {
            inventoryItem.SetActive(true);
            UpdateInventory();
            isOpen = true;
        }
        if(controller.Keyboard.Inventory.triggered && isOpen == true)
        {
            inventoryItem.SetActive(false);
            isOpen = false;
        }
    }
}
