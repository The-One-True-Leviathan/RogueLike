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

    //Player
    public Player playerScript;

    private void Start()
    {
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
        arrowObject.transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime * arrowSpeed;
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
