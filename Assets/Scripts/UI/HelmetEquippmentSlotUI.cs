using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class HelmetEquippmentSlotUI : MonoBehaviour
    {
        UIManager uiManager;
        public Image icon;
        HelmetEquipment helmet;



        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(HelmetEquipment newHelmet)
        {
            helmet = newHelmet;
            icon.sprite = helmet.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            helmet = null;
            icon.sprite = null;
            icon.enabled = false;
          //  gameObject.SetActive(false);
        }

        public void SelectThisSlot()
        {
            uiManager.helmetSlotSelected = true;
            uiManager.UpdateUI();
        }
    }
}



