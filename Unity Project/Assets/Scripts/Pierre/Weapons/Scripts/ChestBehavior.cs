using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class ChestBehavior : MonoBehaviour
    {
        public GameObject gameManager;
        public RandomWeaponGeneration weaponGeneration;
        public EnchantBookRandomGen enchantGeneration;
        //public RandomBoulonGeneration boulonGeneration;
        public InteractibleBehavior interactibleBehavior;
        public Player player;
        public GenerationDungeonMap dungeonMap;

        public List<WeaponScriptableObject> weaponsInChest;
        public List<Enchantment> enchantmentsInChest;
        public GameObject enchantItemOriginal, boulonItemOriginal;
        public Quality quality;
        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
            dungeonMap = GameObject.FindGameObjectWithTag("Map").GetComponent<GenerationDungeonMap>();
            weaponGeneration = GetComponent<RandomWeaponGeneration>();
            enchantGeneration = GetComponent<EnchantBookRandomGen>();
            //boulonGeneration = GetComponent<RandomBoulonGeneration>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            interactibleBehavior = GetComponentInChildren<InteractibleBehavior>();
        }

        // Update is called once per frame
        void Update()
        {
            if (interactibleBehavior.interacted)
            {
                /* Raph
                 * Son d'ouverture de coffre
                 */
                switch (dungeonMap.nodeBehaviors[dungeonMap.playerIsHere].type)
                {
                    case NodeBehavior.DungeonTypes.BOULON:
                        Boulon();
                        break;
                    case NodeBehavior.DungeonTypes.ENCHANT:
                        Enchant();
                        break;
                    case NodeBehavior.DungeonTypes.WEAPON:
                        Weapon();
                        break;
                }
            }

        }

        private void Weapon()
        {
            weaponGeneration.quality = quality;
            weaponGeneration.Generate();
            weaponsInChest = weaponGeneration.weaponsGenerated;
            Vector3 orientationspace = player.attackDirection;
            for (int i = 0; i < weaponsInChest.Count; i++)
            {
                player.attackDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
                player.attackDirection.Normalize();
                GameObject weaponObj = Instantiate(player.weaponDropOriginal, transform.position + player.attackDirection / 2f, transform.rotation);
                weaponObj.GetComponent<WeaponItemBehavior>().weapon = weaponsInChest[i];
                weaponObj.GetComponent<WeaponItemBehavior>().Dropped();
            }
            player.attackDirection = orientationspace;
            //player.droppedWeapon = dropspace;
            Object.Destroy(gameObject);
        }

        private void Boulon()
        {
            Instantiate(boulonItemOriginal, transform.position, Quaternion.identity);
            Object.Destroy(gameObject);
        }

        private void Enchant()
        {
            enchantGeneration.quality = quality;
            enchantGeneration.Generate();
            enchantmentsInChest = enchantGeneration.enchantGenerated;
            for (int i = 0; i < enchantmentsInChest.Count; i++)
            {
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                direction.Normalize();
                Instantiate(enchantItemOriginal, transform.position + direction / 2f, Quaternion.identity)
                    .GetComponent<EnchantItemBehavior>().enchant = enchantmentsInChest[i];
            }
            Object.Destroy(gameObject);
        }
    }
}
