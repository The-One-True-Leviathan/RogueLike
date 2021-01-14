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
        controler = new Controler();
        controler.Keyboard.Enable();
        controler.Keyboard.Rewind.performed += ctx => CallRewind();
    }

    void CallRewind()
    {
        Debug.Log("Called Rewind");
        if (rewindCounter >= 3)
        {
            Debug.Log("Rewind Successful");
            Collider[] objects = Physics.OverlapSphere(playerTransform.position, rewindRange, rewindLayer);

            foreach (Collider obj in objects)
            {
                if (obj.gameObject.CompareTag("BarrelMAIN"))
                {
                    Debug.Log("Detected Barrel");
                    obj.GetComponent<RewindExplosiveBarrel>().BarrelRewind();
                }
                /*if (obj.gameObject.CompareTag("TEST"))
                {
                    Debug.Log("Detected Test Object");
                }*/
            }
            rewindCounter = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, rewindRange);
    }

    public void EnnemyIsKilled()
    {
        if (rewindCounter > 3)
        {
            rewindCounter++;
        }
    }

    public void PlayerIsDamaged()
    {
        if (rewindCounter > 3)
        {
            rewindCounter = 0;
        }
    }
}
