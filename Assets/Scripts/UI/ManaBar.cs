using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class ManaBar : MonoBehaviour
    {
        public Slider slider;
        public PlayerYellowManaBar yellowBar;

        private void Awake()
        {
            yellowBar = GetComponentInChildren<PlayerYellowManaBar>();
        }

        public void SetMaxMana(int maxMana) 
        {
            slider.maxValue = maxMana;
            slider.value = maxMana;

            if (yellowBar != null)
            {
                yellowBar.SetMaxStat(maxMana);
            }
        }

        public void SetCurrentMana(int currentMana)
        {
            if (yellowBar != null)
            {
                yellowBar.gameObject.SetActive(true);

                if (currentMana > slider.value)
                {
                    yellowBar.slider.value = currentMana;
                }
            }

            slider.value = currentMana;

            
        }
    }
}

