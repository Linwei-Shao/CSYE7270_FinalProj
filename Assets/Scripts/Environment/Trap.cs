using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS {
    public class Trap : MonoBehaviour
    {
        public PlayerStats playerStats;
        GameObject trapSpears;
        public Animator anim;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            playerStats = other.GetComponentInParent<PlayerStats>();

            if (playerStats != null)
            {
                anim.Play("TrapSpear");
            }
        }
    }
}

