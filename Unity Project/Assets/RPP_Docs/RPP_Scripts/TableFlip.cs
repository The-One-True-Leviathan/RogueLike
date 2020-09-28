using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TableFlip : MonoBehaviour
{
    public List<Transform> indicatorPosition = new List<Transform>();
    public List<Vector3> overlapSize = new List<Vector3>();
    public LayerMask currentLayer;
    bool tableIsFliped, canFlipBack, canFlipTop, canFlipLeft, canFlipRight;

    private void Start()
    {
        tableIsFliped = false;
    }

    private void FixedUpdate()
    {
        if (tableIsFliped == false)
        {
            DrawOverlaps();
        }
    }

    void FlipTheFuckingTable()
    {
       //if ()
    }

    void DrawOverlaps()
    {
        Collider[] backDetecor = Physics.OverlapBox(indicatorPosition[0].position, overlapSize[0] / 2, Quaternion.identity, currentLayer);
        Collider[] topDetecor = Physics.OverlapBox(indicatorPosition[1].position, overlapSize[1] / 2, Quaternion.identity, currentLayer);
        Collider[] leftDetecor = Physics.OverlapBox(indicatorPosition[2].position, overlapSize[2] / 2, Quaternion.identity, currentLayer);
        Collider[] rightDetecor = Physics.OverlapBox(indicatorPosition[3].position, overlapSize[3] / 2, Quaternion.identity, currentLayer);

        foreach (Collider obj in backDetecor)
        {
            if (obj.CompareTag("Player"))
            {
                canFlipBack = true;
            }
            else
            {
                canFlipBack = false;
            }
        }

        foreach (Collider obj in topDetecor)
        {
            if (obj.CompareTag("Player"))
            {
                canFlipTop = true;
            }
            else
            {
                canFlipTop = false;
            }
        }

        foreach (Collider obj in leftDetecor)
        {
            if (obj.CompareTag("Player"))
            {
                canFlipLeft = true;
            }
            else
            {
                canFlipLeft = false;
            }
        }

        foreach (Collider obj in rightDetecor)
        {
            if (obj.CompareTag("Player"))
            {
                canFlipRight = true;
            }
            else
            {
                canFlipRight = false;
            }
        }
    }
}
