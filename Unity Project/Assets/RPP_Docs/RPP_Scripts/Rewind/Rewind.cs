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
    [SerializeField] AudioClip rewind;
    [SerializeField] AudioSource audioSource;

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
            audioSource.clip = rewind;
            audioSource.Play();

            Collider[] objects = Physics.OverlapSphere(playerTransform.position, rewindRange, rewindLayer);

            foreach (Collider obj in objects)
            {
                if (obj.gameObject.GetComponent<RewindExplosiveBarrel>())
                {
                    Debug.Log("Detected Barrel");
                    obj.GetComponent<RewindExplosiveBarrel>().BarrelRewind();
                }
                if (obj.gameObject.GetComponent<RewindArrowLauncher>())
                {
                    Debug.Log("Detected Arrow Launcher");
                    obj.GetComponent<RewindArrowLauncher>().canRewindArrow = true;
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
        if (rewindCounter < 3)
        {
            rewindCounter++;
        }
    }

    public void PlayerIsDamaged()
    {
            rewindCounter = 0;
    }
}
