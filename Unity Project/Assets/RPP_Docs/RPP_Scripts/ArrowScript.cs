using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public bool canShoot = false, hasBeenShot = false;
    [SerializeField] BoxCollider arrowCollider;
    [SerializeField] GameObject arrowObject;
    public float arrowDamage = 4, arrowFlyingTime = 1, arrowSpeed = 15; 
    public bool goRight, goLeft, goUp, goDown; 

    //Player
    public Player playerScript;

    private void Start()
    {
        canShoot = false;
        arrowCollider = GetComponent<BoxCollider>();
        arrowObject = this.gameObject;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (canShoot)
        {
            StartCoroutine(ShootArrow());
        }
    }

    IEnumerator ShootArrow()
    {        
        {
            if (goRight)
            {
                GoRight();
            }
            if (goLeft)
            {
                GoLeft();
            }
            if (goUp)
            {
                GoUp();
            }
            if (goDown)
            {
                GoDown();
            }
            yield return new WaitForSeconds(arrowFlyingTime);
            arrowObject.SetActive(false);
            hasBeenShot = true;
            canShoot = false;
        }        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerScript.PlayerDamage(arrowDamage);
        }
        if (collision.GetComponent<EnemyDamage>())
        {
            collision.GetComponent<EnemyDamage>().Damage(arrowDamage, 0, arrowObject.transform);
        }
    }

    public void GoRight()
    {
        arrowObject.transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime * arrowSpeed;
    }
    public void GoLeft()
    {
        arrowObject.transform.position += new Vector3(-1f, 0f, 0f) * Time.deltaTime * arrowSpeed;
    }
    public void GoUp()
    {
        arrowObject.transform.position += new Vector3(0f, 0f, 1f) * Time.deltaTime * arrowSpeed;
    }
    public void GoDown()
    {
        arrowObject.transform.position += new Vector3(0f, 0f, -1f) * Time.deltaTime * arrowSpeed;
    }
}
