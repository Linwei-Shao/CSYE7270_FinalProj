using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class LabTrap : MonoBehaviour
    {
        public GameObject fireTrap1;
        public GameObject fireTrap2;
        public GameObject acidTrap;

        public float timer;

        private void Update()
        {
            
            timer += Time.deltaTime;

            fireTrap1.SetActive(IsObjectActive(timer, 4, 3, 1));
            fireTrap2.SetActive(IsObjectActive(timer - 1, 4, 3, 1)); 
            acidTrap.SetActive(IsObjectActive(timer, 5, 4, 1));
        }

        private bool IsObjectActive(float currentTime, float interval, float duration, float gap)
        {
            float phase = Mathf.Floor(currentTime / (interval + gap));

            float startTime = phase * (interval + gap);
            float endTime = startTime + duration;
            return currentTime >= startTime && currentTime < endTime;
        }
    }
}

