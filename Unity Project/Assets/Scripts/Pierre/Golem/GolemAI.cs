using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Scripting.APIUpdating;

public class GolemAI : MonoBehaviour
{

    GameObject player;
    Rigidbody rigidbody;
    public Transform rayCastOrigin;
    
    //Line of Sight
    public float maxSight; //How far can the enemy see ?
    public float maxAwaken; //How far away should we start drawing raycasts to detect the player 
    public float maxSpeed;

    public LayerMask blockLOS;
    public LayerMask isPlayer;

    Vector3 enemyToPlayer, angleToPlayer;
    public float distanceToPlayer;

    public bool playerInSight;
    public bool playerInRange;

    //Combat
    public float attackRange;
    public bool isInAttack, isInBuildup, isAttacking, isInRecover;
    public float buildupTime, attackTime, recoveryTime, attackDimensionsLength, attackDimensionsDepth;
    float buildupTimeElapsed, attackTimeElapsed, recoveryTimeElapsed;
    Vector3 attackAngle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
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
        rigidbody.velocity = Vector3.zero;
        if (distanceToPlayer < maxAwaken)
        {
            angleToPlayer = enemyToPlayer;
            angleToPlayer.Normalize();
            Sightcast();
            if (playerInSight)
            {
                React();
            }
        }
        
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
            }
            isInAttack = true;
        } else if (!isInAttack)
        {
            Move();
        }
        if (isInAttack)
        {
            Attack();
        }
    }

    void Move()
    {
        rigidbody.velocity = angleToPlayer * maxSpeed;
    }

    void Attack()
    {
        if (isInBuildup && !isAttacking && !isInRecover)
        {
            buildupTimeElapsed += Time.deltaTime;
            if (buildupTimeElapsed >= buildupTime)
            {
                isInBuildup = false;
                isAttacking = true;
            }
        } else if (isAttacking && !isInRecover)
        {
            Vector3 attackDimensions = new Vector3(attackDimensionsLength / 2, 1, attackDimensionsDepth / 2);
            bool hit = Physics.BoxCast(rayCastOrigin.position + attackAngle, attackDimensions, attackAngle, Quaternion.Euler(attackAngle), attackDimensionsDepth/2, isPlayer);
            if (hit)
            {
                //Do Damage
            }
            attackTimeElapsed += Time.deltaTime;
            if (attackTimeElapsed >= attackTime)
            {
                isAttacking = false;
                isInRecover = true;
            }
        } else if (isInRecover)
        {
            recoveryTimeElapsed += Time.deltaTime;
            if (recoveryTimeElapsed >= recoveryTime)
            {
                isInRecover = false;
                isInAttack = false;
            }
        }


    }
}
