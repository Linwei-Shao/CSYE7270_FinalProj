using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS {
    public class EquipmentPickUp : Interactable
    {
        public HelmetEquipment helmet;
        public TorsoEquipment torso;
        public HandEquipment hand;
        public LegEquipment leg;


        UIManager uiManager;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpItem(playerManager);
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

            if (helmet != null)
            {
                playerInventory.helmetInventory.Add(helmet);
                playerManager.itemInteratableGameObject.GetComponentInChildren<Text>().text = helmet.itemName;
                playerManager.itemInteratableGameObject.GetComponentInChildren<RawImage>().texture = helmet.itemIcon.texture;
            }
            else if (torso != null)
            {
                playerInventory.torsoInventory.Add(torso);
                playerManager.itemInteratableGameObject.GetComponentInChildren<Text>().text = torso.itemName;
                playerManager.itemInteratableGameObject.GetComponentInChildren<RawImage>().texture = torso.itemIcon.texture;
            }
            else if (hand != null)
            {
                playerInventory.handInventory.Add(hand);
                playerManager.itemInteratableGameObject.GetComponentInChildren<Text>().text = hand.itemName;
                playerManager.itemInteratableGameObject.GetComponentInChildren<RawImage>().texture = hand.itemIcon.texture;
            }
            else if (leg != null)
            {
                playerInventory.legInventory.Add(leg);
                playerManager.itemInteratableGameObject.GetComponentInChildren<Text>().text = leg.itemName;
                playerManager.itemInteratableGameObject.GetComponentInChildren<RawImage>().texture = leg.itemIcon.texture;
            }

            //
            PlayerStats playerStats;
            SoulCount soulCount;

            playerStats = playerManager.GetComponent<PlayerStats>();
            soulCount = FindObjectOfType<SoulCount>();

            playerStats.AddSouls(5000);
            soulCount.SetCurrentSoul(playerStats.soulCount);
            //
           
            playerManager.itemInteratableGameObject.SetActive(true);

            Destroy(gameObject);
        }
    }
}

