using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    /*================================================================
     Basic enemy AI movement is given here.
     =================================================================*/

    public class EnemyAI : MonoBehaviour
    {
        public GameObject player;
        public GameObject enemy;
        
        public float triggerDistance;
        public float provokeDistance;
        public float maxViewAngle;
        public float turnSpeed;
        public float initSpeed;
        public float moveSpeed;
        public float acceleration;
        public float deceleration;

        public Animator enemyAnimator;
        public NavMeshAgent enemyNavMeshAgent;

        public void BasicAIInitiate()
        {
            enemyAnimator = enemy.GetComponent<Animator>();
            enemyNavMeshAgent = enemy.GetComponent<NavMeshAgent>();

            enemyNavMeshAgent.speed = initSpeed;

            #region Gameobjects assignment check

            if (enemyAnimator == null)
            {
                Debug.LogError("Animator component is missing. Attach an animator component to the gameobject.");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }

            if (enemyNavMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent component is missing. Attach a NavMeshAgent component to the gameobject.");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }

            if (player == null)
            {
                Debug.LogError("Player Gameobject is missing. Assign a player object to the Player variable.");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }

            if (enemy == null)
            {
                Debug.LogError("Enemey Gameobject is missing. Assign an enemy object to the Enemy variable.");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            if (triggerDistance == 0 || provokeDistance == 0 || maxViewAngle == 0 || turnSpeed == 0 || moveSpeed == 0 || acceleration == 0 || deceleration == 0)
            {
                Debug.LogWarning("One of the public variable value is not optimum. Enemy AI may not work as intended.");
            }

            #endregion

        }

        public void AIMovement()
        {
            /*CALCULATE THE DISTANCE & DIRECTION OF THE PLAYER FROM ENEMY AND VIEW ANGLE OF THE ENEMY. 
             CALL THIS METHOD IN UPDATE*/

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

                if (playerDirectionMag > provokeDistance)
                {
                    moveSpeed += acceleration * Time.deltaTime;
                    enemyNavMeshAgent.SetDestination(player.transform.position);
                    enemyNavMeshAgent.speed = moveSpeed;
                    enemyAnimator.SetBool("isWalking", true);
                    enemyAnimator.SetBool("isAttacking", false);
                    
                }
                else
                {
                    moveSpeed -= deceleration * Time.deltaTime;
                    enemyNavMeshAgent.speed = initSpeed;
                    enemyAnimator.SetBool("isAttacking", true);
                    enemyAnimator.SetBool("isWalking", false);
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
            }

        }

    }

    public class EnemyHealth : EnemyAI {

       

    }

}

