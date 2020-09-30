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
        // Start is called before the first frame update
        void Start()
        {
            healthBar = GameObject.FindGameObjectWithTag("HUD").GetComponent<HealthBar>();
            interactibleBehavior = GetComponentInChildren<InteractibleBehavior>();
        }

        // Update is called once per frame
        void Update()
        {
            if (interactibleBehavior.interacted && itemScriptableObject.isFromShop)
            {
                if (itemScriptableObject.itemPrice >= compteur.piecettesActuelles)
                {
                compteur.Buy(itemScriptableObject.itemPrice);
                ApplyEffect();
                }
                else
                {
                    //animation de l'itemCard
                }
            }
            if (interactibleBehavior.interactible)
            {

            }
        }

        private void OnCollisionEnter(Collision player)
        {
            if (player.gameObject.tag == "Player" && itemScriptableObject.isFromShop == false)
            {
                ApplyEffect();
            }
        }

        public void ApplyEffect()
        {
            if (itemScriptableObject.itemType == ItemType.Heal)
            {
                healthBar.ApplyDamage(-itemScriptableObject.strength);
            }
            else if (itemScriptableObject.itemType == ItemType.MaxPlus)
            {
                healthBar.UpgradeLife(itemScriptableObject.strength);
            }
            Destroy(gameObject, 1);
        }
    }
}