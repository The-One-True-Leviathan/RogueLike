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
        public InteractibleBehavior interactibleBehavior;
        public Player player;

        public List<WeaponScriptableObject> weaponsInChest;
        public List<Enchantment> enchantmentsInChest;
        public Quality quality;
        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
            weaponGeneration = gameManager.GetComponent<RandomWeaponGeneration>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            interactibleBehavior = GetComponentInChildren<InteractibleBehavior>();
            weaponGeneration.quality = quality;
            weaponGeneration.Generate();
            weaponsInChest = weaponGeneration.weaponsGenerated;
        }

        // Update is called once per frame
        void Update()
        {
            if (interactibleBehavior.interacted)
            {
                WeaponScriptableObject dropspace = player.droppedWeapon;
                Vector3 orientationspace = player.attackDirection;
                for (int i = 0; i < weaponsInChest.Count; i++)
                {
                    player.droppedWeapon = weaponsInChest[i];
                    player.attackDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
                    player.attackDirection.Normalize();
                    WeaponScriptableObject saveWeapon = player.weaponDropOriginal.GetComponent<WeaponItemBehavior>().weapon;
                    player.weaponDropOriginal.GetComponent<WeaponItemBehavior>().weapon = weaponsInChest[i];
                    Instantiate(player.weaponDropOriginal, transform.position + player.attackDirection / 2f, transform.rotation);
                    player.weaponDropOriginal.GetComponent<WeaponItemBehavior>().weapon = saveWeapon;
                }
                player.attackDirection = orientationspace;
                //player.droppedWeapon = dropspace;
                Object.Destroy(gameObject);
            }
        }
    }
}
