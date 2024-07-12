using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class TorsoEquippmentSlotUI : MonoBehaviour
    {
        UIManager uiManager;
        public Image icon;
        TorsoEquipment torso;



        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(TorsoEquipment newTorso)
        {
            torso = newTorso;
            icon.sprite = torso.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            torso = null;
            icon.sprite = null;
            icon.enabled = false;
           // gameObject.SetActive(false);
        }

        public void SelectThisSlot()
        {
            uiManager.torsoSlotSelected = true;
            uiManager.UpdateUI();
        }
    }
}



