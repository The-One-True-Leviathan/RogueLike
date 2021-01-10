using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Controler controller;
    public GameObject inventoryItem;
    public GameObject subcanvas;
    public Player player;
    public Image[] weaponImages;
    public Text[] texts;
    public bool isOpen = false;
    public List<string> displayEnchantInventory1, displayDescriptionEnchant1, displayEnchantInventory2, displayDescriptionEnchant2;

    // Start is called before the first frame update
    void Start()
    {
        controller = new Controler();
        controller.Enable();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        texts = GetComponentsInChildren<Text>();
        weaponImages = GetComponentsInChildren<Image>();
        inventoryItem = gameObject;
        subcanvas = GameObject.FindGameObjectWithTag("InventoryCanvas");
        for (int i = 0; i < 4; i++)
        {
            displayEnchantInventory1.Add("<enchantment slot>");
            displayDescriptionEnchant1.Add("<enchant this by attacking a book>");
            displayEnchantInventory2.Add("<enchantment slot>");
            displayDescriptionEnchant2.Add("<enchant this by attacking a book>");
        }
    }

    void UpdateInventory()
    {
        player.weapon1.InitializeWeapon();
        player.weapon2.InitializeWeapon();
        for (int i = 0; i < 4; i++)
        {
            displayEnchantInventory1[i] = "<enchantment slot>";
            displayDescriptionEnchant1[i] = "<enchant this by attacking a book>";
            texts[i + 2].color = Color.grey;
            texts[i + 12].color = Color.grey;
        }
        for (int i = 0; i < player.weapon1.enchantments.Count; i++)
        {
            displayEnchantInventory1[i] = player.weapon1.enchantments[i].enchantmentName;
            texts[i + 2].color = Color.grey;
            texts[i + 2].color = player.weapon1.enchantments[i].color;
            displayDescriptionEnchant1[i] = player.weapon1.enchantments[i].description;
            texts[i + 12].color = Color.grey;
            texts[i + 12].color = player.weapon1.enchantments[i].color;

        }
        for (int i = 0; i < 4; i++)
        {
            texts[i + 2].text = displayEnchantInventory1[i];
            texts[i + 12].text = displayDescriptionEnchant1[i];
        }
        texts[0].text = player.weapon1.weaponRealName;
        texts[0].color = player.weapon1.weaponColor;
        texts[1].text = player.weapon1.weaponDescription;
        weaponImages[1].sprite = player.weapon1.weaponIcon;
        if (player.dualWielding)
        {
            for (int i = 0; i < 4; i++)
            {
                displayEnchantInventory2[i] = "<enchantment slot>";
                displayDescriptionEnchant2[i] = "<enchant this by attacking a book>";
                texts[i + 8].color = Color.grey;
                texts[i + 16].color = Color.grey;
            }
            for (int i = 0; i < player.weapon2.enchantments.Count; i++)
            {
                displayEnchantInventory2[i] = player.weapon2.enchantments[i].enchantmentName;
                texts[i + 8].color = Color.grey;
                texts[i + 8].color = player.weapon2.enchantments[i].color;
                displayDescriptionEnchant2[i] = player.weapon2.enchantments[i].description;
                texts[i + 16].color = Color.grey;
                texts[i + 16].color = player.weapon2.enchantments[i].color;

            }
            for (int i = 0; i < 4; i++)
            {
                texts[i + 8].text = displayEnchantInventory2[i];
                texts[i + 16].text = displayDescriptionEnchant2[i];
            }
            texts[6].text = player.weapon2.weaponRealName;
            texts[6].color = player.weapon2.weaponColor;
            texts[7].text = player.weapon2.weaponDescription;
            weaponImages[2].sprite = player.weapon2.weaponIcon;
        }
        else if (player.dualWielding == false)
        {
            texts[6].text = "<empty>";
            texts[7].text = "<no weapon>";
            for (int i = 0; i < 4; i++)
            {
                displayEnchantInventory2[i] = "";
                displayDescriptionEnchant2[i] = "";
                texts[i + 8].text = displayEnchantInventory2[i];
                texts[i + 16].text = displayDescriptionEnchant2[i];
                texts[i + 8].color = Color.grey;
                texts[i + 16].color = Color.grey;
            }
            weaponImages[2].sprite = player.weapon1.weaponIconSecondary;
        }
    }

    private void Update()
    {
        if (controller.Keyboard.Inventory.triggered)
        {
            isOpen = !isOpen;
            UpdateInventory();
        }
        if(isOpen)
        {
            subcanvas.GetComponent<RectTransform>().localScale = Vector3.one;
        } else
        {
            subcanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }
}
