using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class ConsumableItem : Item
    {
        [Header("Item Quantity")]
        public int maxItemAmount;
        public int currentItemAmount;

        [Header("Item Model")]
        public GameObject itemModel;

        [Header("Animations")]
        public string consumeAniamtion;
        public bool isInteracting;

        public virtual void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, 
            WeaponSlotManager weaponSlotManager, 
            PlayerEffectsManager playerEffectsManager)
        {
            if (currentItemAmount > 0)
            {
                UIManager uiManager = FindObjectOfType<UIManager>();
                PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();

                currentItemAmount -= 1;
                uiManager.currentConsumableItemAmount.text = playerInventory.currentConsumableItem.currentItemAmount.ToString();
                playerAnimatorManager.PlayTargetAnimation(consumeAniamtion, isInteracting, true);
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation("Shrug", true);
            }
        }
    }
}
