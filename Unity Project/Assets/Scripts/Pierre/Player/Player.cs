using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

public class Player : MonoBehaviour
{
    //Appels de composants nécessaires
    public LayerMask layerEnemies, layerEnchants;
    Rigidbody rigidBody;
    public EnchantmentManager enchant;
    public HealthBar healthBar;
    public GameObject weaponDropOriginal;
    public Animator animator;
    public Animator weaponAnimator;
    public Screenshake screenshake;
    [SerializeField] Rewind rewind;


    public PlayerCollisionDetector left, right, top, bottom;
    public bool collisionLeft, collisionRight, collisionTop, collisionBottom;


    public float damageImmunity; //Longeur (en secondes) de l'immunité après avoir prit des dégâts

    public Controler controller; //Appel du controlleur
    public bool south, secondaryAtk, north, mainAtk;
    public Vector3 mousePosition, rStick, lStick, lastDirection, normalizedLStick;


    public Vector3 currentSpeed, targetSpeed;
    float xVelocity, zVelocity;
    public float maxSpeed = 10f, accelerationTime = 0.3f;
    public bool isSlowed;


    public Collider[] allInteractibleInRange;
    public float interactRange, interactAngle;
    public LayerMask interactible;



    public bool isInRoll;
    public float rollLength, rollRecover, rollSpeed;
    public Vector3 rollDirection;


    //potions
    public bool strenght, speed;
    public float strenghtMultiplier, speedMultiplier;

    //combat
    public bool dualWielding; //Is the character wielding two different weapons ?
    public float switchTime;
    public WeaponScriptableObject weapon1, weapon2, weaponInAtk, weaponInHitSpan, switchSpace, droppedWeapon = null; //Weapon 1 and 2 are the two "hands" of the player, weaponInHitSpan is used for multi-frame attacks, and switchSpace is only used 
                                                                                                                     //when switching weapons in both hands
    public float hitSlowFactor, hitSlowLength;
    public AttackProfileScriptableObject profileInUse;
    public bool isInBuildup, isInCharge, isInAttack, isInRecover, isInCooldown, isInHitSpan, isInImmunity, hasShot, isInHeavyAtk, isInRecoil;
    public float hitSpanDamage;
    public int hitSpanAtkNumber, chargeLevel;
    public bool isInSlowMo;
    public Vector3 attackDirection;
    public List<GameObject> enemiesHitLastAttack, enemiesHitLastAttackRanged;
    public GameObject closestEnemyHitLastAttack, latestEnemyKilled;
    public float clostestEnemyDistance;

    //Sound Design du Joueur
    public AudioSource playerSource, weaponSource, stepsSource;
    public AudioClip heal, dégâtsReçus, criDeMort, pasDuJoueur, chargementAttaque, roulade;
    public AudioClip[] attaques = new AudioClip[4];

    // Start is called before the first frame update
    void Start()
    {
        screenshake = GetComponent<Screenshake>();
        weapon1 = Object.Instantiate(weapon1) as WeaponScriptableObject;
        weapon2 = Object.Instantiate(weapon2) as WeaponScriptableObject;
        healthBar = GameObject.FindGameObjectWithTag("HUD").GetComponent<HealthBar>();
        rewind = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Rewind>();
        weapon1.InitializeWeapon();
        if (weapon2 != null)
        {
            weapon2.InitializeWeapon();
        }
        enchant = GetComponent<EnchantmentManager>();
        rigidBody = GetComponent<Rigidbody>();
        controller = new Controler();
        controller.Enable();
        lastDirection = Vector3.forward;
        controller.Keyboard.Attack1.started += ctx => mainAtk = true;
        controller.Keyboard.Attack1.canceled += ctx => mainAtk = false;


        controller.Keyboard.Attack2.started += ctx => secondaryAtk = true;
        controller.Keyboard.Attack2.canceled += ctx => secondaryAtk = false;

        controller.Keyboard.Switch.started += ctx => north = true;
        controller.Keyboard.Switch.canceled += ctx => north = false;

        controller.Keyboard.Roll.started += ctx => south = true;
        controller.Keyboard.Roll.canceled += ctx => south = false;

    }


