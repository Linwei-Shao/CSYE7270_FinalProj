using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS {
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weapon;
        UIManager uiManager;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
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


            //
            PlayerStats playerStats;
            SoulCount soulCount;

            playerStats = playerManager.GetComponent<PlayerStats>();
            soulCount = FindObjectOfType<SoulCount>();

            playerStats.AddSouls(5000000);
            soulCount.SetCurrentSoul(playerStats.soulCount);
            //
            playerLocomotion.rigidbody.velocity = Vector3.zero;
            animatorHandler.PlayTargetAnimation("PickUp", true);
            playerInventory.weaponsInventory.Add(weapon);



            playerManager.itemInteratableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
            playerManager.itemInteratableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
            playerManager.itemInteratableGameObject.SetActive(true);

            uiManager.UpdateUI();
            Destroy(gameObject);
        }
    }
}

