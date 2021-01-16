using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindArrowLauncher : MonoBehaviour
{
    public Vector3 launcherLocation;
    [SerializeField] ArrowScript arrowScript;
    public GameObject arrowObject;
    public bool canRewindArrow;
    [SerializeField] AudioClip reverseLaunch;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        launcherLocation = this.gameObject.transform.position;
        canRewindArrow = false;
    }

    private void Update()
    {
        if (canRewindArrow && arrowScript.hasBeenShot)
        {
            StartCoroutine(ArrowGoingBack());
            Debug.Log("Successfully Called Rewind Arrow Launcher");
        }
        else if (canRewindArrow && !arrowScript.hasBeenShot)
        {
            canRewindArrow = false;
        }
    }
 
    IEnumerator ArrowGoingBack()
    {
        audioSource.clip = reverseLaunch;
        arrowObject.SetActive(true);
        audioSource.Play();
        arrowObject.transform.position = Vector3.MoveTowards(arrowObject.transform.position, launcherLocation, Time.deltaTime * arrowScript.arrowSpeed);
        Debug.Log("Arrow is back in Launcher");
        yield return new WaitForSeconds(arrowScript.arrowFlyingTime);
        canRewindArrow = false;
        arrowScript.hasBeenShot = false;
    }
}
