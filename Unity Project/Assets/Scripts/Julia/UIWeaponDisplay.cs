using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponDisplay : MonoBehaviour
{
    public Image weaponDisplay1, weaponDisplay2;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        weaponDisplay1.sprite = player.weapon1.weaponIcon;
        if (player.dualWielding)
        {
            weaponDisplay2.sprite = player.weapon2.weaponIconSecondary;
        }
        else
        {
            weaponDisplay2.sprite = player.weapon1.weaponIconSecondary;
        }
    }
}
