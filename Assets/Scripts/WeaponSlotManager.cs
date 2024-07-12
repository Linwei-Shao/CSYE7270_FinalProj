using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS{
    public class WeaponSlotManager : MonoBehaviour
    {
        PlayerManager playerManager;
        PlayerAnimatorManager animatorHandler;
        PlayerInventory playerInventory;

        public WeaponHolderSlot leftHandSlot;
        public WeaponHolderSlot rightHandSlot;
        public WeaponHolderSlot backSlot;

        public DamageCollider leftHandDamageCollider;
        public DamageCollider rightHandDamageCollider;

        public WeaponItem attackingWeapon;

        Animator animator;

        QuickSlotsUI quickSlotsUI;

        PlayerStats playerStats;
        InputHandler inputHandler;

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            animator = GetComponent<Animator>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            playerStats = GetComponentInParent<PlayerStats>();
            inputHandler = GetComponentInParent<InputHandler>();
            animatorHandler = GetComponent<PlayerAnimatorManager>();

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
                else if (weaponSlot.isBackSlot)
                {
                    backSlot = weaponSlot;
                }
            }
        }

        public void LoadBothWeapon()
        {
            LoadWeaponOnSlot(playerInventory.rightWeapon, false);
            LoadWeaponOnSlot(playerInventory.leftWeapon, true);
        }

        public void UnloadBothWeapon()
        {
            rightHandSlot.UnloadWeapon();
            leftHandSlot.UnloadWeapon();
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);

                #region Handle Weapon Idle Animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.left_Hand_Idle, 0.2f);
                }
                else 
                {
                    animator.CrossFade("LeftArmEmpty", 0.2f);
                }
                #endregion
            }
            else 
            {
                if (inputHandler.twoHandFlag)
                {
                    animatorHandler.PlayTargetAnimation("To_2H", true);
                    playerManager.isUsingLeftHand = false;
                    animator.CrossFade(weaponItem.two_Hand_Idle, 0.2f);
                }
                else
                {
                    #region Handle Weapon Idle Animations

                    animator.CrossFade("BothArmsEmpty", 0.2f);

                    backSlot.UnloadWeaponAndDestroy();

                    if (weaponItem != null)
                    {
                        animator.CrossFade(weaponItem.right_Hand_Idle, 0.2f);
                    }
                    else
                    {
                        animator.CrossFade("RightArmEmpty", 0.2f);
                    }
                    #endregion
                }

                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
            }
        }

        #region Handle Weapon's Damage Collider
    

        private void LoadLeftWeaponDamageCollider() 
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            leftHandDamageCollider.currentWeaponDamage = playerInventory.leftWeapon.light1damage;
            leftHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        }

        private void LoadRightWeaponDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            rightHandDamageCollider.currentWeaponDamage = playerInventory.rightWeapon.light1damage;
            rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        }

        public void OpenDamageCollider()
        {
            if (playerManager.isUsingRightHand)
            {
                rightHandDamageCollider.EnableDamageCollider();
            }
            else if (playerManager.isUsingLeftHand)
            {
                leftHandDamageCollider.EnableDamageCollider();
            }
            
        }

        public void CloseDamageCollider()
        {
                rightHandDamageCollider.DisableDamageCollider();
                leftHandDamageCollider.DisableDamageCollider();

        }

        public void toTwoHand()
        {
            backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
            leftHandSlot.UnloadWeaponAndDestroy();
        }

        #endregion

        public void OpenSkillEffect()
        {
            rightHandDamageCollider.EnableParticleEffect();

        }

        public void CloseSkillEffect()
        {
            rightHandDamageCollider.DisableParticleEffect();
        }

        #region Handle Damage
        public void LightAttack1Damage()
        {
            rightHandDamageCollider.currentWeaponDamage = attackingWeapon.light1damage;
        }

        public void LightAttack2Damage()
        {
            rightHandDamageCollider.currentWeaponDamage = attackingWeapon.light2damage;
        }

        public void HeavyAttackDamage()
        {
            rightHandDamageCollider.currentWeaponDamage = attackingWeapon.heavydamage;
        }

        public void LightAttack1Damage_2H()
        {
            rightHandDamageCollider.currentWeaponDamage = Mathf.RoundToInt(attackingWeapon.light1damage * 1.5f);
        }

        public void LightAttack2Damage_2H()
        {
            rightHandDamageCollider.currentWeaponDamage = Mathf.RoundToInt(attackingWeapon.light2damage * 1.5f);
        }

        public void HeavyAttackDamage_2H()
        {
            rightHandDamageCollider.currentWeaponDamage = Mathf.RoundToInt(attackingWeapon.heavydamage * 1.5f);
        }

        public void SkillDamage()
        {
            rightHandDamageCollider.currentWeaponDamage = attackingWeapon.skilldamage;
        }
        #endregion

        #region Handle Weapon's Stamina Drain
        public void DrainStaminaLightAttack()
        {
            playerStats.TakeStaminaDrain(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
        }

        public void DrainStaminaBlock()
        {
            playerStats.TakeStaminaDrain(30);
        }

        public void DrainStamina2HLightAttack()
        {
            playerStats.TakeStaminaDrain(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier * 1.5f));
        }

        public void DrainStaminaHeavyAttack()
        {
            playerStats.TakeStaminaDrain(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
        }

        public void DrainStamina2HHeavyAttack()
        {
            playerStats.TakeStaminaDrain(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier * 1.5f));
        }
        #endregion

    }

}
