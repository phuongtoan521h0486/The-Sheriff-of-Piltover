using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    private bool died = false;

    [Header("State0")]
    private int RUNNING = 1;
    private int ATTACKING = 2;
    private int DIE = 3;

    [Header("Zombie Health and Damage")]
    private float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;
    public HealthBar healthBar;

    [Header("Zombie things")]
    public NavMeshAgent zombieAgent;
    public Transform lookPoint;
    public Camera AttackingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Zomebie Guarding Var")]
    public GameObject[] walkPoints;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingpointRadius = 2;

    [Header("Zombie Attacking var")]
    public float timeBtwAttack;
    public bool previouslyAttack;

    [Header("Zombie Animation")]
    public Animator anim;
    public float timeAttack = 1.75f;

    [Header("Zombie mood/states")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInvisionRadius;
    public bool playerInattackingRadius;

    [Header("Items")]
    public GameObject coinPrefab;

    private void Awake()
    {
        presentHealth = zombieHealth;
        healthBar.GiveFullHealth(zombieHealth);
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (!playerInvisionRadius && !playerInattackingRadius) Guard();
        if (playerInvisionRadius && !playerInattackingRadius) Pursueplayer();
        if (playerInvisionRadius && playerInattackingRadius) AttackPlayer();
    }

    private void changeState(int state)
    {
        if(state == RUNNING)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", false);
        }
        else if(state == ATTACKING)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", true);
            anim.SetBool("Died", false);
        }
        else if(state == DIE)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", true);
        }
    }

    private void Guard()
    {
        if (Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position)  < walkingpointRadius)
        {
            currentZombiePosition = Random.Range(0, walkPoints.Length);
            if(currentZombiePosition >= walkPoints.Length) 
            {
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);
        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
    }
    private void Pursueplayer()
    {
        if(zombieAgent.SetDestination(playerBody.position))
        {
            changeState(RUNNING);
        }
        else
        {
            changeState(DIE);
        }
    }

    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(lookPoint);
        if(!previouslyAttack) 
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(AttackingRaycastArea.transform.position, AttackingRaycastArea.transform.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("Attacking" +  hitInfo.transform.name);

                changeState(ATTACKING);

                PlayerScript playerBody = hitInfo.transform.GetComponent<PlayerScript>();
                if (playerBody != null)
                {
                    StartCoroutine(hitDamePlayer(playerBody));
                }
            }

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }

    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }

    IEnumerator hitDamePlayer(PlayerScript playerBody)
    {
        yield return new WaitForSeconds(timeAttack);
        playerBody.playerHitDamage(giveDamage);
        AudioController.occurrence.playZombieAttack();

    }

    public void zombieHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);
        if (presentHealth <= 0)
        {
            changeState(DIE);

            ZombieDie();
        }
    }

    private void ZombieDie()
    {
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInattackingRadius = false;
        playerInvisionRadius = false;

        if(died == false)
        {
            Vector3 position = transform.position;
            position.y = position.y + 1f;
            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
            Instantiate(coinPrefab, position, rotation);
            ObjectivesComplete.occurrence.GetObjectivesDone("task2");
            died = true;
        }

        Object.Destroy(gameObject, 5.0f);
    }
}
