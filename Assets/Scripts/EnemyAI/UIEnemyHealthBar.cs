using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class UIEnemyHealthBar : MonoBehaviour
    {
        public Slider slider;
        Camera mainCamera;
        float timeUntilBarIsHidden = 0;
        public UIYellowBar yellowBar;
        public Text damage;
        int currentDamageTaken;
        public float Timer = 2f;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            mainCamera = FindObjectOfType<Camera>();
            yellowBar = GetComponentInChildren<UIYellowBar>();
        }

        private void Update()
        {
            timeUntilBarIsHidden = timeUntilBarIsHidden - Time.deltaTime;

            Timer = Timer - Time.deltaTime;

            if (slider != null)
            {
                if (Timer <= 0 && slider.IsActive())
                {
                    currentDamageTaken = 0;
                    damage.text = " ";
                }

                if (timeUntilBarIsHidden <= 0)
                {
                    timeUntilBarIsHidden = 0;
                    slider.gameObject.SetActive(false);
                }
                
                if (slider.value <= 0)
                {
                    Destroy(slider.gameObject);
                }
            }

        }
        private void LateUpdate()
        {
            if (slider != null)
            {
                transform.forward = mainCamera.transform.forward;
                transform.rotation = Quaternion.LookRotation((transform.position - Camera.main.transform.position).normalized);
            }
        }


        public void SetHealth(int health)
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
            timeUntilBarIsHidden = 3;
            /*
            if (yellowBar != null)
            {
                yellowBar.gameObject.SetActive(true);

                if (health > slider.value)
                {
                    yellowBar.slider.value = health;
                }
            }
            */
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
    }
}

