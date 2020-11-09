﻿using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    public enum SpiderState { Dormant, Approach, Pursue, Search, Attack }
    public SpiderState state = SpiderState.Dormant;

    NavMeshAgent navMeshAgent;

    //sensing the player
    public GameObject playerObject;
    Player playerScript;
    public LayerMask blocksLOS, playerMask;
    public Vector3 toPlayer;
    public float distanceToPlayer,
        seeingDistance = 12, //How far can the AI see the player
        seeingReactTime = 2, //How long does it take for the AI to start targeting the player
        timeSeenPlayer,
        seeingFalloff = 1, //Multiplier to how fast does timeSeenPlayer falls off
        memoryMax = 5, //How long does the AI memorize the player
        hearingDistance, //How far can the AI hear the player
        hearingFalloff; //How long (in seconds) does it take for the AI to forget the noise
    public bool seeingPlayer, //Is the AI seeing the player
        hearingPlayer, //If the player makes another noise while this is true, the AI will give chase
        targetingPlayer; //if this is true, the AI chases the player

    //movements
    public Vector3 moveTarget;
    public float pursueDistance = 8, 
        approachDistance = 12,
        approachSpeed = 10,
        approachAcceleration = 10,
        approachJumpRange = 5,
        approachJumpCooldownMin = 0.8f,
        approachJumpCooldownMax = 1.4f,
        approachAngle = 15,
        pursueSpeed = 5,
        pursueAcceleration = 10;
    public bool isInJump;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject.GetComponent<Player>())
        {
            playerScript = playerObject.GetComponent<Player>();
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        seeingPlayer = false;
        if (DistanceToPlayer() < seeingDistance)
        {
            SightCast();
        }

        switch (state) 
        {
            case SpiderState.Dormant:
                Dormant();
                break;
            case SpiderState.Approach:
                Approach();
                break;
            case SpiderState.Pursue:
                Pursue();
                break;
            case SpiderState.Search:
                break;
            case SpiderState.Attack:
                break;
        }
    }

    float DistanceToPlayer()
    {
        toPlayer = playerObject.transform.position - transform.position;
        distanceToPlayer = toPlayer.magnitude;
        return (distanceToPlayer);
    }

    bool SightCast()
    {
        float sightLength = seeingDistance;
        Ray sightRay = new Ray(transform.position, toPlayer);
        RaycastHit sightInfo;
        if (Physics.Raycast(sightRay, out sightInfo, sightLength, blocksLOS))
        {
            sightLength = sightInfo.distance;
            seeingPlayer = false;
        }

        if (Physics.Raycast(sightRay, out sightInfo, sightLength, playerMask))
        {
            sightLength = sightInfo.distance;
            seeingPlayer = true;
        }
        return (seeingPlayer);
    }

    void Dormant()
    {
        navMeshAgent.SetDestination(transform.position);
        if (seeingPlayer == true)
        {
            timeSeenPlayer += Time.deltaTime;
        } else
        {
            timeSeenPlayer -= Time.deltaTime * seeingFalloff;
        }

        if (timeSeenPlayer < 0)
        {
            timeSeenPlayer = 0;
        }

        if (timeSeenPlayer > seeingReactTime)
        {
            timeSeenPlayer = seeingReactTime;
        }

        if (timeSeenPlayer >= seeingReactTime)
        {
            targetingPlayer = true;
            state = SpiderState.Approach;
        }
    }

    void Approach()
    {
        if (distanceToPlayer < pursueDistance)
        {
            state = SpiderState.Pursue;
            return;
        }
        if (!targetingPlayer)
        {
            state = SpiderState.Search;
            return;
        }

        if (!isInJump)
        {
            StartCoroutine("Jump");
        }
        

    }

    IEnumerator Jump()
    {
        isInJump = true;
        navMeshAgent.speed = approachSpeed;
        navMeshAgent.acceleration = approachAcceleration;
        float rng = Random.Range(-approachAngle/2, approachAngle/2) + Random.Range(-approachAngle/2, approachAngle/2);
        float cooldown = Random.Range(approachJumpCooldownMin, approachJumpCooldownMax);
        moveTarget = Quaternion.AngleAxis(rng, Vector3.up) * toPlayer;
        moveTarget.Normalize();
        moveTarget = moveTarget * approachJumpRange;
        navMeshAgent.SetDestination(transform.position + moveTarget);
        yield return new WaitForSeconds(cooldown);
        navMeshAgent.SetDestination(transform.position);
        isInJump = false;
    }

    void Pursue()
    {
        if (distanceToPlayer > pursueDistance)
        {
            state = SpiderState.Approach;
            return;
        }
        moveTarget = playerObject.transform.position;
        navMeshAgent.speed = pursueSpeed;
        navMeshAgent.acceleration = pursueAcceleration;
        navMeshAgent.SetDestination(moveTarget);

    }
}
