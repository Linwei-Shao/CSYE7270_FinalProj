using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class PlayerYellowStaminaBar : MonoBehaviour
    {
        public Slider slider;
        StaminaBar parentStaminabar;

        public float timer;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            parentStaminabar = GetComponentInParent<StaminaBar>();

        }

        private void OnEnable()
        {
            if (timer <= 0)
            {
                timer = 2f;
            }
        }

        public void SetMaxStat(int maxStat)
        {
            slider.maxValue = maxStat;
            slider.value = maxStat;



        }

        private void Update()
        {
            if (timer <= 0)
            {
                if (slider.value > parentStaminabar.slider.value)
                {
                    slider.value = slider.value - 5f;
                }
                else if (slider.value <= parentStaminabar.slider.value)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                timer = timer - Time.deltaTime;
            }
        }
    }
}

