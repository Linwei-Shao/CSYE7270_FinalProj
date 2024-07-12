using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 25;

        BoxCollider boxCollider;

        private void Awake()
        {
            boxCollider = this.GetComponent<BoxCollider>();
        }
        public void DisableColldier()
        {
            boxCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            PlayerStats playerStats = collision.GetComponentInParent<PlayerStats>();

            PlayerEffectsManager playerEffectManager = collision.GetComponentInParent<PlayerEffectsManager>();

            if (this.CompareTag("PhysicalTrap"))
            {
                Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                playerEffectManager.PlayBloodFX(contactPoint);
            }
            else if (this.CompareTag("FireTrap"))
            {
                Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                playerEffectManager.PlayFireFX(contactPoint);
            }
            else if (this.CompareTag("AcidTrap"))
            {
                Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                playerEffectManager.PlayAcidFX(contactPoint);
            }
            

            if (playerStats != null)
            {
                if (this.CompareTag("FireTrap"))
                {
                    playerStats.TakeFireDamage(damage);
                }
                else
                {
                    playerStats.TakeDamage(damage);
                }
                
            }
        }
    }
}

