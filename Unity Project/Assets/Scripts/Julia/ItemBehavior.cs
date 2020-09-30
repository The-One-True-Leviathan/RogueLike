using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace items
{

    public class ItemBehavior : MonoBehaviour
    {
        public ItemScriptableObject itemScriptableObject;
        public HealthBar healthBar;
        // Start is called before the first frame update
        void Start()
        {
            healthBar = GameObject.FindGameObjectWithTag("HUD").GetComponent<HealthBar>();
        }

        // Update is called once per frame
        void Update()
        {

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