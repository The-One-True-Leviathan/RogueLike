using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Scripting.APIUpdating;

public class GolemAI : MonoBehaviour
{

    GameObject player;
    public Player playerScript;
    Rigidbody rigidBody;
    EnemyDamage damageManager;
    public Transform rayCastOrigin;

    //Line of Sight
    public float maxSight, reactTime; //How far can the enemy see ? / How fast does he react to the player ?
    public float maxAwaken; //How far away should we start drawing raycasts to detect the player 
    public float maxSpeed;

    public LayerMask blockLOS;
    public LayerMask isPlayer;

    Vector3 enemyToPlayer, angleToPlayer, targetVelocity, currentVelocity;
    float refVelocityx, refVelocityz;
    public float accelerationTime;
    public float distanceToPlayer;

    public bool playerInSight, playerInMind;
    public bool playerInRange;

    //Combat
    public float attackRange, attackDamage, attackKnockback;
    public bool isInAttack, isInBuildup, isAttacking, isInRecover;
    public float buildupTime, attackTime, recoveryTime, attackDimensionsLength, attackDimensionsDepth;
    float buildupTimeElapsed, attackTimeElapsed, recoveryTimeElapsed;
    Vector3 attackAngle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody>();
        damageManager = GetComponent<EnemyDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Activation();
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Activation()
    {
        enemyToPlayer = player.transform.position - rayCastOrigin.position;
        distanceToPlayer = enemyToPlayer.magnitude;
        if (!damageManager.isInKnockback)
        {
            rigidBody.velocity = Vector3.zero;
        }
        if (distanceToPlayer < maxAwaken)
        {
            angleToPlayer = enemyToPlayer;
            angleToPlayer.Normalize();
            Sightcast();
            if (playerInSight && !playerInMind)
            {
                StartCoroutine("ReactCoroutine");
            }
            if (!playerInSight)
            {
                StopCoroutine("ReactCoroutine");
                playerInMind = false;
            }
            if (playerInMind)
            {
                React();
            }
        }
        
    }

    public IEnumerator ReactCoroutine()
    {
        yield return new WaitForSeconds(reactTime);
        playerInMind = true;
    }

    void Sightcast() 
    {
        RaycastHit hit;
        float hitLength = maxSight;

        //Detect if objects are between the player and the golem
        Physics.Raycast(rayCastOrigin.position, angleToPlayer, out hit, hitLength, blockLOS);
        if (hit.collider)
        {
            hitLength = hit.distance;
        }

        //Detect if the player is in range
        Physics.Raycast(rayCastOrigin.position, angleToPlayer, out hit, hitLength, isPlayer);
        if (hit.collider)
        {
            playerInSight = true;
            //print("Player in sight !");
            hitLength = hit.distance;
        } else
        {
            playerInSight = false;
        }

        //Debug.DrawRay(rayCastOrigin.position, angleToPlayer * hitLength, Color.red);

        //Detect if the player is in melee range
        if (hit.distance <= attackRange)
        {
            playerInRange = true;
        } else
        {
            playerInRange = false;
        }
    }

    void React()
    {
        if (playerInRange)
        {
            if (!isInAttack)
            {
                isInBuildup = true;
                attackAngle = angleToPlayer;
                buildupTimeElapsed = attackTimeElapsed = recoveryTimeElapsed = 0;
                Attack();
            }
            isInAttack = true;
        } else if (!isInAttack)
        {
            Move();
        }
    }

    void Move()
    {
        if (!damageManager.isInKnockback)
        {
            targetVelocity = angleToPlayer * maxSpeed;
            currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, targetVelocity.x, ref refVelocityx, accelerationTime);
            currentVelocity.z = Mathf.SmoothDamp(currentVelocity.z, targetVelocity.z, ref refVelocityz, accelerationTime);
            rigidBody.velocity = currentVelocity;
        } else
        {
            currentVelocity = Vector3.zero;
            targetVelocity = Vector3.zero;
            refVelocityx = 0;
            refVelocityz = 0;
        }
    }

    void Attack()
    {
        StartCoroutine("AttackCoroutine");
    }
    public IEnumerator AttackCoroutine()
    {
        Vector3 attackDimensions = new Vector3(attackDimensionsLength / 2, 1, attackDimensionsDepth / 2);
        Vector3 attackDirection = player.transform.position - transform.position;
        yield return new WaitForSeconds(buildupTime);
        isInBuildup = false;
        isAttacking = true;
        Collider[] hitPlayer = Physics.OverlapSphere(transform.position, attackDimensions.z, isPlayer);
        foreach (Collider target in hitPlayer)
        {
            Vector3 playerDirection = player.transform.position - transform.position;

            float playerAngle = Vector3.Angle(attackDirection, playerDirection);
            print(playerAngle);
            if (playerAngle <= attackDimensions.x)
            {
                playerScript.PlayerDamage(attackDamage);
            }
        }
        isInRecover = true;
        yield return new WaitForSeconds(recoveryTime);
        isInRecover = false;
        isInAttack = false;
    }
}
