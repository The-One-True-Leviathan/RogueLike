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
    Vector3 currentSpeed;
    public float dropStrength = 1, stopTime, refx, refz;
    public bool dropped = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        interactible = GetComponentInChildren<InteractibleBehavior>();
        rigidBody = GetComponent<Rigidbody>();
        if (player.droppedWeapon != null)
        {
            Dropped();
        }
    }

    public void Dropped()
    {
        dropped = true;
        weapon = player.droppedWeapon;
        currentSpeed = player.attackDirection * dropStrength;
        currentSpeed.x = Mathf.SmoothDamp(currentSpeed.x, 0, ref refx, stopTime);
        currentSpeed.z = Mathf.SmoothDamp(currentSpeed.z, 0, ref refz, stopTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (interactible.interacted)
        {
            player.ChangeWeapon(weapon);
            Object.Destroy(gameObject);
        }
        if (dropped)
        {
            rigidBody.velocity = currentSpeed;
        } else
        {
            if (currentSpeed.x == 0 && currentSpeed.z == 0)
            {
                dropped = false;
            }
        }
    }
}
