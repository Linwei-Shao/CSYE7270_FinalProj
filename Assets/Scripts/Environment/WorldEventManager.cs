using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class WorldEventManager : MonoBehaviour
    {
        public BossHealthBar bossHealthBar;
        public BGMController bGMController;
        public List<FogWall> fogWalls;
        public BossManager boss;

        public bool bossFightIsActive;
        public bool bossHasBeenAwakened;
        public bool bossHasBeenDefeated;

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<BossHealthBar>();
            bGMController = FindObjectOfType<BGMController>();
        }

        public void ActiveBossFight()
        {
            bossFightIsActive = true;
            bossHasBeenAwakened = true;
            bossHealthBar.OpenUIHealthBar();
            bGMController.PlayBossBGM();

            foreach (var fogWall in fogWalls)
            {
                fogWall.ActiveFogWall();
            }
        }

        public void BossDefeated()
        {
            bossHasBeenDefeated = true;
            bossFightIsActive = false;

            foreach (var fogWall in fogWalls)
            {
                fogWall.DeactiveFogWall();
            }
        }
    }
}