    // Update is called once per frame
    void Update()
    {
        if (healthBar.isDead)
        {
            animator.SetBool("death", true);
            this.enabled = false;
        }
        enchant.DoEnchants(weapon1, 0);
        if (dualWielding)
        {
        enchant.DoEnchants(weapon2, 0);
        }
        Inputs();
        if (south && !isInRoll && !isInRecover)
        {
            Roll();
        }
        if (dualWielding)
        {
            if(controller.Keyboard.Drop.triggered)
            {
                DropWeapon();
            }
        }

        if (!isInAttack && !isInCooldown && !isInCharge)
        {
            if (mainAtk)
            {
                Attack(weapon1, 0);
            }

            if (secondaryAtk)
            {
                if (dualWielding)
                {
                    Attack(weapon1, 1);
                } else
                {
                    Attack(weapon1, 1);
                }
            }
        }

        if (dualWielding && north && !isInCooldown)
        {
            Switch();
        }

        InteractSphere();
        if (controller.Keyboard.Interact.triggered)
        {
            InteractAction();
        }

        Move();


        if (isInHitSpan)
        {
            HitSpan(weaponInHitSpan, hitSpanDamage, hitSpanAtkNumber);
        }

        Anim();
    }

    void Switch()
    {
        switchSpace = weapon1;
        weapon1 = weapon2;
        weapon2 = switchSpace;
        StartCoroutine("SwitchTime");
    }

