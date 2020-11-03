using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rewind : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    public float rewindRange;
    [SerializeField] LayerMask rewindLayer;
    public int rewindCounter = 0;
    Controler controler;

    void Awake()
    {
        controler.Keyboard.Rewind.performed += ctx => CallRewind();
    }

    void CallRewind()
    {
        if (rewindCounter == 3)
        {
            Collider[] objects = Physics.OverlapSphere(playerTransform.position, rewindRange, rewindLayer);

            foreach (Collider obj in objects)
            {
                if (obj.GetComponent<RewindTable>())
                {
                    // Resets Table
                }
                if (obj.GetComponent<RewindExplosiveBarrel>())
                {
                    // Resets Explosive Barrel
                }
            }

            rewindCounter = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, rewindRange);
    }
}
