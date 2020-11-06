using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTable : MonoBehaviour
{
    [SerializeField] GameObject tableObj;

    public void TableRewind()
    {
        if (tableObj.activeSelf == false)
        {
            tableObj.SetActive(true);
        }
    }
}
