using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace items
{

    public class ItemBehavior : MonoBehaviour
    {
        public ItemScriptableObject itemScriptableObject;
        public HealthBar healthBar;
        public Compteur compteur;
        public MerchantScript merchantScript;
        public InteractibleBehavior interactibleBehavior;
        public SpriteRenderer spriteRenderer;
        bool used = false;
        public GameObject itemCard;
        bool cardIsOn = false;

        // Start is called before the first frame update
        void Start()
        {
            compteur = GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>();
            healthBar = GameObject.FindGameObjectWithTag("HUD").GetComponent<HealthBar>();
            interactibleBehavior = GetComponentInChildren<InteractibleBehavior>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = itemScriptableObject.itemSprite;
            itemCard = GameObject.Find("ItemCard");
        }

        // Update is called once per frame
        void Update()
        {
            if (interactibleBehavior.interacted && !used)
            {
                if (itemScriptableObject.isFromShop)
                {
                    if (itemScriptableObject.itemPrice >= compteur.piecettesActuelles)
                    {
                        compteur.Buy(itemScriptableObject.itemPrice);
                        merchantScript.BuyingDialogue();
                        ApplyEffect();
                    }
                    else
                    {
                        merchantScript.NoMoneyDialogue();
                    }
                } else
                {
                    ApplyEffect();
                }
            }
            if (interactibleBehavior.interactible)
            {
                if (!cardIsOn)
                {
                    itemCard.GetComponent<RectTransform>().localScale = Vector3.one;
                    cardIsOn = true;
                }else
                {
                    itemCard.GetComponent<RectTransform>().localScale = Vector3.zero;
                    cardIsOn = false;
                }
            }
        } 

        public void ApplyEffect()
        {
            if (!used)
            {
                if (itemScriptableObject.itemType == ItemType.Heal)
                {
                    healthBar.ApplyDamage(-itemScriptableObject.strength);
                }
                else if (itemScriptableObject.itemType == ItemType.MaxPlus)
                {
                    healthBar.UpgradeLife(itemScriptableObject.strength);
                }
                used = true;
            }
            Destroy(gameObject);
        }
    }
}