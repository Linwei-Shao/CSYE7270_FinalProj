using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class SpellDamageCollider : DamageCollider
    {
        public GameObject impactFX;
        public GameObject projectileFX;
        public GameObject muzzleFX;

        public bool isPyroSpell;
        public bool isMageSpell;

        bool hasCollided = false;

        CharacterStats spellTarget;
        public PlayerStats playerStats;
        //Rigidbody rigidbody;

        Vector3 impactNormal;

        private void Awake()
        {
            //rigidbody = GetComponent<Rigidbody>();
            playerStats = FindObjectOfType<PlayerStats>();
            if (isPyroSpell)
            {
                currentWeaponDamage = Mathf.RoundToInt(playerStats.faithLevel * 5.88f);
            }
            else if (isMageSpell)
            {

            }
            else
            { 
            
            }
        }
        private void Start()
        {
            projectileFX = Instantiate(projectileFX, transform.position, transform.rotation);
            projectileFX.transform.parent = transform;

            if (muzzleFX)
            {
                muzzleFX = Instantiate(muzzleFX, transform.position, transform.rotation);
                Destroy(muzzleFX, 0.5f);
            }
        }

        
        private void OnCollisionEnter(Collision collision)
        {
            if (!hasCollided)
            {
                spellTarget = collision.transform.GetComponent<CharacterStats>();

                if (spellTarget != null)
                {
                    spellTarget.TakeFireDamage(currentWeaponDamage);
                }

                hasCollided = true;
                impactFX = Instantiate(impactFX, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
                Destroy(projectileFX);
                Destroy(impactFX, 2f);
                Destroy(gameObject, 2f);
                
            }
        }
    }
}

