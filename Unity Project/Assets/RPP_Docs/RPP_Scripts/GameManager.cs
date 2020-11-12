using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Rewind rewind;

    private void Start()
    {
        rewind = GameObject.FindGameObjectWithTag("Player").GetComponent<Rewind>();
    }

    public void EnnemyIsKilled()
    {
        if (rewind.rewindCounter > 3)
        {
            rewind.rewindCounter++;
        }
    }

    public void PlayerIsDamaged()
    {
        if(rewind.rewindCounter > 3)
        {
            rewind.rewindCounter = 0;
        }
    }
}
