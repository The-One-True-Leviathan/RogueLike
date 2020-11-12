using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


//enum Direction { Haut, Bas, Gauche, Droite }
public class TableFlip : MonoBehaviour
{
    public List<GameObject> indicatorObject = new List<GameObject>();
    public List<Transform> indicatorPosition = new List<Transform>();
    public List<Vector3> overlapSize = new List<Vector3>();
    public LayerMask currentLayer;
    public Controler controler;
    public List<InteractibleBehavior> interactibleBehaviors;
    bool tableIsFliped, canFlipBack, canFlipTop, canFlipLeft, canFlipRight;
    Animator animator;
    public EnemyDamage enemyDamageTable;
    [SerializeField] GameObject tableObj;
    [SerializeField] ParticleSystem tableDestruction;

    private void Start()
    {
        tableDestruction.Stop();
        tableIsFliped = false;
        foreach (GameObject obj in indicatorObject)
        {
            obj.SetActive(false);
        }
        animator = GetComponent<Animator>();
        enemyDamageTable.isTable = true;
    }

    private void FixedUpdate()
    {
        if (tableIsFliped == false)
        {
            DrawOverlaps();
        }
        for (int i = 0; i < interactibleBehaviors.Count; i++)
        {
            if (interactibleBehaviors[i].interacted)
            {
                FlipTheFuckingTable(i);
            }
        }

        if (enemyDamageTable.currentHP <= 0)
        {
            tableDestruction.Play();
            tableObj.SetActive(false);
        }
    }

    void FlipTheFuckingTable(int side)
    {        
        switch (side) 
        {
            case 0:
                Debug.Log("Flip Back");
                animator.SetInteger("FlipInt", 0);
                indicatorObject[0].SetActive(false);
                tableIsFliped = true;
                break;
            case 1:
                Debug.Log("Flip Top");
                animator.SetInteger("FlipInt", 1);
                indicatorObject[1].SetActive(false);
                tableIsFliped = true;
                break;
            case 2:
                Debug.Log("Flip Right");
                animator.SetInteger("FlipInt", 2);
                indicatorObject[3].SetActive(false);
                tableIsFliped = true;
                break;
            case 3:
                Debug.Log("Flip Left");
                animator.SetInteger("FlipInt", 3);
                indicatorObject[2].SetActive(false);
                tableIsFliped = true;
                break;
        }

       /*if (canFlipBack)
       {
            animator.SetTrigger("FlipBack");
            tableIsFliped = true;
       }
       else if (canFlipTop)
       {
            animator.SetTrigger("FlipFront");
            tableIsFliped = true;
       }
       else if (canFlipLeft)
       {
            animator.SetTrigger("FlipLeft");
            tableIsFliped = true;
       }
       else if (canFlipRight)
       {
            animator.SetTrigger("FlipRight");
            tableIsFliped = true;
       }*/
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
