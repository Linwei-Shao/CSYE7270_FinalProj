using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "Spells/Projectile Spell")]
    public class ProjectileSpell : SpellItem
    {
        public float baseDamage;

        [Header("Projectile Physics")]
        public float projectileVelocity;
        public float projectileUpwardVelocity;
        public bool isEffectedByGravity;
        public float projectileMass;
        Rigidbody rigidbody;


        public GameObject instantiatedWarmUpSpellFX;
        public GameObject instantiatedSpellFX;


        public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, WeaponSlotManager weaponSlotManager)
        {
            instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManager.rightHandSlot.transform);
            
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
        }

        public override void DestroyWarmUpEffect()
        {
            Destroy(instantiatedWarmUpSpellFX);
        }

        // public override void DestroySpellEffect()
        //{

        //  Destroy(instantiatedSpellFX);
        //}



        public override void SuccessfullyCasted(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, CameraHandler cameraHandler, WeaponSlotManager weaponSlotManager)
        {
            instantiatedSpellFX = Instantiate(spellCastFX, weaponSlotManager.rightHandSlot.transform.position, cameraHandler.cameraPivotTransform.rotation);
            rigidbody = instantiatedSpellFX.GetComponent<Rigidbody>();

            if (cameraHandler.currentLockOnTarget != null)
            {
                instantiatedSpellFX.transform.LookAt(cameraHandler.currentLockOnTarget.transform);
            }
            else
            {
                instantiatedSpellFX.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.eulerAngles.x, playerStats.transform.eulerAngles.y, 0);
            }

            rigidbody.AddForce(instantiatedSpellFX.transform.forward * projectileVelocity);
            rigidbody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocity);
            rigidbody.useGravity = isEffectedByGravity;
            rigidbody.mass = projectileMass;
            instantiatedSpellFX.transform.parent = null;
        }
    }

}

