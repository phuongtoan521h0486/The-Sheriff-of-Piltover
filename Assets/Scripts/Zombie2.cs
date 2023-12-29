using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie2 : MonoBehaviour
{
    private bool died = false;

    [Header("Zombie Health and Damage")]
    private float zombieHealth =600f;
    private float presentHealth;
    public float giveDamage = 10f;
    public HealthBar healthBar;

    [Header("Zombie things")]
    public NavMeshAgent zombieAgent;
    public Transform lookPoint;
    public Camera AttackingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Zomebie Standing Var")]  
    public float zombieSpeed;


    [Header("Zombie Attacking var")]
    public float timeBtwAttack;
    public bool previouslyAttack;

    [Header("Zombie Animation")]
    public Animator anim;
    public float timeAttack = 1.75f;
    public float timeReAttack = 1.25f;

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
        zombieAgent = GetComponent<NavMeshAgent>();
        healthBar.GiveFullHealth(zombieHealth);
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (!playerInvisionRadius && !playerInattackingRadius) Idle();
        if (playerInvisionRadius && !playerInattackingRadius) Pursueplayer();
        if (playerInvisionRadius && playerInattackingRadius) AttackPlayer();
    }

    private void Idle()
    {
        zombieAgent.SetDestination(transform.position);
        anim.SetBool("Idle", true);
        anim.SetBool("Running", false);
    }
    private void Pursueplayer()
    {
        if (zombieAgent.SetDestination(playerBody.position))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
        }
     
    }

    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(lookPoint);
        if (!previouslyAttack)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(AttackingRaycastArea.transform.position, AttackingRaycastArea.transform.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("Attacking" + hitInfo.transform.name);

                anim.SetBool("Running", false);
                anim.SetBool("Attacking", true);

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
        yield return new WaitForSeconds(timeReAttack);

    }

    public void zombieHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);
        if (presentHealth <= 0)
        {
            anim.SetBool("Died", true);

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

        if (died == false)
        {
            died = true;
            GameController.occurrence.defeatedBoss();
        }

        Object.Destroy(gameObject, 5.0f);
    }
}
