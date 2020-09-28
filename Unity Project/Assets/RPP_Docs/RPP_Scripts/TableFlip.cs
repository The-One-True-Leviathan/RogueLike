using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class TableFlip : MonoBehaviour
{
    public List<GameObject> indicatorObject = new List<GameObject>();
    public List<Transform> indicatorPosition = new List<Transform>();
    public List<Vector3> overlapSize = new List<Vector3>();
    public LayerMask currentLayer;
    public Controler controler;
    bool tableIsFliped, canFlipBack, canFlipTop, canFlipLeft, canFlipRight;

    private void Start()
    {
        tableIsFliped = false;
        foreach (GameObject obj in indicatorObject)
        {
            obj.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (tableIsFliped == false)
        {
            DrawOverlaps();
        }
        controler.Keyboard.Interact.performed += ctx => FlipTheFuckingTable();
    }

    void FlipTheFuckingTable()
    {
       if (canFlipBack)
       {
            //flip the table backwards
            tableIsFliped = true;
       }
       else if (canFlipTop)
       {
            //flip the table on top
            tableIsFliped = true;
       }
       else if (canFlipLeft)
       {
            //flip the table left
            tableIsFliped = true;
       }
       else if (canFlipRight)
       {
            //flip th table right
            tableIsFliped = true;
       }
    }

    void DrawOverlaps()
    {
        Collider[] backDetecor = Physics.OverlapBox(indicatorPosition[0].position, overlapSize[0] / 2, Quaternion.identity, currentLayer);
        Collider[] topDetecor = Physics.OverlapBox(indicatorPosition[1].position, overlapSize[1] / 2, Quaternion.identity, currentLayer);
        Collider[] leftDetecor = Physics.OverlapBox(indicatorPosition[2].position, overlapSize[2] / 2, Quaternion.identity, currentLayer);
        Collider[] rightDetecor = Physics.OverlapBox(indicatorPosition[3].position, overlapSize[3] / 2, Quaternion.identity, currentLayer);

        indicatorObject[0].SetActive(false);
        canFlipBack = false;
        indicatorObject[1].SetActive(false);
        canFlipTop = false;
        indicatorObject[2].SetActive(false);
        canFlipLeft = false;
        indicatorObject[3].SetActive(false);
        canFlipRight = false;

        foreach (Collider obj in backDetecor)
        {
            if (obj.CompareTag("Player"))
            {
                indicatorObject[0].SetActive(true);
                canFlipBack = true;
            }
        }

        foreach (Collider obj in topDetecor)
        {
            if (obj.CompareTag("Player"))
            {
                indicatorObject[1].SetActive(true);
                canFlipTop = true;
            }
        }

        foreach (Collider obj in leftDetecor)
        {
            if (obj.CompareTag("Player"))
            {
                indicatorObject[2].SetActive(true);
                canFlipLeft = true;
            }
        }

        foreach (Collider obj in rightDetecor)
        {
            if (obj.CompareTag("Player"))
            {
                indicatorObject[3].SetActive(true);
                canFlipRight = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(indicatorPosition[0].position, overlapSize[0]);
        Gizmos.DrawWireCube(indicatorPosition[1].position, overlapSize[1]);
        Gizmos.DrawWireCube(indicatorPosition[2].position, overlapSize[2]);
        Gizmos.DrawWireCube(indicatorPosition[3].position, overlapSize[3]);
    }
}
