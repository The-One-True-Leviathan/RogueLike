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
        public Animator animator;
        bool cardIsOn = false;
        [SerializeField]
        bool dialogueWasSaid = false;

        // Start is called before the first frame update
        void Awake()
        {
            compteur = GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>();
            healthBar = GameObject.FindGameObjectWithTag("HUD").GetComponent<HealthBar>();
            interactibleBehavior = GetComponentInChildren<InteractibleBehavior>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = itemScriptableObject.itemSprite;
            animator = itemCard.GetComponent<Animator>();
            if(itemScriptableObject.isFromShop)
            {
                merchantScript = GameObject.Find("Merchant").GetComponent<MerchantScript>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (interactibleBehavior.interacted && !used)
            {
                if (itemScriptableObject.isFromShop)
                {
                    if (itemScriptableObject.itemPrice <= compteur.piecettesActuelles)
                    {
                        used = true;
                        compteur.Buy(itemScriptableObject.itemPrice);
                        merchantScript.BuyingDialogue();
                        ApplyEffect();
                        
                    }
                    else if (!dialogueWasSaid)
                    {
                        dialogueWasSaid = true;
                        merchantScript.NoMoneyDialogue();
                        
                    }
                } else
                {
                    used = true;
                    ApplyEffect();
                }
                interactibleBehavior.interacted = false;
            }
            if(animator!=null)
            {
                animator.SetBool("Open", interactibleBehavior.interactible);
            }
            
            dialogueWasSaid = false;
        } 

        public void ApplyEffect()
        {
            switch(itemScriptableObject.itemType)
            {
                case ItemType.Heal:
                    healthBar.ApplyDamage(-itemScriptableObject.strength);
                    break;

                case ItemType.MaxPlus:
                    healthBar.UpgradeLife(itemScriptableObject.strength);
                    break;

                case ItemType.Boulon:
                    compteur.GainBoulon(1);
                    break;
            }
            

                used = true;
            
            Destroy(gameObject);
        }
    }
}