using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        EnemyManager enemyManager;
        EnemyStats enemyStats;
        BossManager bossManager;

        public PlayerStats playerStats;
        public SoulCount soulCount;

     
        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyManager = GetComponentInParent<EnemyManager>();
            bossManager = GetComponentInParent<BossManager>();
            enemyStats = GetComponentInParent<EnemyStats>();
            playerStats = FindObjectOfType<PlayerStats>();
            soulCount = FindObjectOfType<SoulCount>();
        }

        private void Start()
        {
            soulCount.SetCurrentSoul(playerStats.soulCount);
        }

        public override void TakeCriticalDamageAnimationEvent()
        {
            enemyStats.TakeDamageNoAnim(enemyManager.pendingCriticalDamage);
            enemyManager.pendingCriticalDamage = 0;
        }


        public void AwardSoulsOnDeath()
        {

            if (playerStats != null)
            {
                playerStats.AddSouls(enemyStats.soulsAwarded);

                if (soulCount != null)
                {
                    soulCount.SetCurrentSoul(playerStats.soulCount);
                };
            }

            
        }

        public void CanRotate()
        {
            anim.SetBool("canRotate", true);
        }

        public void StopRotation()
        {
            anim.SetBool("canRotate", false);
        }

        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }

        public void EnableIsInvulnerable()
        {
            anim.SetBool("isInvulnerable", true);
        }

        public void DisableIsInvulnerable()
        {
            anim.SetBool("isInvulnerable", false);
        }

        public void StartPhaseShifting()
        {
            anim.SetBool("isPhaseShifting", true);
        }

        public void EndPhaseShifting()
        {
            anim.SetBool("isPhaseShifting", false);
        }

        public void EnableIsParrying()
        {
            enemyManager.isParrying = true;
        }

        public void DisableIsParrying()
        {
            enemyManager.isParrying = false;
        }

        public void EnableCanBeRiposted()
        {
            enemyManager.canBeRiposted = true;
        }

        public void DisableCanBeRiposted()
        {
            enemyManager.canBeRiposted = false;
        }

        public void InstantiateBossParticleFX()
        {
            BossFXTransform bossFXTransform = GetComponentInChildren<BossFXTransform>();
            HeadTransform headTransform = GetComponentInChildren<HeadTransform>();
            

            GameObject phaseWeaponFX = Instantiate(bossManager.weaponParticleFX, bossFXTransform.transform);
            GameObject phaseHeadFX = Instantiate(bossManager.headParticleFX, headTransform.transform);
            
        }

        public void InstantiateGroundCrackFX()
        {
            RootTransform rootTransform = GetComponentInChildren<RootTransform>();
            GameObject phaseFX = Instantiate(bossManager.phaseFX, rootTransform.transform);
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidBody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemyRigidBody.velocity = velocity;

            if (enemyManager.isRotatingWithRoot)
            {
                enemyManager.transform.rotation *= anim.deltaRotation;
            }
        }

        
    }
}

