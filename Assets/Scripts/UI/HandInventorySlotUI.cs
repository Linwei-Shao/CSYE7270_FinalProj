using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class HandInventorySlotUI : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        UIManager uiManager;
        PlayerEquipmentManager playerEquipmentManager;

        public Image icon;
        HandEquipment hand;

        private void Awake()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            playerEquipmentManager = FindObjectOfType<PlayerEquipmentManager>();
            uiManager = FindObjectOfType<UIManager>();
            inputHandler = FindObjectOfType<InputHandler>();

        }
        public void AddItem(HandEquipment newHand)
        {
            hand = newHand;
            icon.sprite = hand.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            hand = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void EquipThisItem()
        {
            if (uiManager.handSlotSelected)
            {
                if (uiManager.playerInventory.currentHandEquipment != null)
                {
                    uiManager.playerInventory.handInventory.Add(uiManager.playerInventory.currentHandEquipment);
                }

                uiManager.playerInventory.currentHandEquipment = hand;
                uiManager.playerInventory.handInventory.Remove(hand);
                playerEquipmentManager.EquipAllEquipments();

            }
            else
            {
                return;
            }

            uiManager.equipmentWindowUI.LoadArmorOnEquipmentScren(uiManager.playerInventory);
            uiManager.UpdateUI();
            uiManager.ResetAllSelectedSlots();
            inputHandler.menuFlag = false;
        }

    }
}

