using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS {
    public class SpellPickUp : Interactable
    {
        public SpellItem spell;
        UIManager uiManager;
        QuickSlotsUI quickSlotsUI;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
        }

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpItem(playerManager);
            uiManager.UpdateUI();
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            PlayerAnimatorManager animatorHandler;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren<PlayerAnimatorManager>();

            playerLocomotion.rigidbody.velocity = Vector3.zero;
            animatorHandler.PlayTargetAnimation("PickUp", true);
            playerInventory.spellsInventory.Add(spell);
            //
            PlayerStats playerStats;
            SoulCount soulCount;

            playerStats = playerManager.GetComponent<PlayerStats>();
            soulCount = FindObjectOfType<SoulCount>();

            playerStats.AddSouls(5000);
            soulCount.SetCurrentSoul(playerStats.soulCount);
            //
            if (playerInventory.spellItems[0] == null)
            {
                playerInventory.spellItems[0] = spell;
            }
            else if(playerInventory.spellItems[0] != null && playerInventory.spellItems[1] == null)
            {
                playerInventory.spellItems[1] = spell;
            }
            playerInventory.currentSpell = spell;
            quickSlotsUI.UpdateSpellUI(playerInventory.currentSpell);

            playerManager.itemInteratableGameObject.GetComponentInChildren<Text>().text = spell.itemName;
            playerManager.itemInteratableGameObject.GetComponentInChildren<RawImage>().texture = spell.itemIcon.texture;
            playerManager.itemInteratableGameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}

