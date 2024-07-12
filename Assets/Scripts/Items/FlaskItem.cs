using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "Items/Consumables/Flask")]
    public class FlaskItem : ConsumableItem
    {
        [Header("Flask Type")]
        public bool estusFlask;
        public bool ashenFlask;

        [Header("Recovery Amount")]
        public int healthRecoverAmount;
        public int manaRecoverAmount;

        [Header("Recovery FX")]
        public GameObject recoveryFX;

        public PlayerLocomotion playerLocomotion;

        private void Start()
        {
            playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        }

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager,
            WeaponSlotManager weaponSlotManager,
            PlayerEffectsManager playerEffectsManager)
        {
            base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerEffectsManager);

            
            
            GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
            playerEffectsManager.currentFX = recoveryFX;
            playerEffectsManager.amountToBeHealed = healthRecoverAmount;
            playerEffectsManager.amountToBeRecovered = manaRecoverAmount;
            playerEffectsManager.instantiatedFXModel = flask;
            weaponSlotManager.rightHandSlot.UnloadWeapon();
            
        }
    }
}