    IEnumerator SwitchTime()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(switchTime);
        isInCooldown = false;
    }

    public void ChangeWeapon(WeaponScriptableObject newWeapon)
    {
        if (!dualWielding)
        {
            dualWielding = true;
            weapon2 = newWeapon;
        }
        else
        {
            DropWeapon();
            //droppedWeapon = null;
            weapon2 = newWeapon;
            dualWielding = true;
        }
        weapon1.InitializeWeapon();
        if (weapon2 != null)
        {
            weapon2.InitializeWeapon();
        }
    }

    public void DropWeapon()
    {
        droppedWeapon = weapon2;
        GameObject dropped = Instantiate(weaponDropOriginal, transform.position + attackDirection / 2f, transform.rotation);
        dropped.GetComponent<WeaponItemBehavior>().weapon = droppedWeapon;
        dropped.GetComponent<WeaponItemBehavior>().Dropped();
        dualWielding = false;
    }

    public void Roll()
    {
        isInRoll = true;
        isInRecover = true;
        animator.SetBool("roll", true);
        StartCoroutine("RollCoroutine");
    }

    public IEnumerator RollCoroutine()
    {
        targetSpeed = rollDirection * rollSpeed;
        GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale = new Vector3(1,0.5f,1);
        yield return new WaitForSeconds(rollLength);
        GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale = new Vector3(1, 1f, 1);
        isInRoll = false;
        animator.SetBool("roll", false);
        yield return new WaitForSeconds(rollRecover);
        isInRecover = false;
    }

    public void Heal(float amount)
    {
        healthBar.ApplyDamage(-amount);
        //RPP
        playerSource.clip = heal;
        playerSource.Play();
    }

    public void IncreaseMaxHealth(float amount)
    {
        healthBar.UpgradeLife(amount);
        //RPP
        playerSource.clip = heal;
        playerSource.Play();
    }

    public void PlayerDamage(float amount)
    {
        PlayerDamage(amount, transform.position, 0, 0);
    }
    public void PlayerDamage(float amount, Vector3 origin, float kbstrength, float kblength)
    {

        if (!isInImmunity && !isInRoll)
        {
            screenshake.Shake(0.05f, 0.1f*amount, 0.01f);
            healthBar.ApplyDamage(amount);
            //Debug.LogError("Damaged for " + amount);

            //RPP    
            rewind.PlayerIsDamaged();
            if (healthBar.vieTemp <= 0)
            {
                playerSource.clip = criDeMort;
                playerSource.Play();
            }
            else
            {
                playerSource.clip = dégâtsReçus;
                playerSource.Play();
            }

            enchant.DoEnchants(weapon1, 3);
            if (dualWielding) { enchant.DoEnchants(weapon2, 3); }
            Immunity(damageImmunity);
            rewind.PlayerIsDamaged();
        }
        StartCoroutine(Knockback(origin, kbstrength, kblength));
    }

    private IEnumerator Knockback(Vector3 origin, float kbstrength, float kblength)
    {
        if (kblength != 0)
        {
            isInRecoil = true;
        }
        currentSpeed = (origin - transform.position).normalized * kbstrength;
        currentSpeed.y = 0;
        yield return new WaitForSeconds(kblength);
        if (kblength != 0)
        {
            isInRecoil = false;
        }
    }

    public void Immunity(float duration)
    {
        if (!isInImmunity)
        {
            StartCoroutine("ImmunityCoroutine", duration);
        }
    }

    public IEnumerator ImmunityCoroutine(float duration)
    {
        isInImmunity = true;
        yield return new WaitForSeconds(duration);
        isInImmunity = false;
    }

    public void Inputs()
    {


        if (Gamepad.current != null)
        {
            rStick = new Vector3(controller.Keyboard.LookAround.ReadValue<Vector2>().x, 0, controller.Keyboard.LookAround.ReadValue<Vector2>().y);
        }
        else
        {
            rStick = mousePosition - transform.position;
            rStick.y = 0;
        }
        lStick = new Vector3(controller.Keyboard.Movement.ReadValue<Vector2>().x, 0, controller.Keyboard.Movement.ReadValue<Vector2>().y);
        
        rStick.Normalize();
        normalizedLStick = lStick.normalized;


        if (!isInHeavyAtk)
        {
            if (normalizedLStick != Vector3.zero)
            {
                lastDirection = normalizedLStick;
                animator.SetBool("moving", true);
            }
            else
            {
                animator.SetBool("moving", false);
            }

            attackDirection = lastDirection;
            if (!(rStick == Vector3.zero))
            {
                attackDirection = rStick;
            }
            Debug.LogWarning("attackDirection = "+attackDirection);
        }
        Debug.DrawRay(transform.position, attackDirection, Color.red);

        if (!isInRoll)
        {

            rollDirection = lastDirection;
            targetSpeed = Vector3.ClampMagnitude(lStick, 1) * maxSpeed;
        }
        Debug.DrawRay(transform.position, targetSpeed, Color.blue);
    }

    public void Anim()
    {
        if (lastDirection.x > 0.5 || lastDirection.x < -0.5)
        {
            animator.SetBool("horizontal", true);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }
        if (lastDirection.z > 0.5)
        {
            animator.SetBool("horizontal", false);
            animator.SetBool("up", true);
            animator.SetBool("down", false);
        }
        if (lastDirection.z < -0.5)
        {
            animator.SetBool("horizontal", false);
            animator.SetBool("up", false);
            animator.SetBool("down", true);
        }
        if (lastDirection.x < -0.5)
        {
            animator.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            animator.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void Move()
    {
        collisionLeft = left.isCollision;
        collisionRight = right.isCollision;
        collisionTop = top.isCollision;
        collisionBottom = bottom.isCollision;

        if (!isInRecoil)
        {
            currentSpeed.x = Mathf.SmoothDamp(currentSpeed.x, targetSpeed.x, ref xVelocity, accelerationTime);
            currentSpeed.z = Mathf.SmoothDamp(currentSpeed.z, targetSpeed.z, ref zVelocity, accelerationTime);
            if (isInHeavyAtk)
            {
                currentSpeed *= 0.5f;
                targetSpeed *= 0.5f;
                xVelocity *= 0.5f;
                zVelocity *= 0.5f;
            }
            if (currentSpeed != Vector3.zero)
            {
                //RPP
                stepsSource.clip = pasDuJoueur;
                stepsSource.Play();
            }
        }

        if (collisionLeft && currentSpeed.x < 0)
        {
            currentSpeed.x = 0;
        }
        if (collisionRight && currentSpeed.x > 0)
        {
            currentSpeed.x = 0;
        }
        if (collisionTop && currentSpeed.z > 0)
        {
            currentSpeed.z = 0;
        }
        if (collisionBottom && currentSpeed.z < 0)
        {
            currentSpeed.z = 0;
        }

        rigidBody.velocity = currentSpeed;
        if (isInCharge)
        {
            rigidBody.velocity = currentSpeed / 3;
        }
        if (speed)
        {
            rigidBody.velocity *= speedMultiplier;
        }
    }

    public void Attack(WeaponScriptableObject weapon, int atkNumber)
    {
        // (!weapon.atk1Charge)
        //{
        enemiesHitLastAttack.Clear();
        enemiesHitLastAttackRanged.Clear();
        clostestEnemyDistance = Mathf.Infinity;
        weaponInAtk = weapon;
        isInAttack = true;
        hasShot = false;
        hitSpanAtkNumber = atkNumber;
        StartCoroutine("ResolveAttack", atkNumber);
        //{
        //StartCoroutine("ChargeAttack1", weapon);
        //}
    }

    IEnumerator ResolveAttack(int atkNumber)
    {
        WeaponScriptableObject weapon = weaponInAtk;
        print("start attack");
        isInBuildup = true;
        chargeLevel = 0;
        if (weapon.atk[atkNumber].isHeavy[0])
        {
            isInHeavyAtk = true;
        }
        if (!weapon.atk[atkNumber].isCharge)
        {
            weaponAnimator.SetInteger("Index", weapon.atk[atkNumber].animationIndex[0]);
            yield return new WaitForSeconds(weapon.atk[atkNumber].buildup * weapon.totalBuildupMultiplier);
        } else
        {
            //RPP
            playerSource.clip = chargementAttaque;
            playerSource.Play();

            isInCharge = true;
            weaponAnimator.SetInteger("Index", weapon.atk[atkNumber].animationIndex[0]);
            yield return new WaitForSeconds(weapon.atk[atkNumber].chargeTime[0] * weapon.totalBuildupMultiplier);
            print("attack charge 0");
            if (!mainAtk && !secondaryAtk)
            {
                chargeLevel = 0;
                weaponAnimator.SetInteger("Index", weapon.atk[atkNumber].animationIndex[chargeLevel+1]);
                yield return new WaitForSeconds((weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]) * weapon.totalBuildupMultiplier);
            }
            else
            {
                if (weapon.atk[atkNumber].isHeavy[1])
                {
                    isInHeavyAtk = true;
                }
                yield return new WaitForSeconds((weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]) * weapon.totalBuildupMultiplier);
                print("attack charge 1");
                if (!mainAtk && !secondaryAtk)
                {
                    chargeLevel = 1;
                    weaponAnimator.SetInteger("Index", weapon.atk[atkNumber].animationIndex[chargeLevel+1]);
                }
                else
                {
                    if (weapon.atk[atkNumber].isHeavy[2])
                    {
                        isInHeavyAtk = true;
                    }
                    yield return new WaitForSeconds((weapon.atk[atkNumber].chargeTime[2] - weapon.atk[atkNumber].chargeTime[1] - weapon.atk[atkNumber].chargeTime[0]) * weapon.totalBuildupMultiplier);
                    chargeLevel = 2;
                    weaponAnimator.SetInteger("Index", weapon.atk[atkNumber].animationIndex[chargeLevel+1]);
                    print("attack charge 2");
                }
            }
        }
        //RPP
        playerSource.clip = attaques[Random.Range(0, 3)];
        playerSource.Play();
        weaponSource.clip = weapon.audioClip;
        weaponSource.Play();

        print("attack");
        isInHitSpan = true;
        enchant.DoEnchants(weapon, 4);
        weaponInHitSpan = weapon;
        hitSpanDamage = (weapon.atk[atkNumber].damage[chargeLevel]) * weapon.totalDamageMultiplier;
        if(strenght)
        {
            hitSpanDamage *= strenghtMultiplier;
        }
        if (normalizedLStick == Vector3.zero)
        {
                StartCoroutine("RecoilCoroutine", weapon.atk[atkNumber].recoil[chargeLevel] * weapon.totalRecoilMultiplier);
        }
        else if (Vector3.Angle(attackDirection, normalizedLStick) <= 60)
        {
            if (weapon.atk[atkNumber].recoil[chargeLevel].x * weapon.totalRecoilMultiplier.x <= 0)
            {
                StartCoroutine("RecoilCoroutine", weapon.atk[atkNumber].recoil[chargeLevel] * weapon.totalRecoilMultiplier);
            }
        }
        isInBuildup = false;
        isInRecover = true;
        isInCooldown = true;
        yield return new WaitForSeconds((weapon.atk[atkNumber].hitSpan[chargeLevel]) * weapon.totalBuildupMultiplier);
        isInHitSpan = false;
        isInHeavyAtk = false;
        isInCharge = false;
        yield return new WaitForSeconds((weapon.atk[atkNumber].recover[chargeLevel] - weapon.atk[atkNumber].hitSpan[chargeLevel]) * weapon.totalBuildupMultiplier);

        weaponAnimator.SetInteger("Index", 0); print("recover");
        isInRecover = false;
        isInAttack = false;
        yield return new WaitForSeconds((weapon.atk[atkNumber].cooldown[chargeLevel] - weapon.atk[atkNumber].recover[chargeLevel]) * weapon.totalBuildupMultiplier);
        print("cooldown");
        isInCooldown = false;
    }

    public void HitSpan(WeaponScriptableObject weapon, float damage, int atkNumber)
    {
        if (weapon.atk[atkNumber].reach[chargeLevel] != Vector3.zero)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, weapon.atk[atkNumber].reach[chargeLevel].z * weapon.totalReachMultiplier.z, layerEnemies);
            foreach (Collider enemy in hitEnemies)
            {
                print("hitspan");
                if (!enemiesHitLastAttack.Contains(enemy.gameObject))
                {
                    Vector3 enemyDirection = enemy.transform.position - transform.position;

                    float enemyAngle = Vector3.Angle(attackDirection, enemyDirection);
                    float a = enemyDirection.magnitude;
                    float b = enemyDirection.magnitude;
                    float c = enemy.bounds.extents.x;
                    float additionalAngle = Mathf.Rad2Deg*Mathf.Acos(((a * a) + (b*b) - (c * c)) / (2 * (a * b)));
                    if (enemyAngle <= weapon.atk[atkNumber].reach[chargeLevel].x * weapon.totalReachMultiplier.x + additionalAngle)
                    {
                        if (enemyDirection.magnitude < clostestEnemyDistance)
                        {
                            clostestEnemyDistance = enemyDirection.magnitude;
                            closestEnemyHitLastAttack = enemy.gameObject;
                        }
                        Debug.DrawRay(transform.position, enemyDirection, Color.red);
                        //Debug.LogError("Enemy hit ! Inflicted " + damage + " damage !");
                        float finalKnockback = weapon.atk[atkNumber].knockBack[chargeLevel] * weapon.totalKnockbackMultiplier;
                        DoAttack(damage, finalKnockback, enemy.gameObject);
                        screenshake.Shake(0.05f, 0.01f*damage, 0.01f);
                        enemiesHitLastAttack.Add(enemy.gameObject);
                    }
                }


            }
            Collider[] hitEnchants = Physics.OverlapSphere(transform.position, weapon.atk[atkNumber].reach[chargeLevel].z * weapon.totalReachMultiplier.z, layerEnchants);
            foreach (Collider hitEnchant in hitEnchants)
            {
                Vector3 enemyDirection = hitEnchant.transform.position - transform.position;

                float enemyAngle = Vector3.Angle(attackDirection, enemyDirection);
                float a = enemyDirection.magnitude;
                float b = enemyDirection.magnitude;
                float c = hitEnchant.bounds.extents.x;
                float additionalAngle = Mathf.Rad2Deg * Mathf.Acos(((a * a) + (b * b) - (c * c)) / (2 * (a * b)));
                if (enemyAngle <= weapon.atk[atkNumber].reach[chargeLevel].x * weapon.totalReachMultiplier.x + additionalAngle)
                {
                    Debug.LogWarning("Hit enchant");
                    hitEnchant.GetComponent<EnchantItemBehavior>().Attacked();
                }



            }
            AttackEnchant(weapon);
            //Debug.LogError("Number of Enemies hit : " + enemiesHitLastAttack.Count);
        }

        if (weapon.atk[atkNumber].isRanged)
        {
            if (!hasShot)
            {
                Instantiate(weapon.atk[atkNumber].projectile[chargeLevel], transform.position, Quaternion.LookRotation(attackDirection, Vector3.up));
                hasShot = true;
            }
        }
    }
    public IEnumerator RecoilCoroutine(Vector2 recoil)
    {
        isInRecoil = true;
        currentSpeed = attackDirection * -recoil.x;
        yield return new WaitForSeconds(recoil.y);
        isInRecoil = false;
    }

    public void KillEnchant ()
    {
        enchant.DoEnchants(weapon1, 2);
        if (dualWielding)
        {
            enchant.DoEnchants(weapon2, 2);
        }
    }

    public void DoAttack(float damage, float knockback, GameObject enemy)
    {
        EnemyDamage enemyDamage = enemy.GetComponent<EnemyDamage>();
        enemyDamage.Damage(damage, knockback, transform);
        if (!isInSlowMo)
        {
            StartCoroutine("SlowMo");
        }
    }

    public void AttackEnchant(WeaponScriptableObject weapon)
    {
        for (int i = 0; i < enemiesHitLastAttack.Count; i++) 
        {
            enchant.DoEnchants(weapon, 1);
        }
    }
    
    public void InteractSphere()
    {
        allInteractibleInRange = Physics.OverlapSphere(transform.position, interactRange, interactible);
        foreach (Collider interactible in allInteractibleInRange)
        {
            interactible.GetComponent<InteractibleBehavior>().interactible = true;
        }
    }

    public void InteractAction()
    {
        if (!allInteractibleInRange.Count().Equals(0))
        {
            if (allInteractibleInRange.Count().Equals(1))
            {
                allInteractibleInRange[0].GetComponent<InteractibleBehavior>().interacted = true;
            } 
            else
            {
                float smallestAngle = Mathf.Infinity;
                Collider interactionTarget = allInteractibleInRange[0];
                foreach (Collider interactible in allInteractibleInRange)
                {
                    Vector3 playerToInteractible = interactible.transform.position - transform.position;
                    float interactibleAngle = Vector3.Angle(attackDirection, playerToInteractible);
                    if (interactibleAngle < smallestAngle)
                    {
                        interactionTarget = interactible;
                        smallestAngle = interactibleAngle;
                    }
                }
                interactionTarget.GetComponent<InteractibleBehavior>().interacted = true;
            }
        }
    }


    public IEnumerator SlowMo()
    {
        isInSlowMo = true;
        float preSlowMo = Time.timeScale;
        float trueTime = (hitSlowLength * hitSlowFactor)/1;
        Time.timeScale = Time.timeScale * hitSlowFactor;
        yield return new WaitForSeconds(trueTime);
        Time.timeScale = preSlowMo;
        isInSlowMo = false;
    }
}
