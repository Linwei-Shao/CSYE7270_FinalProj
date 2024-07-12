using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class LegInventorySlotUI : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        UIManager uiManager;
        PlayerEquipmentManager playerEquipmentManager;

        public Image icon;
        LegEquipment leg;

        private void Awake()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            playerEquipmentManager = FindObjectOfType<PlayerEquipmentManager>();
            uiManager = FindObjectOfType<UIManager>();
            inputHandler = FindObjectOfType<InputHandler>();

        }
        public void AddItem(LegEquipment newLeg)
        {
            leg = newLeg;
            icon.sprite = leg.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            leg = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void EquipThisItem()
        {
            if (uiManager.legSlotSelected)
            {
                if (uiManager.playerInventory.currentLegEquipment != null)
                {
                    uiManager.playerInventory.legInventory.Add(uiManager.playerInventory.currentLegEquipment);
                }

                uiManager.playerInventory.currentLegEquipment = leg;
                uiManager.playerInventory.legInventory.Remove(leg);
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

