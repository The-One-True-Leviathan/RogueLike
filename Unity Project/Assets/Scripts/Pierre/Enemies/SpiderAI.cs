using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    public enum SpiderState { Dormant, Approach, Pursue, Search, Attack }
    public SpiderState state = SpiderState.Dormant;

    NavMeshAgent navMeshAgent;
    public Animator animator;

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
        lastTimeSincePlayerSeen = 0, //How long since the AI saw the player
        hearingDistance, //How far can the AI hear the player
        hearingFalloff; //How long (in seconds) does it take for the AI to forget the noise
    public bool seeingPlayer, //Is the AI seeing the player
        hearingPlayer, //If the player makes another noise while this is true, the AI will give chase
        targetingPlayer; //if this is true, the AI chases the player

    //movements
    public Vector3 moveTarget;
    public float pursueDistance = 8,
        approachDistance = 12,
        approachSpeed = 15,
        approachAcceleration = 500,
        approachJumpRange = 5,
        approachJumpCooldownMin = 0.8f,
        approachJumpCooldownMax = 1.4f,
        approachAngle = 15,
        pursueSpeed = 5,
        pursueAcceleration = 10;
    public bool isInJump, canJump = true;


    //Attacks
    public Vector3 attackDirection;
    public float attackDistance = 1.5f,
        attackRange = 2,
        attackAngle = 30,
        attackBuildup = 0.1f,
        attackRecover = 0.2f,
        attackCooldown = 0.2f,
        attackDamage = 2f,
        backJumpAngle = 15;
    public bool isInAttack;


    //Search
    public float searchMaxTime = 5,
        searchTime = 0,
        distanceToSearchPoint = 1.5f,
        lookingAroundRadiusMin = 4,
        lookingAroundRadiusMax = 12;
    public bool arrivedToSearchPoint = false,
        lookingAround = false,
        movingToNextSearchPoint = false;
    public Vector3 lastKnownPosition;


    //Animation
    Vector3 lastPosition,
        currentSpeed;

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
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        seeingPlayer = false;
        if (DistanceToPlayer() < seeingDistance || targetingPlayer)
        {
            SightCast();
        }

        if (seeingPlayer)
        {
            lastKnownPosition = playerObject.transform.position;
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
                Search();
                break;
            case SpiderState.Attack:
                Attack();
                break;
        }

        Anim();
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
            timeSeenPlayer = 0;
            targetingPlayer = true;
            state = SpiderState.Approach;
        }
    }

    void Approach()
    {
        if(!isInJump)
        {
            if (distanceToPlayer < pursueDistance || !seeingPlayer)
            {
                state = SpiderState.Pursue;
                return;
            }
            if (!targetingPlayer)
            {
                state = SpiderState.Search;
                return;
            }
        }

        Memory();

        if (canJump)
        {
            canJump = false;
            StartCoroutine("Jump");
        }
        

    }

    IEnumerator Jump()
    {
        isInJump = true;
        navMeshAgent.speed = approachSpeed;
        navMeshAgent.acceleration = approachAcceleration;
        float rng = Random.Range(-approachAngle / 2, approachAngle / 2) + Random.Range(-approachAngle / 2, approachAngle / 2);
        float cooldown = Random.Range(approachJumpCooldownMin, approachJumpCooldownMax);
        moveTarget = Quaternion.AngleAxis(rng, Vector3.up) * toPlayer;
        moveTarget.Normalize();
        moveTarget = moveTarget * approachJumpRange;
        navMeshAgent.SetDestination(transform.position + moveTarget);
        float time = ((navMeshAgent.destination - transform.position).magnitude / approachSpeed)*(approachAcceleration/10);
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        navMeshAgent.SetDestination(transform.position);
        isInJump = false;
        yield return new WaitForSeconds(cooldown - time);
        canJump = true;
    }

    void Pursue()
    {
        if (!isInJump)
        {
            if (distanceToPlayer > pursueDistance && seeingPlayer)
            {
                state = SpiderState.Approach;
                return;
            }
            if (distanceToPlayer < attackDistance)
            {
                state = SpiderState.Attack;
                return;
            }
        }


        Memory();

        moveTarget = playerObject.transform.position;
        navMeshAgent.speed = pursueSpeed;
        navMeshAgent.acceleration = pursueAcceleration;
        navMeshAgent.SetDestination(moveTarget);
    }

    void Attack()
    {
        if (!isInAttack)
        {
            if (distanceToPlayer > attackDistance && !isInJump)
            {
                state = SpiderState.Pursue;
                return;
            }

            if (!isInJump)
            {
                isInAttack = true;
                animator.SetInteger("state", 3);
                attackDirection = toPlayer;
                StartCoroutine("AttackCoroutine");
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        isInAttack = true;
        yield return new WaitForSeconds(attackBuildup);
        DoDamage();
        yield return new WaitForSeconds(attackRecover);
        StartCoroutine("BackJump");
        yield return new WaitForSeconds(attackCooldown);
        isInAttack = false;

    }

    IEnumerator BackJump()
    {

        isInJump = true;
        navMeshAgent.speed = approachSpeed;
        navMeshAgent.acceleration = approachAcceleration;
        float rng = Random.Range(-approachAngle / 2, approachAngle / 2) + Random.Range(-approachAngle / 2, approachAngle / 2);
        float cooldown = Random.Range(approachJumpCooldownMin, approachJumpCooldownMax);
        moveTarget = Quaternion.AngleAxis(rng, Vector3.up) * (transform.position - playerObject.transform.position);
        moveTarget.Normalize();
        moveTarget = moveTarget * approachJumpRange;
        navMeshAgent.SetDestination(transform.position + moveTarget);
        float time = ((navMeshAgent.destination - transform.position).magnitude / approachSpeed) * (approachAcceleration / 10);
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        navMeshAgent.SetDestination(transform.position);
        isInJump = false;
        yield return new WaitForSeconds(cooldown - time);
        canJump = true;
    }

    void DoDamage()
    {
        /* Raph
         Son d'attaque spooder

         */
        Collider[] hitPlayer = Physics.OverlapSphere(transform.position, attackRange, playerMask);
        foreach (Collider player in hitPlayer)
        {
            Vector3 playerDirection = player.transform.position - transform.position;

            float playerAngle = Vector3.Angle(attackDirection, playerDirection);
            print(playerAngle);
            if (playerAngle <= attackAngle)
            {
                if (playerObject.GetComponent<Player>())
                {
                    playerScript.PlayerDamage(attackDamage);
                }
            }
        }


    }

    void Memory()
    {

        if (!seeingPlayer)
        {
            lastTimeSincePlayerSeen += Time.deltaTime;
        }
        else
        {
            lastTimeSincePlayerSeen = 0;
        }

        if (lastTimeSincePlayerSeen > memoryMax)
        {
            searchTime = 0;
            lookingAround = false;
            state = SpiderState.Search;
        }
    }

    void Search()
    {
        if (seeingPlayer)
        {
            targetingPlayer = true;
            movingToNextSearchPoint = false;
            state = SpiderState.Approach;
            return;
        }
        if (!lookingAround)
        {
            arrivedToSearchPoint = navMeshAgent.remainingDistance < distanceToSearchPoint;
            if (!movingToNextSearchPoint)
            {
                MoveToLastKnownPosition();
            }

            if (arrivedToSearchPoint)
            {
                movingToNextSearchPoint = false;
                lookingAround = true;
            }
        }
        if (lookingAround)
        {
            if (searchTime >= searchMaxTime)
            {
                state = SpiderState.Dormant;
                return;
            }
            else
            {
                searchTime += Time.deltaTime;
            }
            if (!movingToNextSearchPoint)
            {
                StartCoroutine("LookAround");
            }
        }

    }

    void MoveToLastKnownPosition()
    {
        movingToNextSearchPoint = true;
        moveTarget = lastKnownPosition;
        navMeshAgent.speed = pursueSpeed;
        navMeshAgent.acceleration = pursueAcceleration;
        navMeshAgent.SetDestination(moveTarget);
    }

    IEnumerator LookAround()
    {
        movingToNextSearchPoint = true;
        float rngx = Random.Range(-1, 1);
        float rngz = Random.Range(-1, 1);
        Vector3 direction = new Vector3(rngx, 0, rngz);
        direction.Normalize();
        direction = direction * Random.Range(lookingAroundRadiusMin, lookingAroundRadiusMax);
        moveTarget = lastKnownPosition + direction;
        navMeshAgent.speed = pursueSpeed;
        navMeshAgent.acceleration = pursueAcceleration;
        navMeshAgent.SetDestination(moveTarget);
        float movetime = (navMeshAgent.remainingDistance / navMeshAgent.speed)+0.2f;
        yield return new WaitForSeconds(movetime);
        lastKnownPosition = transform.position;
        movingToNextSearchPoint = false;
    }

    void Anim()
    {
        currentSpeed = transform.position - lastPosition;
        currentSpeed = currentSpeed / Time.deltaTime;
        Vector3 dir = new Vector3();
        switch (isInAttack)
        {
            case false:
                dir = currentSpeed.normalized;
                break;
            case true:
                dir = attackDirection;
                break;
        }
        lastPosition = transform.position;

        if (dir.x > 0.5 || dir.x < -0.5)
        {
            animator.SetBool("horizontal", true);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }
        if (dir.z > 0.5)
        {
            animator.SetBool("horizontal", false);
            animator.SetBool("up", true);
            animator.SetBool("down", false);
        }
        if (dir.z < -0.5)
        {
            animator.SetBool("horizontal", false);
            animator.SetBool("up", false);
            animator.SetBool("down", true);
        }
        if (dir.x < -0.5)
        {
            animator.GetComponentInParent<Transform>().localScale = new Vector3(-1, 1, 1);

        }
        else
        {
            animator.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (isInJump)
        {
            animator.SetInteger("state", 2);
            return;
        }

        if (!isInAttack && !isInJump)
        {
            if(currentSpeed.magnitude == 0)
            {
                animator.SetInteger("state", 0);
            } else
            {
                animator.SetInteger("state", 1);
            }
        }

    }

}
