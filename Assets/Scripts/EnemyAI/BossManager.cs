using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class BossManager : MonoBehaviour
    {
        public string bossName;
        public BossHealthBar bossHealthBar;
        public EnemyStats enemyStats;
        public EnemyAnimatorManager enemyAnimatorManager;

        public GameObject weaponParticleFX;
        public GameObject headParticleFX;
        public GameObject phaseFX;

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<BossHealthBar>();
            enemyStats = GetComponent<EnemyStats>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        }

        private void Start()
        {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }

        public void UpdateBossHealthBar(int currentHealth, int maxHealth)
        {
            bossHealthBar.SetBossCurrentHealth(currentHealth);

            
        }

        public void ShiftTo2ndPhase()
        {
            

        }
    }
}

