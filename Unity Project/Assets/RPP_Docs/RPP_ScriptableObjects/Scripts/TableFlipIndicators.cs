using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "TableFlipIndicator", menuName = "Raphael/ table flip indicator", order = 1)]
public class TableFlipIndicators : ScriptableObject
{
    public Transform indicatorPosition;
    public Vector3 indicatorSize;
    public LayerMask currentLayer;
    public TableFlip table;
    public MeshRenderer indicator;
    bool canFlipBack;

    private void Start()
    {
        canFlipBack = false;
        indicator.enabled = false;
    }

    private void FixedUpdate()
    {
        PlayerCheck();
    }

    void PlayerCheck()
    {
        Collider[] objects = Physics.OverlapBox(indicatorPosition.position, indicatorSize / 2, Quaternion.identity, currentLayer);

        foreach (Collider obj in objects)
        {
            if (obj.CompareTag("Player"))
            {
                canFlipBack = true;
                indicator.enabled = true;
            }
            else
            {
                canFlipBack = false;
                indicator.enabled = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(indicatorPosition.position, indicatorSize);
    }
}
