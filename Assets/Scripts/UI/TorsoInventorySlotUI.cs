using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class TorsoInventorySlotUI : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        UIManager uiManager;
        PlayerEquipmentManager playerEquipmentManager;

        public Image icon;
        TorsoEquipment torso;

        private void Awake()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            playerEquipmentManager = FindObjectOfType<PlayerEquipmentManager>();
            uiManager = FindObjectOfType<UIManager>();
            inputHandler = FindObjectOfType<InputHandler>();

        }
        public void AddItem(TorsoEquipment newTorso)
        {
            torso = newTorso;
            icon.sprite = torso.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            torso = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void EquipThisItem()
        {
            if (uiManager.torsoSlotSelected)
            {
                if (uiManager.playerInventory.currentTorsoEquipment != null)
                {
                    uiManager.playerInventory.torsoInventory.Add(uiManager.playerInventory.currentTorsoEquipment);
                }

                uiManager.playerInventory.currentTorsoEquipment = torso;
                uiManager.playerInventory.torsoInventory.Remove(torso);
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

