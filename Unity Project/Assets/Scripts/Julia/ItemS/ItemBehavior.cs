using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace items
{

    public class ItemBehavior : MonoBehaviour
    {
        public ItemScriptableObject itemScriptableObject;
        public HealthBar healthBar;
        public Compteur compteur;
        public InteractibleBehavior interactibleBehavior;
        public SpriteRenderer spriteRenderer;
        bool used = false;
        // Start is called before the first frame update
        void Start()
        {
            compteur = GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>();
            healthBar = GameObject.FindGameObjectWithTag("HUD").GetComponent<HealthBar>();
            interactibleBehavior = GetComponentInChildren<InteractibleBehavior>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = itemScriptableObject.itemSprite;
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
                        ApplyEffect();
                    }
                    else
                    {
                        //Not enough moneh !!
                    }
                } else
                {
                    ApplyEffect();
                }
            }
            if (interactibleBehavior.interactible)
            {
                //animation de l'itemCard
            }
        }
        /*
        private void OnCollisionEnter(Collision player)
        {
            if (player.gameObject.tag == "Player" && itemScriptableObject.isFromShop == false)
            {
                ApplyEffect();
            }
        }*/

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