using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class EnemyWeaponSlotManager : MonoBehaviour
    {
        public WeaponItem rightHandWeapon;
        public WeaponItem leftHandWeapon;

        WeaponHolderSlot rightHandSlot;
        WeaponHolderSlot leftHandSlot;

        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        private void Start()
        {
            if (rightHandWeapon != null)
            {
                LoadWeaponOnSlot(rightHandWeapon, false);
            }

            if (leftHandWeapon != null)
            {
                LoadWeaponOnSlot(leftHandWeapon, true);
            }

        }

        public void LoadWeaponOnSlot(WeaponItem weapon, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weapon;
                leftHandSlot.LoadWeaponModel(weapon);
                LoadWeaponDamageCollider(true);
            }
            else
            { 
                rightHandSlot.currentWeapon = weapon;
                rightHandSlot.LoadWeaponModel(weapon);
                LoadWeaponDamageCollider(false);
            }
        }

        public void LoadWeaponDamageCollider(bool isLeft)
        {
            if (isLeft)
            {
                leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                leftHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
            }
            else
            {
                rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
            }
            
        }

        public void OpenSkillEffect()
        {
            rightHandDamageCollider.EnableParticleEffect();

        }

        public void CloseSkillEffect()
        {
            rightHandDamageCollider.DisableParticleEffect();
        }
        public void OpenDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }


        public void DrainStaminaLightAttack()
        {
           // playerStats.TakeStaminaDrain(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
        }

        public void DrainStamina2HLightAttack()
        {
            //playerStats.TakeStaminaDrain(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier * 1.5f));
        }

        public void DrainStaminaHeavyAttack()
        {
            //playerStats.TakeStaminaDrain(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
        }

        public void DrainStamina2HHeavyAttack()
        {
            //playerStats.TakeStaminaDrain(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier * 1.5f));
        }

        public void EnableCombo()
        {
            //anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            //anim.SetBool("canDoCombo", false);
        }

        #region Handle Damage
        public void LightAttack1Damage()
        {
            rightHandDamageCollider.currentWeaponDamage = rightHandSlot.currentWeapon.light1damage;
        }

        public void LightAttack2Damage()
        {
            rightHandDamageCollider.currentWeaponDamage = rightHandSlot.currentWeapon.light2damage;
        }

        public void HeavyAttackDamage()
        {
            rightHandDamageCollider.currentWeaponDamage = rightHandSlot.currentWeapon.heavydamage;
        }

        public void LightAttack1Damage_2H()
        {
            rightHandDamageCollider.currentWeaponDamage = Mathf.RoundToInt(rightHandSlot.currentWeapon.light1damage * 1.5f);
        }

        public void LightAttack2Damage_2H()
        {
            rightHandDamageCollider.currentWeaponDamage = Mathf.RoundToInt(rightHandSlot.currentWeapon.light2damage * 1.5f);
        }

        public void HeavyAttackDamage_2H()
        {
            rightHandDamageCollider.currentWeaponDamage = Mathf.RoundToInt(rightHandSlot.currentWeapon.heavydamage * 1.5f);
        }

        public void SkillDamage()
        {
            rightHandDamageCollider.currentWeaponDamage = rightHandSlot.currentWeapon.skilldamage;
        }
        #endregion
    }

}
