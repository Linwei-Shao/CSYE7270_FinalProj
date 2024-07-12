using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS 
{
    public class PlayerAttacker : MonoBehaviour
    {
        PlayerAnimatorManager animatorHandler;
        PlayerEquipmentManager playerEquipmentManager;
        InputHandler inputHandler;
        PlayerManager playerManager;
        WeaponSlotManager weaponSlotManager;
        PlayerInventory playerInventory;
        PlayerStats playerStats;
        CameraHandler cameraHandler;

        public EnemyStats enemyStats;
        public EnemyAnimatorManager enemyAnimatorManager;

        public string lastAttack;


        LayerMask backStabLayer = 1 << 12;
        LayerMask riposteLayer = 1 << 13;

        private void Awake()
        {
            playerStats = GetComponentInParent<PlayerStats>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            animatorHandler = GetComponent<PlayerAnimatorManager>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
            inputHandler = GetComponentInParent<InputHandler>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OH_Light_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                }
                else if(lastAttack == weapon.TH_Light_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_2, true);
                }
            }
        }
        public void HandleLightAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;

            if (inputHandler.twoHandFlag)
            {
                
                if (playerStats.currentStamina >= Mathf.RoundToInt(weapon.baseStamina * weapon.lightAttackMultiplier))
                {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_1, true);
                    lastAttack = weapon.TH_Light_Attack_1;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (playerStats.currentStamina >= Mathf.RoundToInt(weapon.baseStamina * weapon.lightAttackMultiplier))
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
                    lastAttack = weapon.OH_Light_Attack_1;
                }
                else
                {
                    return;
                }
            }
            
                
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;

            if (inputHandler.twoHandFlag)
            {
                if (playerStats.currentStamina >= Mathf.RoundToInt(weapon.baseStamina * weapon.heavyAttackMultiplier))
                {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Heavy_Attack_1, true);
                    lastAttack = weapon.TH_Heavy_Attack_1;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (playerStats.currentStamina >= Mathf.RoundToInt(weapon.baseStamina * weapon.heavyAttackMultiplier))
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
                    lastAttack = weapon.OH_Heavy_Attack_1;
                }
                else
                {
                    return;
                }
            }
            
        }

        public void HandleSkill(WeaponItem weapon)
        {
            if (playerStats.currentMana >= weapon.manaCost)
            {
                playerStats.TakeManaCost(weapon.manaCost);
                animatorHandler.PlayTargetAnimation(weapon.Skill_1, true);
            }
            else 
            {
                animatorHandler.PlayTargetAnimation("Shrug", true);
            }
        }

        public void HandleAttackAction()
        {
            if (playerInventory.rightWeapon.isMeleeWeapon)
            {
                PerformMeleeAction();
            }
            else if (playerInventory.rightWeapon.isSpellCaster || playerInventory.rightWeapon.isFaithCaster || playerInventory.rightWeapon.isPyroCaster)
            {
                PerformMagicAction(playerInventory.rightWeapon);
            }



        }

        public void HandleBlockAction()
        {
            PerformBlockAction();
        }

        public void HandleParryAction()
        {
            if (playerInventory.leftWeapon.isShield)
            {
                PerformShieldAction(inputHandler.twoHandFlag);
            }
            else if (playerInventory.leftWeapon.isMeleeWeapon)
            { 
            
            }
        }

        #region Attack Actions
        private void PerformMeleeAction()
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                    return;
                if (playerManager.canDoCombo)
                    return;

                animatorHandler.anim.SetBool("isUsingRightHand", true);
                HandleLightAttack(playerInventory.rightWeapon);
            }
        }

        private void PerformShieldAction(bool isTwoHand)
        {
            if (playerManager.isInteracting)
                return;

            if (isTwoHand)
            {
                animatorHandler.PlayTargetAnimation(playerInventory.rightWeapon.Skill_1, true);
            }
            else
            {
                animatorHandler.PlayTargetAnimation(playerInventory.leftWeapon.Skill_1, true);
            }
        }

        private void PerformBlockAction()
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.isBlocking)
                return;

            if (inputHandler.twoHandFlag)
            {
                animatorHandler.PlayTargetAnimation("Block2H_Start", false, true);
            }
            else
            {
                animatorHandler.PlayTargetAnimation("Block_Start", false, true);
            }

            playerEquipmentManager.OpenBlockingCollider();
            playerManager.isBlocking = true;
        }

        private void PerformMagicAction(WeaponItem weapon)
        {
            if (playerManager.isInteracting)
                return;

            if (weapon.isSpellCaster)
            {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.isMagicSpell)
                {

                }
            }
            else if (weapon.isFaithCaster || weapon.isPyroCaster)
            {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
                {
                    if (playerStats.currentStamina >= playerInventory.currentSpell.staminaCost)
                    {
                        if (playerStats.currentMana >= playerInventory.currentSpell.manaCost)
                        {
                            playerStats.TakeManaCost(playerInventory.currentSpell.manaCost);
                            playerStats.TakeStaminaDrain(playerInventory.currentSpell.staminaCost);
                            playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
                        }
                        else
                        {
                            animatorHandler.PlayTargetAnimation("Shrug", true);
                        }
                    }
                    
                }
                else if(playerInventory.currentSpell != null && playerInventory.currentSpell.isPyroSpell)
                {
                    if (playerStats.currentStamina >= playerInventory.currentSpell.staminaCost)
                    {
                        if (playerStats.currentMana >= playerInventory.currentSpell.manaCost)
                        {
                            playerStats.TakeManaCost(playerInventory.currentSpell.manaCost);
                            playerStats.TakeStaminaDrain(playerInventory.currentSpell.staminaCost);
                            playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
                        }
                        else
                        {
                            animatorHandler.PlayTargetAnimation("Shrug", true);
                        }
                    }
                }


            }
            
                
            
        }

        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCasted(animatorHandler, playerStats, cameraHandler, weaponSlotManager);

        }

        private void DestroyWarmUpEffect()
        {
            playerInventory.currentSpell.DestroyWarmUpEffect(); ;
        }

        private void DestroySpellEffect()
        {
            playerInventory.currentSpell.DestroySpellEffect();
        }
        #endregion

        public void AttemptBackStabOrRiposte()
        {
            RaycastHit hit;

            if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position,
                transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = weaponSlotManager.rightHandDamageCollider;

                if (enemyCharacterManager != null)
                {
                    if (enemyCharacterManager.GetComponent<EnemyStats>().isDead)
                        return;

                    playerManager.transform.position = enemyCharacterManager.backStabCollider.criticalDamageStandPoint.position;
                    Vector3 rotationDir = hit.transform.position - playerManager.transform.position;
                    rotationDir.y = 0;
                    rotationDir.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDir);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;



                    int criticalDamage = playerInventory.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    animatorHandler.PlayTargetAnimation("Back Stab", true);
                    enemyCharacterManager.GetComponentInChildren<EnemyAnimatorManager>().PlayTargetAnimationRoot("Back Stabbed", true);

                }
            }
            else if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position,
                transform.TransformDirection(Vector3.forward), out hit, 0.5f, riposteLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = weaponSlotManager.rightHandDamageCollider;

                if (enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
                {
                    if (enemyCharacterManager.GetComponent<EnemyStats>().isDead)
                        return;

                    playerManager.transform.position = enemyCharacterManager.riposteCollider.criticalDamageStandPoint.position;

                    Vector3 rotationDir = hit.transform.position - playerManager.transform.position;
                    rotationDir.y = 0;
                    rotationDir.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDir);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;



                    int criticalDamage = playerInventory.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    animatorHandler.PlayTargetAnimation("Riposte", true);
                    enemyCharacterManager.GetComponentInChildren<EnemyAnimatorManager>().PlayTargetAnimation("Riposted", true);
                }
            }
        }
    }
}

