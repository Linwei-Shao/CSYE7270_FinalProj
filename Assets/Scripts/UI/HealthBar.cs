using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public PlayerYellowHPBar yellowBar;

        private void Awake()
        {
            yellowBar = GetComponentInChildren<PlayerYellowHPBar>();
        }

        public void SetMaxHealth(int maxHealth) 
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;


            if (yellowBar != null)
            {
                yellowBar.SetMaxStat(maxHealth);
            }
        }

        public void SetCurrentHealth(int health)
        {
            if (yellowBar != null)
            {
                yellowBar.gameObject.SetActive(true);

                if (health > slider.value)
                {
                    yellowBar.slider.value = health;
                }
            }

            slider.value = health;

            
        }
    }
}

