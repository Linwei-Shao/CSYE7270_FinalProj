using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class QuickSlotsUI : MonoBehaviour
    {
        public Image leftWeaponIcon;
        public Image rightWeaponIcon;
        public Image spellIcon;
        public Image itemIcon;

        public void UpdateSpellUI(SpellItem spell)
        {
            spellIcon.sprite = spell.itemIcon;
            spellIcon.enabled = true;
        }

        public void UpdateItemUI(ConsumableItem item)
        {
            itemIcon.sprite = item.itemIcon;
            itemIcon.enabled = true;
        }
        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
        {

            if (isLeft == false)
            {
                if (weapon.itemIcon != null)
                {
                    rightWeaponIcon.sprite = weapon.itemIcon;
                    rightWeaponIcon.enabled = true;
                }
                else 
                {
                    rightWeaponIcon.sprite = null;
                    rightWeaponIcon.enabled = false;
                }
            }
            else 
            {
                if (weapon.itemIcon != null)
                {
                    leftWeaponIcon.sprite = weapon.itemIcon;
                    leftWeaponIcon.enabled = true;
                }
                else
                {
                    leftWeaponIcon.sprite = null;
                    leftWeaponIcon.enabled = false;
                }
            }
        }
    }

}
