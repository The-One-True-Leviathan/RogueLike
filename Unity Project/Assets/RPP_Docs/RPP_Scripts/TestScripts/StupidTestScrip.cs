using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidTestScrip : MonoBehaviour
{
    public Transform mainObject, positionA, positionB;
    public int speed = 2;

    private void Start()
    {
        mainObject.position = positionA.position;
    }

    void Update()
    {
        if (mainObject.position != positionB.position)
        {
            mainObject.position = positionB.position / 10 * Time.deltaTime * 2;
        }
        if (mainObject.position != positionA.position)
        {
            mainObject.position = positionA.position / 10 * Time.deltaTime * 2;
        }

    }
}
