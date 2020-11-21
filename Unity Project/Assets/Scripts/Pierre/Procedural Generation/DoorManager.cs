using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject door;


    public void Open()
    {
        door.SetActive(false);
    }

    public void Close()
    {
        door.SetActive(true);
    }
}
