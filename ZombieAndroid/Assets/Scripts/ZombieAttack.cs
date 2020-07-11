using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttack : MonoBehaviour
{
    public GameObject player;
    [HideInInspector]
    public GameObject enemy;

    public float triggerDistance;
    public float provokeDistance;
    public float maxViewAngle;
    public float turnSpeed;
    public float initSpeed;
    public float moveSpeed;
    public float acceleration;
    public float deceleration;

    [HideInInspector]
    public Animator enemyAnimator;
    [HideInInspector]
    public NavMeshAgent enemyNavMeshAgent;
        
    [HideInInspector]
    public PlayerHealth playerHealth;
    [HideInInspector]
    public ZombieHealth zombieHealth;
    public float playerDamage;

    void Start()
    {
        enemy = this.gameObject;
        enemyAnimator = enemy.GetComponent<Animator>();
        enemyNavMeshAgent = enemy.GetComponent<NavMeshAgent>();
        enemy.layer = LayerMask.NameToLayer("Enemy");
        enemyNavMeshAgent.speed = initSpeed;
        playerHealth = GameObject.Find("GameManager").GetComponent<PlayerHealth>();
        zombieHealth = GetComponent<ZombieHealth>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = player.transform.position - enemy.transform.position;
        float viewAngle = Vector3.Angle(playerDirection, enemy.transform.forward);
        float distanceToPlayer = Vector3.Distance(player.transform.position, enemy.transform.position);
        float playerDirectionMag = playerDirection.magnitude;

        if (distanceToPlayer < triggerDistance && viewAngle < maxViewAngle)
        {

            playerDirection.y = 0;
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation,
                Quaternion.LookRotation(playerDirection), turnSpeed * Time.deltaTime);

            enemyAnimator.SetBool("isIdle", false);
            enemyAnimator.SetBool("GetHit", false);

            if (playerDirectionMag > provokeDistance)
            {
                moveSpeed += acceleration * Time.deltaTime;
                enemyNavMeshAgent.SetDestination(player.transform.position);
                enemyNavMeshAgent.speed = moveSpeed;
                enemyAnimator.SetBool("isWalking", true);
                enemyAnimator.SetBool("isAttacking", false);
                enemyAnimator.SetBool("GetHit", false);

            }
            else
            {
                moveSpeed -= deceleration * Time.deltaTime;
                enemyNavMeshAgent.speed = initSpeed;
                enemyAnimator.SetBool("isAttacking", true);
                enemyAnimator.SetBool("isWalking", false);
                enemyAnimator.SetBool("GetHit", false);
                playerHealth.PlayerDamage(playerDamage);
            }

        }
        else
        {
            moveSpeed = initSpeed;
            enemyNavMeshAgent.SetDestination(enemy.transform.position);
            enemyNavMeshAgent.speed = initSpeed;
            enemyAnimator.SetBool("isIdle", true);
            enemyAnimator.SetBool("isWalking", false);
            enemyAnimator.SetBool("isAttacking", false);
            enemyAnimator.SetBool("GetHit", false);
        }

        if (zombieHealth.gettingHit)
        {
            enemyAnimator.SetBool("isIdle", false);
            enemyAnimator.SetBool("isWalking", false);
            enemyAnimator.SetBool("isAttacking", false);
            enemyAnimator.SetBool("GetHit", true);

            moveSpeed -= deceleration * Time.deltaTime;
            enemyNavMeshAgent.speed = initSpeed;
        }

        if (zombieHealth.enemyDied)
        {
            enemyAnimator.SetBool("isDead", true);
            enemyNavMeshAgent.speed = initSpeed;
        }

    }
}

