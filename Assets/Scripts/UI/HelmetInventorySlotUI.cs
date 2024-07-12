using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class HelmetInventorySlotUI : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        UIManager uiManager;
        PlayerEquipmentManager playerEquipmentManager;

        public Image icon;
        HelmetEquipment helmet;

        private void Awake()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            playerEquipmentManager = FindObjectOfType<PlayerEquipmentManager>();
            uiManager = FindObjectOfType<UIManager>();
            inputHandler = FindObjectOfType<InputHandler>();

        }
        public void AddItem(HelmetEquipment newHelmet)
        {
            helmet = newHelmet;
            icon.sprite = helmet.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            helmet = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void EquipThisItem()
        {
            if (uiManager.helmetSlotSelected)
            {
                if (uiManager.playerInventory.currentHelmetEquipment != null)
                {
                    uiManager.playerInventory.helmetInventory.Add(uiManager.playerInventory.currentHelmetEquipment);
                }

                uiManager.playerInventory.currentHelmetEquipment = helmet;
                uiManager.playerInventory.helmetInventory.Remove(helmet);
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

