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
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        interactible = GetComponentInChildren<InteractibleBehavior>();
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
        weapon = player.droppedWeapon;
        weapon.InitializeWeapon();
        sprite.sprite = weapon.weaponItemSprite;
        GetComponentInChildren<WeaponItemCard>().weapon = weapon;
        GetComponentInChildren<WeaponItemCard>().Initialize();
        gameObject.name = weapon.weaponRealName;
        currentSpeed = player.attackDirection * dropStrength;
        rigidBody.AddForce(currentSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon == null)
        {
            if (player.droppedWeapon != null)
            {
                Dropped();
                player.droppedWeapon = null;
            }
        } else
        {
            if (interactible.interacted)
            {
                player.ChangeWeapon(weapon);
                Object.Destroy(gameObject);
            }
            animator.SetBool("Open", interactible.interactible);
        }
    }
}
