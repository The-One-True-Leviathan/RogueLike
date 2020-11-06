using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindExplosiveBarrel : MonoBehaviour
{
    [SerializeField] GameObject barrelObj;

    public void BarrelRewind()
    {
        if(barrelObj.activeSelf == false)
        {
            barrelObj.SetActive(true);
        }
    }
}
