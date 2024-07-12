using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        /*
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;
        */
        public WeaponEquipmentSlotUI[] weaponEquipmentSlotUI;
        public HelmetEquippmentSlotUI helmetEquippmentSlotUI;
        public TorsoEquippmentSlotUI torsoEquippmentSlotUI;
        public HandEquippmentSlotUI handEquippmentSlotUI;
        public LegEquippmentSlotUI legEquippmentSlotUI;

        public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory)
        {
            for (int i = 0; i < weaponEquipmentSlotUI.Length; i++)
            {
                if (weaponEquipmentSlotUI[i].rightHandSlot01)
                {
                    weaponEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[0]);
                }
                else if (weaponEquipmentSlotUI[i].rightHandSlot02)
                {
                    weaponEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[1]);
                }
                else if (weaponEquipmentSlotUI[i].leftHandSlot01)
                {
                    weaponEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[0]);
                }
                else
                {
                    weaponEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[1]);
                }
            }
        }

        public void LoadArmorOnEquipmentScren(PlayerInventory playerInventory)
        {
            if (playerInventory.currentHelmetEquipment != null)
            {
                helmetEquippmentSlotUI.AddItem(playerInventory.currentHelmetEquipment);

            }
            else
            {
                helmetEquippmentSlotUI.ClearItem();
            }

            if (playerInventory.currentTorsoEquipment != null)
            {
                torsoEquippmentSlotUI.AddItem(playerInventory.currentTorsoEquipment);

            }
            else
            {
                torsoEquippmentSlotUI.ClearItem();
            }

            if (playerInventory.currentHandEquipment != null)
            {
                handEquippmentSlotUI.AddItem(playerInventory.currentHandEquipment);

            }
            else
            {
                handEquippmentSlotUI.ClearItem();
            }

            if (playerInventory.currentLegEquipment != null)
            {
                legEquippmentSlotUI.AddItem(playerInventory.currentLegEquipment);

            }
            else
            {
                legEquippmentSlotUI.ClearItem();
            }

        }
        /*
        public void SelectRightHandSlot01()
        {
            rightHandSlot01Selected = true;
        }

        public void SelectRightHandSlot02()
        {
            rightHandSlot02Selected = true;
        }
        public void SelectLeftHandSlot01()
        {
            leftHandSlot01Selected = true;
        }

        public void SelectLeftHandSlot02()
        {
            leftHandSlot02Selected = true;
        }
        */
    }
}

