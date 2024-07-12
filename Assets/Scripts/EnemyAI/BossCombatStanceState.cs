using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class BossCombatStanceState : CombatStanceState
    {

        public bool hasPhaseShifted;
        public EnemyAttackAction[] secondPhaseAttacks;

        public override void GetNewAttack(EnemyManager enemyManager)
        {
            if (hasPhaseShifted)
            {
                Vector3 targetsDirection = enemyManager.currentTarget.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);

                int maxScore = 0;

                for (int i = 0; i < secondPhaseAttacks.Length; i++)
                {
                    EnemyAttackAction enemyAttackAction = secondPhaseAttacks[i];

                    if (enemyManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                        && enemyManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                    {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                            && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                        {
                            maxScore += enemyAttackAction.attackScore;
                        }
                    }
                }

                int randomValue = Random.Range(0, maxScore + 1);
                int temporaryScore = 0;

                for (int i = 0; i < secondPhaseAttacks.Length; i++)
                {
                    EnemyAttackAction enemyAttackAction = secondPhaseAttacks[i];

                    if (enemyManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                        && enemyManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                    {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                            && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                        {
                            if (attackState.currentAttack != null)
                                return;

                            temporaryScore += enemyAttackAction.attackScore;

                            if (temporaryScore > randomValue)
                            {
                                attackState.currentAttack = enemyAttackAction;
                            }
                        }
                    }
                }
            }
            else
            {
                base.GetNewAttack(enemyManager);
            }
        }
    }
}

