using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class PursueTargetState : State
    {
        public CombatStanceState combatStanceState;
        public RotateTowardsState rotateTowardsState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

            

            HandleRotateTowardsTarget(enemyManager);

            if (enemyManager.isInteracting)
                return this;

            if (enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return this;
            }

            if (enemyManager.distanceFromTarget > enemyManager.maximumAggroRadius)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 1f, 0.1f, Time.deltaTime);

                targetDirection.Normalize();
                targetDirection.y = 0;

                float speed = 5;
                targetDirection *= speed;
                Vector3 projectedVelocity = Vector3.ProjectOnPlane(targetDirection, Vector3.up);
                enemyManager.enemyRigidBody.velocity = projectedVelocity;
            }

            
            if (enemyManager.distanceFromTarget <= enemyManager.maximumAggroRadius)
            {
                return combatStanceState;
            }
            else
            {
                return this;
            }
            
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            if (enemyManager.isPerformingAction)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = enemyManager.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
            }
            else
            {
                Vector3 relativeDirection = enemyManager.transform.InverseTransformDirection(enemyManager.navmeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;

                enemyManager.navmeshAgent.enabled = true;
                enemyManager.navmeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidBody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navmeshAgent.transform.rotation, enemyManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}

