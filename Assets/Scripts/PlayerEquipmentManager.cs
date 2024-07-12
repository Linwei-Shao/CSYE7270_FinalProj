using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        PlayerStats playerStats;

        [Header("Default Model")]
        public GameObject defaultHeadModel;
        public GameObject defaultBodyModel;
        public GameObject defaultHipModel;
        public GameObject defaultLeftUpperArmModel;
        public GameObject defaultLeftLowerArmModel;
        public GameObject defaultRightUpperArmModel;
        public GameObject defaultRightLowerArmModel;
        public GameObject defaultLeftLegModel;
        public GameObject defaultRightLegModel;
        public GameObject defaultLeftHandModel;
        public GameObject defaultRightHandModel;

        [Header("Equipment Changer")]
        HelmetChanger helmetChanger;
        TorsoChanger torsoChanger;
        HipChanger hipChanger;
        LeftLegChanger leftLegChanger;
        RightLegChanger rightLegChanger;
        RightUpperArmChanger rightUpperArmChanger;
        LeftUpperArmChanger leftUpperArmChanger;
        LeftLowerArmChanger leftLowerArmChanger;
        RightLowerArmChanger rightLowerArmChanger;
        LeftHandChanger leftHandChanger;
        RightHandChanger rightHandChanger;

        public BlockingCollider blockingCollider;

        private void Awake()
        {
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerStats = GetComponentInParent<PlayerStats>();
            inputHandler = GetComponentInParent<InputHandler>();
            helmetChanger = GetComponentInChildren<HelmetChanger>();
            torsoChanger = GetComponentInChildren<TorsoChanger>();
            hipChanger = GetComponentInChildren<HipChanger>();
            leftLegChanger = GetComponentInChildren<LeftLegChanger>();
            rightLegChanger = GetComponentInChildren<RightLegChanger>();
            leftUpperArmChanger = GetComponentInChildren<LeftUpperArmChanger>();
            rightUpperArmChanger = GetComponentInChildren<RightUpperArmChanger>();
            leftLowerArmChanger = GetComponentInChildren<LeftLowerArmChanger>();
            rightLowerArmChanger = GetComponentInChildren<RightLowerArmChanger>();
            leftHandChanger = GetComponentInChildren<LeftHandChanger>();
            rightHandChanger = GetComponentInChildren<RightHandChanger>();
        }

        private void Start()
        {
            EquipAllEquipments();
        }

        public void EquipAllEquipments()
        {
            helmetChanger.UnequipAllHelmetModels();

            if (playerInventory.currentHelmetEquipment != null)
            {
                defaultHeadModel.SetActive(false);
                helmetChanger.EquipmentHelmetModelByName(playerInventory.currentHelmetEquipment.helmetModelName);
                playerStats.physicalDamaageAbsorptionHead = playerInventory.currentHelmetEquipment.physicalDef;
                playerStats.fireDamaageAbsorptionHead = playerInventory.currentHelmetEquipment.fireDef;
            }
            else
            {
                defaultHeadModel.SetActive(true);
                playerStats.physicalDamaageAbsorptionHead = 0;
            }

            
            torsoChanger.UnequipAllTorsoModels();
            leftUpperArmChanger.UnequipAllLeftUpperArmModels();
            rightUpperArmChanger.UnequipAllRightUpperArmModels();

            if (playerInventory.currentTorsoEquipment != null)
            {
                defaultBodyModel.SetActive(false);
                defaultLeftUpperArmModel.SetActive(false);
                defaultRightUpperArmModel.SetActive(false);
                torsoChanger.EquipmentTorsoModelByName(playerInventory.currentTorsoEquipment.torsoModelName);
                leftUpperArmChanger.EquipmentLeftUpperArmModelByName(playerInventory.currentTorsoEquipment.leftUpperArmName);
                rightUpperArmChanger.EquipmentRightUpperArmModelByName(playerInventory.currentTorsoEquipment.rightUpperArmName);
                playerStats.physicalDamaageAbsorptionBody = playerInventory.currentTorsoEquipment.physicalDef;
                playerStats.fireDamaageAbsorptionBody = playerInventory.currentTorsoEquipment.fireDef;
            }
            else
            {
                defaultBodyModel.SetActive(true);
                defaultLeftUpperArmModel.SetActive(true);
                defaultRightUpperArmModel.SetActive(true);
                playerStats.physicalDamaageAbsorptionBody = 0;
            }

            hipChanger.UnequipAllHipModels();
            leftLegChanger.UnequipAllLeftLegModels();
            rightLegChanger.UnequipAllRightLegModels();

            if (playerInventory.currentLegEquipment != null)
            {
                defaultHipModel.SetActive(false);
                defaultLeftLegModel.SetActive(false);
                defaultRightLegModel.SetActive(false);
                hipChanger.EquipmentHipModelByName(playerInventory.currentLegEquipment.hipModelName);
                leftLegChanger.EquipmentLeftLegModelByName(playerInventory.currentLegEquipment.leftLegName);
                rightLegChanger.EquipmentRightLegModelByName(playerInventory.currentLegEquipment.rightLegName);
                playerStats.physicalDamaageAbsorptionLeg = playerInventory.currentLegEquipment.physicalDef;
                playerStats.fireDamaageAbsorptionLeg = playerInventory.currentLegEquipment.fireDef;
            }
            else
            {
                defaultHipModel.SetActive(true);
                defaultLeftLegModel.SetActive(true);
                defaultRightLegModel.SetActive(true);
                playerStats.physicalDamaageAbsorptionLeg = 0;
            }

            rightLowerArmChanger.UnequipAllRightLowerArmModels();
            leftLowerArmChanger.UnequipAllLeftLowerArmModels();
            rightHandChanger.UnequipAllRightHandModels();
            leftHandChanger.UnequipAllLeftHandModels();

            if (playerInventory.currentHandEquipment != null)
            {
                defaultLeftLowerArmModel.SetActive(false);
                defaultRightLowerArmModel.SetActive(false);
                defaultRightHandModel.SetActive(false);
                defaultLeftHandModel.SetActive(false);
                leftHandChanger.EquipmentLeftHandModelByName(playerInventory.currentHandEquipment.leftHandName);
                rightHandChanger.EquipmentRightHandModelByName(playerInventory.currentHandEquipment.rightHandName);
                leftLowerArmChanger.EquipmentLeftLowerArmModelByName(playerInventory.currentHandEquipment.leftLowerArmName);
                rightLowerArmChanger.EquipmentRightLowerArmModelByName(playerInventory.currentHandEquipment.rightLowerArmName);
                playerStats.physicalDamaageAbsorptionHand = playerInventory.currentHandEquipment.physicalDef;
                playerStats.fireDamaageAbsorptionHand = playerInventory.currentHandEquipment.fireDef;
            }
            else
            {
                defaultLeftLowerArmModel.SetActive(true);
                defaultRightLowerArmModel.SetActive(true);
                defaultRightHandModel.SetActive(true);
                defaultLeftHandModel.SetActive(true);
                playerStats.physicalDamaageAbsorptionHand = 0;
            }
        }

        public void OpenBlockingCollider()
        {
            if (inputHandler.twoHandFlag)
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventory.rightWeapon);
            }
            else
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventory.leftWeapon);
            }
            blockingCollider.EnableBlockingCollider();

        }

        public void CloseBlockingCollider()
        {
            blockingCollider.DisableBlockingCollider();
        }
    }

}
