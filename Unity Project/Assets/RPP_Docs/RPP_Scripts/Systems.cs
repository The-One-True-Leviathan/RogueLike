using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systems : MonoBehaviour
{
    public int playerHP = 20;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerHP = 20;
        }  
    }

    public void TakeDamage(int damage)
    {
        playerHP -= damage;
    }
}
