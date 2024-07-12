using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LS
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyStats enemyStats;
        PlayerManager playerManager;

        public State currentState;
        public CharacterStats currentTarget;
        public NavMeshAgent navmeshAgent;
        public Rigidbody enemyRigidBody;

        
        public bool isPerformingAction;
        public bool isInteracting;
        public float rotationSpeed = 15;
        public float maximumAggroRadius = 1.5f;

        [Header("Combat Flags")]
        public bool canDoCombo;
        public bool isInvulnerable;
        public bool isPhaseShifting;

        [Header("AI Settings")]
        public float detectionRadius = 20;

        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;

        public float currentRevoveryTime = 0;

        [Header("AI Combat Settings")]
        public bool allowToPerformCombos;
        public float comboLikelyHood;

        public float distanceFromTarget;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyStats = GetComponent<EnemyStats>();
            navmeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRigidBody = GetComponent<Rigidbody>();
            playerManager = FindObjectOfType<PlayerManager>();

            navmeshAgent.enabled = false;
            
        }

        private void Start()
        {
            enemyRigidBody.isKinematic = false;
        }

        private void Update()
        {
            HandleRecoveryTimer();

            isRotatingWithRoot = enemyAnimatorManager.anim.GetBool("isRotatingWithRoot");
            isInteracting = enemyAnimatorManager.anim.GetBool("isInteracting");
            enemyAnimatorManager.anim.SetBool("isDead", enemyStats.isDead);
            canDoCombo = enemyAnimatorManager.anim.GetBool("canDoCombo");
            canRotate = enemyAnimatorManager.anim.GetBool("canRotate");
            isInvulnerable = enemyAnimatorManager.anim.GetBool("isInvulnerable");
            isPhaseShifting = enemyAnimatorManager.anim.GetBool("isPhaseShifting");

            if (currentTarget != null)
            {
                distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
                playerManager.isCombating = true;
            }
        }

        private void LateUpdate()
        {
            HandleStateMachine();

            transform.position = new Vector3(transform.position.x, navmeshAgent.transform.position.y, transform.position.z);

            navmeshAgent.transform.localPosition = Vector3.zero;
            navmeshAgent.transform.localRotation = Quaternion.identity;

        }

        private void HandleStateMachine()
        {
            if (enemyStats.isDead)
                return;

            else if (currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);

                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        private void HandleRecoveryTimer()
        {
            if (currentRevoveryTime > 0)
            {
                currentRevoveryTime -= Time.deltaTime;
            }

            if(isPerformingAction)
            {
                if (currentRevoveryTime <= 0)
                {
                    isPerformingAction = false;
                }

            }
        }

        }
    }

