using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using Weapons;

public class WeaponItemCard : MonoBehaviour
{
    public GameObject itemUI;
    public WeaponScriptableObject weapon;
    public Text[] texts;
    //public string[] displayEnchantUI;
    public Image panel;

    // Start is called before the first frame update
    public void Initialize()
    {
        weapon.InitializeWeapon();
        Debug.Log("pouet");
        texts = GetComponentsInChildren<Text>();
        panel = GetComponentInChildren<Image>();
        itemUI = gameObject;
        string[] displayEnchantUI = new string[4];
        //displayWeaponNameUI.text = weapon.weaponRealName;
        for (int i = 0; i < 4; i++)
        {
            displayEnchantUI[i] = "<enchantment slot>";
        }
        for (int i = 0; i < weapon.enchantments.Count; i++)
        {
            if (weapon.enchantments[i])
            {
            displayEnchantUI[i] = weapon.enchantments[i].name;
            }
        }
        //displayDescriptionWeaponUI.text = weapon.weaponDescription;
        texts[0].text = weapon.weaponRealName;
        texts[1].text = weapon.weaponDescription;
        texts[2].text = displayEnchantUI[0];
        texts[3].text = displayEnchantUI[1];
        texts[4].text = displayEnchantUI[2];
        texts[5].text = displayEnchantUI[3];
        panel.color = weapon.weaponColor;

    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            itemUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            itemUI.SetActive(false);
        }
    }
}
