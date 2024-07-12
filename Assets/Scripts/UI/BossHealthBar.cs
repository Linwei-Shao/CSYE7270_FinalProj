using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class BossHealthBar : MonoBehaviour
    {
        public Text bossName;
        public Slider slider;

        public BossYellowBar yellowBar;
        public Text damage;
        int currentDamageTaken;
        public float Timer = 2f;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            bossName = GetComponentInChildren<Text>();
            yellowBar = GetComponentInChildren<BossYellowBar>();
        }

        private void Update()
        {
            Timer = Timer - Time.deltaTime;

            if (Timer <= 0 && slider.IsActive())
            {
                currentDamageTaken = 0;
                damage.text = " ";
            }
        }

        private void Start()
        {
            CloseUIHealthBar();
        }

        public void SetBossName(string name)
        {
            bossName.text = name;
        }

        public void OpenUIHealthBar()
        {
            slider.gameObject.SetActive(true);
        }

        public void CloseUIHealthBar()
        {
            slider.gameObject.SetActive(false);
        }

        public void SetBossMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;

            if (yellowBar != null)
            {
                yellowBar.SetMaxStat(maxHealth);
            }
        }

        public void SetBossCurrentHealth(int health)
        {
            if (yellowBar != null)
            {
                yellowBar.gameObject.SetActive(true);

                if (health > slider.value)
                {
                    yellowBar.slider.value = health;
                }
            }

            Timer = 1.5f;
            currentDamageTaken = currentDamageTaken + Mathf.RoundToInt((slider.value - health));
            damage.text = currentDamageTaken.ToString();


            slider.value = health;

            if (yellowBar != null)
            {
                yellowBar.gameObject.SetActive(true);

                if (health > slider.value)
                {
                    yellowBar.slider.value = health;
                }
            }
        }
    }
}

