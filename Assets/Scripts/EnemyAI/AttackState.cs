using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class AttackState : State
    {
        public CombatStanceState combatStanceState;
        public PursueTargetState pursueTargetState;
        public EnemyAttackAction currentAttack;
        public RotateTowardsState rotateTowardsState;

        public bool willDoComboOnNext;
        public bool hasPerformedAttack;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            HandleRotateTowardsTarget(enemyManager);

            if (enemyManager.distanceFromTarget > enemyManager.maximumAggroRadius)
            {
                return pursueTargetState;
            }

            if (willDoComboOnNext && enemyManager.canDoCombo && !enemyManager.canBeRiposted)
            {
                HandleRotateTowardsTarget(enemyManager);
                AttackTargetWithCombo(enemyAnimatorManager, enemyManager);
            }

            if(!hasPerformedAttack && currentAttack != null)
            {
                HandleRotateTowardsTarget(enemyManager);
                AttackTarget(enemyAnimatorManager, enemyManager);
                RollForComboChance(enemyManager);
            }

            

            if (willDoComboOnNext && hasPerformedAttack)
            {
                HandleRotateTowardsTarget(enemyManager);
                return this;
            }


                return rotateTowardsState;
        }

        private void AttackTarget(EnemyAnimatorManager enemyAnimatorManager, EnemyManager enemyManager)
        {
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyManager.currentRevoveryTime = currentAttack.recoveryTime;
            hasPerformedAttack = true;
        }

        private void AttackTargetWithCombo(EnemyAnimatorManager enemyAnimatorManager, EnemyManager enemyManager)
        {
            willDoComboOnNext = false;
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyManager.currentRevoveryTime = currentAttack.recoveryTime;
            hasPerformedAttack = false;
            currentAttack = null;
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            if (enemyManager.canRotate && enemyManager.isInteracting)
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
            
        }
        

        private void RollForComboChance(EnemyManager enemyManager)
        {
            float comboChance = UnityEngine.Random.Range(0, 100);

            if (enemyManager.allowToPerformCombos && comboChance <= enemyManager.comboLikelyHood)
            {
                if (currentAttack.comboAction != null)
                {
                    willDoComboOnNext = true;
                    currentAttack = currentAttack.comboAction;
                }
                else
                {
                    willDoComboOnNext = false;
                    currentAttack = null;
                }
            }
        }
    }
}

