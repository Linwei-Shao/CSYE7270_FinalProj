using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class StaminaBar : MonoBehaviour
    {
        public Slider slider;

        public PlayerYellowStaminaBar yellowBar;

        private void Awake()
        {
            yellowBar = GetComponentInChildren<PlayerYellowStaminaBar>();
        }

        public void SetMaxStamina(int maxStamina) 
        {
            slider.maxValue = maxStamina;
            slider.value = maxStamina;

            if (yellowBar != null)
            {
                yellowBar.SetMaxStat(maxStamina);
            }
        }

        public void SetCurrentStamina(float currentStamina)
        {
            if (yellowBar != null)
            {
                yellowBar.gameObject.SetActive(true);

                if (currentStamina > slider.value)
                {
                    yellowBar.slider.value = currentStamina;
                }
            }

            slider.value = currentStamina;

            
        }
    }
}

