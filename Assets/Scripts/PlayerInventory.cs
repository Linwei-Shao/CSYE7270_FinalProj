using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS {
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;
        QuickSlotsUI quickSlotsUI;
        UIManager uiManager;

        [Header("Quick Slot Items")]
        public SpellItem currentSpell;
        public ConsumableItem currentConsumableItem;
        
        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;
        public WeaponItem unarmedWeapon;

        [Header("Current Equipment")]
        public HelmetEquipment currentHelmetEquipment;
        public TorsoEquipment currentTorsoEquipment;
        public LegEquipment currentLegEquipment;
        public HandEquipment currentHandEquipment;

        public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[1];
        public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[1];

        public SpellItem[] spellItems = new SpellItem[1];
        public ConsumableItem[] consumableItems = new ConsumableItem[1];

        public int currentRightWeaponIndex = 0;
        public int currentLeftWeaponIndex = 0;
        public int currentSpellIndex = 0;
        public int currentItemIndex = 0;

        public List<WeaponItem> weaponsInventory;
        public List<SpellItem> spellsInventory;
        public List<ConsumableItem> itemsInventory;

        public List<HelmetEquipment> helmetInventory;
        public List<HandEquipment> handInventory;
        public List<TorsoEquipment> torsoInventory;
        public List<LegEquipment> legInventory;

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            uiManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            rightWeapon = weaponsInRightHandSlots[0];
            leftWeapon = weaponsInLeftHandSlots[0];
            currentSpell = spellItems[0];
            currentConsumableItem = consumableItems[0];
            weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
            currentRightWeaponIndex = 0;
            currentLeftWeaponIndex = 0;
            currentSpellIndex = 0;
            currentItemIndex = 0;
        }

        public void ChangeCurrentSpell()
        {
            SpellItem spell = null;

            if (currentSpellIndex == spellItems.Length - 1)
            {
                currentSpellIndex = 0;

                if (spellItems[currentSpellIndex] == null)
                    return;

                spell = spellItems[currentSpellIndex];
            }
            else
            {
                while (currentSpellIndex < spellItems.Length - 1 && spell == null)
                {
                    currentSpellIndex += 1;

                    if (spellItems[currentSpellIndex] == null)
                        return;

                    spell = spellItems[currentSpellIndex];
                }
            }
            
            
            if (spell != null)
            {
                currentSpell = spell;
                quickSlotsUI.UpdateSpellUI(currentSpell);
            }
        }

        public void ChangeCurrentItem()
        {
            ConsumableItem item = null;
            if (currentItemIndex == consumableItems.Length - 1)
            {
                currentItemIndex = 0;
                item = consumableItems[currentItemIndex];
            }
            else
            {
                while (currentItemIndex < consumableItems.Length - 1 && item == null)
                {
                    currentItemIndex += 1;
                    item = consumableItems[currentItemIndex];
                }
            }

            if (item != null)
            {
                currentConsumableItem = item;
                quickSlotsUI.UpdateItemUI(currentConsumableItem);
                uiManager.currentConsumableItemAmount.text = currentConsumableItem.currentItemAmount.ToString();
            }
        }

        public void ChangeRightWeapon()
        {
            WeaponItem weapon = null;
            while (currentRightWeaponIndex < weaponsInRightHandSlots.Length - 1 && weapon == null)
            {
                currentRightWeaponIndex += 1;
                weapon = weaponsInRightHandSlots[currentRightWeaponIndex];
            }
            if (weapon != null)
            {
                rightWeapon = weapon;
                weaponSlotManager.LoadWeaponOnSlot(weapon, false);
            }
            else
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }
        }


        public void ChangeLeftWeapon() 
        {
            WeaponItem weapon = null;
            while (currentLeftWeaponIndex < weaponsInLeftHandSlots.Length - 1 && weapon == null)
            {
                currentLeftWeaponIndex += 1;
                weapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
            }
            if (weapon != null)
            {
                leftWeapon = weapon;
                weaponSlotManager.LoadWeaponOnSlot(weapon, true);
            }
            else
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);
            }
        }
    }

}
