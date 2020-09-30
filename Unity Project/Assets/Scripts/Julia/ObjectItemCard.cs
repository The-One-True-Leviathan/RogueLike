using items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectItemCard : MonoBehaviour
{
    public GameObject objectUI;
    public ItemScriptableObject item;
    public Text[] texts;
    // Start is called before the first frame update
    void Start()
    {
        texts = GetComponentsInChildren<Text>();
        objectUI = gameObject;
        texts[0].text = item.itemName;
        texts[1].text = item.itemDescription;
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            objectUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            objectUI.SetActive(false);
        }
    }
}
