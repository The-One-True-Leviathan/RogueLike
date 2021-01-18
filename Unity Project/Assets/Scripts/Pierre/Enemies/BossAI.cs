using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public enum BossState { Dormant, Pursue, Search, Attack, Spawn }
    public BossState state = BossState.Dormant;

    NavMeshAgent navMeshAgent;
    public Animator animator;

    //sensing the player
    public GameObject playerObject;
    Player playerScript;
    public LayerMask blocksLOS, playerMask;
    public Vector3 toPlayer;
    public float distanceToPlayer,
        seeingDistance = 25, //How far can the AI see the player
        seeingReactTime = 1, //How long does it take for the AI to start targeting the player
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
    public float pursueSpeed = 3,
        pursueAcceleration = 10;
    public bool isInJump, canJump = true;


    //Attacks
    public Vector3 attackDirection;
    public float attackDistance = 1.5f,
        attackRange = 3,
        attackAngle = 80,
        attackBuildup = 0.2f,
        attackRecover = 0.2f,
        attackCooldown = 0.2f,
        attackDamage = 8f;
    public bool isInAttack, isInCooldown;


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

    //Spawn
    [SerializeField]
    GameObject spiderOriginal;
    [SerializeField]
    float spawnBuildup,
        spawnRecover,
        spawnCooldown;
    public bool isInSpawn,
        isInSpawnCooldown;

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
        navMeshAgent.ResetPath();
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
            case BossState.Dormant:
                Dormant();
                break;
            case BossState.Pursue:
                Pursue();
                break;
            case BossState.Search:
                Search();
                break;
            case BossState.Attack:
                Attack();
                break;
            case BossState.Spawn:
                Spawn();
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
        }
        else
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
            state = BossState.Pursue;
        }
    }

    void Pursue()
    {
        if (!isInJump)
        {
            if (!isInSpawnCooldown)
            {
                state = BossState.Spawn;
                return;
            }
            if (distanceToPlayer < attackDistance && !isInCooldown)
            {
                state = BossState.Attack;
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
            if (!isInSpawnCooldown)
            {
                state = BossState.Spawn;
                return;
            }
            if (distanceToPlayer > attackDistance && !isInJump)
            {
                state = BossState.Pursue;
                return;
            }

            if (!isInJump && !isInCooldown)
            {
                isInAttack = true;
                animator.SetInteger("state", 3);
                attackDirection = toPlayer.normalized;
                StartCoroutine("AttackCoroutine");
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        isInAttack = true;
        isInCooldown = true;
        yield return new WaitForSeconds(attackBuildup);
        DoDamage();
        yield return new WaitForSeconds(attackRecover);
        isInAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        isInCooldown = false;

    }

    void DoDamage()
    {
        /* Raph
         Son d'attaque boss

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
            state = BossState.Search;
        }
    }

    void Search()
    {
        if (seeingPlayer)
        {
            targetingPlayer = true;
            movingToNextSearchPoint = false;
            state = BossState.Pursue;
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
                state = BossState.Dormant;
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
        float movetime = (navMeshAgent.remainingDistance / navMeshAgent.speed) + 0.2f;
        yield return new WaitForSeconds(movetime);
        lastKnownPosition = transform.position;
        movingToNextSearchPoint = false;
    }

    void Spawn()
    {
        if (isInSpawnCooldown)
        {
            state = BossState.Pursue;
            return;
        }
        if (!isInSpawn)
        {
            StartCoroutine(SpawnCoroutine());
        }
    }

    IEnumerator SpawnCoroutine()
    {
        isInSpawn = true;
        yield return new WaitForSeconds(spawnBuildup);
        Instantiate(spiderOriginal, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnRecover);
        isInSpawn = false;
        isInSpawnCooldown = true;
        float ran = Random.Range(1f, 2f);
        ran += Random.Range(1f, 2f);
        ran = ran / 2;
        yield return new WaitForSeconds(spawnCooldown*ran);
        isInSpawnCooldown = false;
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
            animator.GetComponentInParent<Transform>().localScale = new Vector3(1, 1, 1);
        }

        if (isInJump)
        {
            animator.SetInteger("state", 2);
            return;
        }

        if (!isInAttack && !isInJump)
        {
            if (currentSpeed.magnitude == 0)
            {
                animator.SetInteger("state", 0);
            }
            else
            {
                animator.SetInteger("state", 1);
            }
        }

        if (isInSpawn)
        {
            animator.SetInteger("state", 10);
        }

    }

}
