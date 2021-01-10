using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class WeaponItemBehavior : MonoBehaviour
{
    public WeaponScriptableObject weapon;
    public InteractibleBehavior interactible;
    public Player player;
    public Rigidbody rigidBody;
    public Animator animator;
    Vector3 currentSpeed;
    public float dropStrength = 1, stopTime, refx, refz;
    public bool dropped = false;
    public SpriteRenderer sprite;
    public bool isFromShop = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (weapon != null)
        {
            weapon = Object.Instantiate(weapon) as WeaponScriptableObject;
        }

        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //interactible = GetComponentInChildren<InteractibleBehavior>();
        rigidBody = GetComponent<Rigidbody>();
        if (weapon != null)
        {
            weapon.InitializeWeapon();
            sprite.sprite = weapon.weaponItemSprite;
            GetComponentInChildren<WeaponItemCard>().weapon = weapon;
            GetComponentInChildren<WeaponItemCard>().Initialize();
            gameObject.name = weapon.weaponRealName;
        }
    }

    public void Dropped()
    {
        Awake();
        weapon.InitializeWeapon();
        currentSpeed = player.attackDirection * dropStrength;
        rigidBody.AddForce(currentSpeed, ForceMode.Impulse);
        StartCoroutine(DroppedCorou());
    }

    private IEnumerator DroppedCorou()
    {
        yield return new WaitForSeconds(0.1f);
        sprite.sprite = weapon.weaponItemSprite;
        GetComponentInChildren<WeaponItemCard>().weapon = weapon;
        GetComponentInChildren<WeaponItemCard>().Initialize();
        gameObject.name = weapon.weaponRealName;
    }

    public void Shop()
    {
        weapon = Object.Instantiate(weapon) as WeaponScriptableObject;
        weapon.InitializeWeapon();
        sprite.sprite = weapon.weaponItemSprite;
        GetComponentInChildren<WeaponItemCard>().weapon = weapon;
        GetComponentInChildren<WeaponItemCard>().Initialize();
        gameObject.name = weapon.weaponRealName;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Open", interactible.interactible);
        Debug.Log(interactible.interactible);
        if (interactible.interacted)
        {
            if (isFromShop)
            {
                if(GetComponent<ShopWeapon>().weaponPrice <= GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>().piecettesActuelles)
                {
                    GameObject.Find("Merchant").GetComponent<MerchantScript>().BuyingDialogue();
                    GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>().Buy(GetComponent<ShopWeapon>().weaponPrice);
                    player.ChangeWeapon(weapon);
                    Object.Destroy(gameObject);
                }
                else
                {
                    GameObject.Find("Merchant").GetComponent<MerchantScript>().NoMoneyDialogue();
                }
                interactible.interacted = false;
            }

            else
            {
                player.ChangeWeapon(weapon);
                Object.Destroy(gameObject);

            }
        }

    }
}
