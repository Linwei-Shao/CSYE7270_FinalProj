using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class HandEquippmentSlotUI : MonoBehaviour
    {
        UIManager uiManager;
        public Image icon;
        HandEquipment hand;



        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(HandEquipment newHand)
        {
            hand = newHand;
            icon.sprite = hand.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            hand = null;
            icon.sprite = null;
            icon.enabled = false;
         //   gameObject.SetActive(false);
        }

        public void SelectThisSlot()
        {
            uiManager.handSlotSelected = true;
            uiManager.UpdateUI();
        }
    }
}



