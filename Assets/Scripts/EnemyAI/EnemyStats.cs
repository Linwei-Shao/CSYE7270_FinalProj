using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class EnemyStats : CharacterStats
    {
        public BonfireInteractable bonfire;
        EnemyAnimatorManager enemyAnimatorManager;
        WorldEventManager worldEventManager;
        public BossManager bossManager;
        public UIEnemyHealthBar enemyHealthbar;
        public BossHealthBar bossHealthBar;
        public Collider collider;
        EnemyManager enemyManager;
        public GameObject enemy;
        public bool isBoss;
        BossCombatStanceState bossCombatStanceState;
        AttackState attackState;
        public DamageCollider damageCollider;
        PlayerManager playerManager;

        public UIManager uiManager;

        public int soulsAwarded;

        private void Awake()
        {
            bossManager = GetComponent<BossManager>();
            collider = GetComponent<Collider>();
            collider.gameObject.SetActive(true);
            collider.enabled = true;
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            bossHealthBar = FindObjectOfType<BossHealthBar>();
            worldEventManager = FindObjectOfType<WorldEventManager>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
            attackState = GetComponentInChildren<AttackState>();
            playerManager = FindObjectOfType<PlayerManager>();
        }

        void Start()
        {
            if (!isBoss)
            {
                enemyHealthbar.SetMaxHealth(maxHealth);
            }
            
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = vigorLevel * 18 + 300;
               return maxHealth;
        }

        public void TakeDamageNoAnim(int physicalDamage)
        {
            if (isDead)
                return;

            if (enemyManager.isInvulnerable)
                return;

            float totalPhysicalDamageAbsorption = 1 - (1 - physicalDamaageAbsorptionHead / 100) *
                (1 - physicalDamaageAbsorptionHand / 100) *
                (1 - physicalDamaageAbsorptionBody / 100) *
                (1 - physicalDamaageAbsorptionLeg / 100);

            physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

            float finalDamage = physicalDamage;

            currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

            if (!isBoss)
            {
                
                if (enemyHealthbar.slider != null)
                {
                    enemyHealthbar.SetHealth(currentHealth);
                    enemyHealthbar.slider.gameObject.SetActive(true);
                }
            }
            else if (isBoss && bossManager != null)
            {
                bossManager.UpdateBossHealthBar(currentHealth, maxHealth);

                if (currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted)
                {
                    damageCollider = GetComponentInChildren<DamageCollider>();
                    bossCombatStanceState.hasPhaseShifted = true;
                    damageCollider.isFireDamage = true;
                    attackState.currentAttack = null;
                    enemyAnimatorManager.PlayTargetAnimation("Roar", true);
                }
            }

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        public override void TakeDamage(int damage, string damageAnimation = "Damage")
        {
            if (isDead)
                return;

            if (enemyManager.isInvulnerable)
                return;

            base.TakeDamage(damage, damageAnimation = "Damage");

            if (!isBoss)
            {
                
                if (enemyHealthbar.slider != null)
                {
                    enemyHealthbar.SetHealth(currentHealth);
                    enemyHealthbar.slider.gameObject.SetActive(true);
                }

                if (damage >= 20)
                {
                    enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);
                }
                

            }
            else if (isBoss && bossManager != null)
            {
                bossManager.UpdateBossHealthBar(currentHealth, maxHealth);

                if (currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted)
                {
                    damageCollider = GetComponentInChildren<DamageCollider>();
                    bossCombatStanceState.hasPhaseShifted = true;
                    damageCollider.isFireDamage = true;
                    attackState.currentAttack = null;
                    enemyAnimatorManager.PlayTargetAnimation("Roar", true);
                }
                else 
                {
                    if (damage >= 40)
                    {
                        enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);
                    }
                }
                
            }

            

            

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        public override void TakeFireDamage(int damage, string damageAnimation = "Damage")
        {
            if (isDead)
                return;

            if (enemyManager.isInvulnerable)
                return;

            base.TakeFireDamage(damage, damageAnimation = "Damage");

            if (!isBoss)
            {
               
                if (enemyHealthbar.slider != null)
                {
                    enemyHealthbar.SetHealth(currentHealth);
                    enemyHealthbar.slider.gameObject.SetActive(true);
                }

                if (damage >= 20)
                {
                    enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);
                }

            }
            else if (isBoss && bossManager != null) 
            {
                bossManager.UpdateBossHealthBar(currentHealth, maxHealth);

                if (currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted)
                {
                    damageCollider = GetComponentInChildren<DamageCollider>();
                    bossCombatStanceState.hasPhaseShifted = true;
                    damageCollider.isFireDamage = true;
                    attackState.currentAttack = null;
                    enemyAnimatorManager.PlayTargetAnimation("Roar", true);
                }
                else
                {
                    if (damage >= 40)
                    {
                        enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);
                    }
                }

            }

            if (currentHealth <= 0)
            {
                HandleDeath();
            }


        }

        public void HandleDeath()
        {
            currentHealth = 0;
            enemyManager.currentTarget = null;
            playerManager.isCombating = false;

            enemyAnimatorManager.anim.SetBool("isDead", true);
            isDead = true;

            enemyAnimatorManager.PlayTargetAnimation("Damage_Die", true);
            Destroy(enemy, 20f);

            if (!isBoss)
            {
                enemyHealthbar.SetHealth(currentHealth);
            }
            else if (isBoss && bossManager != null)
            {
                if (bonfire.isBossBonfire)
                {
                    bonfire.gameObject.SetActive(true);
                }
                uiManager.EnemyFelledUI();
                bossHealthBar.CloseUIHealthBar();
                worldEventManager.BossDefeated();
            }
           
            
            
            
        }
    }

}
