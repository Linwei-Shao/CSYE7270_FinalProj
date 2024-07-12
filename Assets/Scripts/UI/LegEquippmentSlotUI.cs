using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class LegEquippmentSlotUI : MonoBehaviour
    {
        UIManager uiManager;
        public Image icon;
        LegEquipment leg;



        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(LegEquipment newLeg)
        {
            leg = newLeg;
            icon.sprite = leg.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            leg = null;
            icon.sprite = null;
            icon.enabled = false;
          //  gameObject.SetActive(false);
        }

        public void SelectThisSlot()
        {
            uiManager.legSlotSelected = true;
            uiManager.UpdateUI();
        }
    }
}



