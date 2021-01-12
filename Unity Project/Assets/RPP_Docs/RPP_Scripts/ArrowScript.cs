using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public bool canShoot = false;
    [SerializeField] BoxCollider arrowCollider;
    [SerializeField] GameObject arrowObject;
    public float arrowSpeed = 15;
    public float arrowDamage = 4;
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

    void Update()
    {
        if (canShoot)
        {
            StartCoroutine(ShootArrow());
        }
    }

    IEnumerator ShootArrow()
    {        
        if (goRight)
        {
            arrowObject.transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime * arrowSpeed;
        }
        if (goLeft)
        {
            arrowObject.transform.position += new Vector3(-1f, 0f, 0f) * Time.deltaTime * arrowSpeed;
        }
        if (goUp)
        {
            arrowObject.transform.position += new Vector3(0f, 0f, 1f) * Time.deltaTime * arrowSpeed;
        }
        if (goDown)
        {
            arrowObject.transform.position += new Vector3(0f, 0f, -1f) * Time.deltaTime * arrowSpeed;
        }
        yield return new WaitForSeconds(4f);
        arrowObject.SetActive(false);
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
}
