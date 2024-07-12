using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class PlayerEffectsManager : MonoBehaviour
    {
        public GameObject bloodFX;

        public GameObject fireFX;
        public GameObject acidFX;
        public GameObject brokenFX;

        PlayerStats playerStats;
        WeaponSlotManager weaponSlotManager;

        public GameObject currentFX;
        public GameObject instantiatedFXModel;

        public GameObject healFX;

        public int amountToBeHealed;
        public int amountToBeRecovered;

        public bool isDrinking;

        PlayerLocomotion playerLocomotion;
        InputHandler inputHandler;

        private void Awake()
        {
            playerStats = GetComponentInParent<PlayerStats>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            inputHandler = GetComponentInParent<InputHandler>();
        }

        public void PlayBloodFX(Vector3 bloodLocation)
        {
            GameObject blood = Instantiate(bloodFX, bloodLocation, Quaternion.identity);
        }

        public void PlayFireFX(Vector3 fireLocation)
        {
            GameObject fire = Instantiate(fireFX, fireLocation, Quaternion.identity);
        }

        public void PlayAcidFX(Vector3 acidLocation)
        {
            GameObject acid = Instantiate(acidFX, acidLocation, Quaternion.identity);
        }

        public void PlayBrokenFX(Vector3 brokenLocation)
        {
            GameObject broken = Instantiate(brokenFX, brokenLocation, Quaternion.identity);
        }

        public void HealPlayerEffect()
        {
            if (amountToBeHealed > 0)
            {
                playerStats.HealthRecoverPlayer(amountToBeHealed);
                healFX = Instantiate(currentFX, weaponSlotManager.backSlot.transform);
                Destroy(instantiatedFXModel.gameObject);
                weaponSlotManager.LoadBothWeapon();
                
            }
            else
            {
                playerStats.ManaRecoverPlayer(amountToBeRecovered);
                healFX = Instantiate(currentFX, weaponSlotManager.backSlot.transform);
                Destroy(instantiatedFXModel.gameObject);
                weaponSlotManager.LoadBothWeapon();
            }
            
        }

        public void SetWalkingMovementSpeed()
        {
            inputHandler.walkingFlag = true;
        }

        public void ResetWalkingMovementSpeed()
        {
            inputHandler.walkingFlag = false;
        }

        public void DestroyEffect()
        {
            Destroy(healFX);
            inputHandler.walkingFlag = false;
            
        }

        public void DestoryModel()
        {
            Destroy(instantiatedFXModel.gameObject);
            weaponSlotManager.LoadBothWeapon();
        }

        public void AllowDrinking()
        {
            isDrinking = false;
        }


    }
}
