using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS {
    public class UIManager : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        public EquipmentWindowUI equipmentWindowUI;
        public InputHandler inputHandler;

        [Header("UI Windows")]
        public GameObject selectWindow;
        public GameObject HUD;
       // public GameObject equipmentScreenWindow;
        public GameObject weaponInventoryWindow;
        public GameObject helmetInventoryWindow;
        public GameObject handInventoryWindow;
        public GameObject legInventoryWindow;
        public GameObject torsoInventoryWindow;
        public GameObject levelWindow;
        public GameObject equipmentWindow;


        [Header("PopUps")]
        public GameObject die;
        public GameObject enemyFelled;
        public GameObject bonfireLit;
        public CanvasGroup dieCanvas;
        public CanvasGroup enemyFelledCanvas;
        public CanvasGroup bonfireLitCanvas;

        public BGMController bgmController;

        [Header("Equipment Window Slot Selected")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;
        public bool helmetSlotSelected;
        public bool torsoSlotSelected;
        public bool handSlotSelected;
        public bool legSlotSelected;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotsParent;
        WeaponInventorySlotUI[] weaponInventorySlots;

        [Header("Helmet Inventory")]
        public GameObject helmetInventorySlotPrefab;
        public Transform helmetInventorySlotsParent;
        HelmetInventorySlotUI[] helmetInventorySlots;

        [Header("Torso Inventory")]
        public GameObject torsoInventorySlotPrefab;
        public Transform torsoInventorySlotsParent;
        TorsoInventorySlotUI[] torsoInventorySlots;

        [Header("Hand Inventory")]
        public GameObject handInventorySlotPrefab;
        public Transform handInventorySlotsParent;
        HandInventorySlotUI[] handInventorySlots;

        [Header("Leg Inventory")]
        public GameObject legInventorySlotPrefab;
        public Transform legInventorySlotsParent;
        LegInventorySlotUI[] legInventorySlots;

        public Text currentConsumableItemAmount;

        private void Awake()
        {
            weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlotUI>();

            helmetInventorySlots = helmetInventorySlotsParent.GetComponentsInChildren<HelmetInventorySlotUI>();
            torsoInventorySlots = torsoInventorySlotsParent.GetComponentsInChildren<TorsoInventorySlotUI>();
            handInventorySlots = handInventorySlotsParent.GetComponentsInChildren<HandInventorySlotUI>();
            legInventorySlots = legInventorySlotsParent.GetComponentsInChildren<LegInventorySlotUI>();
            bgmController = FindObjectOfType<BGMController>();
        }

        private void Start()
        {
            UpdateUI();

            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);

            equipmentWindowUI.LoadArmorOnEquipmentScren(playerInventory);

            currentConsumableItemAmount.text = playerInventory.currentConsumableItem.currentItemAmount.ToString();

           
        }

        private void Update()
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            #region WeaponInventorySlots
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerInventory.weaponsInventory.Count)
                {
                    if (weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlotUI>();
                    }
                    weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }

            #endregion

            #region EquipmentInventorySlots
            for (int i = 0; i < helmetInventorySlots.Length; i++)
            {
                if (i < playerInventory.helmetInventory.Count)
                {
                    if (helmetInventorySlots.Length < playerInventory.helmetInventory.Count)
                    {
                        Instantiate(helmetInventorySlotPrefab, helmetInventorySlotsParent);
                        helmetInventorySlots = helmetInventorySlotsParent.GetComponentsInChildren<HelmetInventorySlotUI>();
                    }
                    helmetInventorySlots[i].AddItem(playerInventory.helmetInventory[i]);
                }
                else
                {
                    helmetInventorySlots[i].ClearInventorySlot();
                }
            }

            for (int i = 0; i < torsoInventorySlots.Length; i++)
            {
                if (i < playerInventory.torsoInventory.Count)
                {
                    if (torsoInventorySlots.Length < playerInventory.torsoInventory.Count)
                    {
                        Instantiate(torsoInventorySlotPrefab, torsoInventorySlotsParent);
                        torsoInventorySlots = torsoInventorySlotsParent.GetComponentsInChildren<TorsoInventorySlotUI>();
                    }
                    torsoInventorySlots[i].AddItem(playerInventory.torsoInventory[i]);
                }
                else
                {
                    torsoInventorySlots[i].ClearInventorySlot();
                }
            }

            for (int i = 0; i < handInventorySlots.Length; i++)
            {
                if (i < playerInventory.handInventory.Count)
                {
                    if (handInventorySlots.Length < playerInventory.handInventory.Count)
                    {
                        Instantiate(handInventorySlotPrefab, handInventorySlotsParent);
                        handInventorySlots = handInventorySlotsParent.GetComponentsInChildren<HandInventorySlotUI>();
                    }
                    handInventorySlots[i].AddItem(playerInventory.handInventory[i]);
                }
                else
                {
                   handInventorySlots[i].ClearInventorySlot();
                }
            }

            for (int i = 0; i < legInventorySlots.Length; i++)
            {
                if (i < playerInventory.legInventory.Count)
                {
                    if (legInventorySlots.Length < playerInventory.legInventory.Count)
                    {
                        Instantiate(legInventorySlotPrefab, legInventorySlotsParent);
                        legInventorySlots = legInventorySlotsParent.GetComponentsInChildren<LegInventorySlotUI>();
                    }
                    legInventorySlots[i].AddItem(playerInventory.legInventory[i]);
                }
                else
                {
                    legInventorySlots[i].ClearInventorySlot();
                }
            }

            
            #endregion
        }

        

        

        public void inventoryCheck() 
        {
            if (weaponInventoryWindow.activeSelf == true ||
                equipmentWindow.activeSelf == true ||
                levelWindow.activeSelf == true ||
                helmetInventoryWindow.activeSelf == true ||
                handInventoryWindow.activeSelf == true ||
                torsoInventoryWindow.activeSelf == true ||
                legInventoryWindow.activeSelf == true)
            {
                inputHandler.windowFlag = true;
            }
            else
            {
                inputHandler.windowFlag = false;
            }
        }
        public void OpenMenu()
        {
            selectWindow.SetActive(true);
        }

        public void CloseMenu()
        {
            selectWindow.SetActive(false);
        }

        public void OpenHUD()
        {
            HUD.SetActive(true);
        }

        public void CloseHUD()
        {
            HUD.SetActive(false);
        }

        public void Died()
        {
            StartCoroutine(FadeInDied());
        }

        IEnumerator FadeInDied()
        {
            die.SetActive(true);
            HUD.SetActive(false);
            bgmController.PlayDiedSound();
            for (float fade = 0.05f; fade < 1; fade = fade + 0.05f)
            {
                dieCanvas.alpha = fade;
            }

            yield return new WaitForSeconds(0.05f);
        }

        public void EnemyFelledUI()
        {
            StartCoroutine(FadeInEnemyFelled());
        }

        IEnumerator FadeInEnemyFelled()
        {
            enemyFelled.SetActive(true);
            bgmController.PlayEnemyFelledSound();

            for (float fade = 0.05f; fade < 1; fade = fade + 0.05f)
            {
                enemyFelledCanvas.alpha = fade;
                if (fade > 0.9f)
                {
                    StartCoroutine(FadeOutEnemyFelled());
                }
                yield return new WaitForSeconds(0.05f);
            }
            
        }

        IEnumerator FadeOutEnemyFelled()
        {
            yield return new WaitForSeconds(2f);
            for (float fade = 1f; fade > 0; fade = fade - 0.05f)
            {
                enemyFelledCanvas.alpha = fade;
                if (fade <= 0.05f)
                {
                    enemyFelled.SetActive(false);
                }

                yield return new WaitForSeconds(0.05f);
            }
            
        }

        public void BonfireLitUI()
        {
            StartCoroutine(FadeInBonfireLit());
        }

        IEnumerator FadeInBonfireLit()
        {
            bonfireLit.SetActive(true);

            for (float fade = 0.05f; fade < 1f; fade = fade + 0.05f)
            {
                bonfireLitCanvas.alpha = fade;
                if (fade > 0.9f)
                {
                    StartCoroutine(FadeOutBonfireLit());
                }

                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator FadeOutBonfireLit()
        {
            yield return new WaitForSeconds(2f);
            for (float fade = 1f; fade > 0; fade = fade - 0.05f)
            {
                bonfireLitCanvas.alpha = fade;
                if (fade <= 0.05f)
                {
                    bonfireLit.SetActive(false);
                }

                yield return new WaitForSeconds(0.05f);
            }
            
        }

        public void ResetAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;

            helmetSlotSelected = false;
            handSlotSelected = false;
            torsoSlotSelected = false;
            legSlotSelected = false;
        }
    }

}
