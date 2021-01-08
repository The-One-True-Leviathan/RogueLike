using items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectItemCard : MonoBehaviour
{
    public GameObject objectUI;
    public GameObject parent;
    public ItemScriptableObject item;
    public Text[] texts;
    public Text priceText;
    // Start is called before the first frame update
    void Start()
    {
        item = parent.GetComponent<ItemBehavior>().itemScriptableObject;
        texts = GetComponentsInChildren<Text>();
        objectUI = gameObject;
        texts[0].text = item.itemName;
        texts[1].text = item.itemDescription;
        priceText.enabled = false;
        priceText.text = "Costs : " + item.itemPrice.ToString() + " Coins"; 
        if(item.isFromShop == true)
        {
            priceText.enabled = true;
        }

    }
}
