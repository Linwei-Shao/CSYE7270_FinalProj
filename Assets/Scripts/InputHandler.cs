using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS 
{
    public class InputHandler : MonoBehaviour
    {
        [Header ("Mouse Input")]
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        [Header("Misc Input")]
        public bool space_Input;   
        public bool alt_Input;
        public bool E_Input;
        public bool F_Input;
        public bool R_Input;
        public bool twoHand_Input;
        public bool esc_Input;
        public bool lockon_Input;
        public bool rightlock_Input;
        public bool leftlock_Input;

        public bool shift_Input;
        [Header("Combat Input")]
        public bool la_Input;
        public bool ha_Input;
        public bool sk_Input;
        public bool ct_Input;
        public bool bl_Input;

        [Header("Inventory Input")]
        public bool d_pad_up;
        public bool d_pad_left;
        public bool d_pad_right;
        public bool d_pad_down;

        [Header("Flags")]
        public bool twoHandFlag;
        public bool rollFlag;
        public bool sprintFlag;
        public bool walkingFlag;
        public bool comboFlag;
        public bool lockOnFlag;
        public bool windowFlag;
        public bool menuFlag = false;
        public float rollInputTimer;

        public Transform criticalAttackRayCastStartPoint;

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        BlockingCollider blockingCollider;
        PlayerManager playerManager;
        WeaponSlotManager weaponSlotManager;
        UIManager uiManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerEffectsManager playerEffectsManager;
        CameraHandler cameraHandler;
        QuickSlotsUI quickSlotsUI;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Start()
        {

            quickSlotsUI.UpdateItemUI(playerInventory.currentConsumableItem);
        }
        private void Awake()
        {
            playerAttacker = GetComponentInChildren<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            uiManager = FindObjectOfType<UIManager>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
            blockingCollider = GetComponentInChildren<BlockingCollider>();
            playerEffectsManager = GetComponentInChildren<PlayerEffectsManager>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
        }

        private void Update()
        {
            uiManager.inventoryCheck();
        }

        public void OnEnable()
        {
           if(inputActions == null) 
           {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                inputActions.PlayerActions.LA.performed += i => la_Input = true;
                inputActions.PlayerActions.HA.performed += i => ha_Input = true;
                inputActions.PlayerActions.Skill.performed += i => sk_Input = true;
                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_pad_right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_pad_left = true;
                inputActions.PlayerQuickSlots.DPadUp.performed += i => d_pad_up = true;
                inputActions.PlayerQuickSlots.DPadDown.performed += i => d_pad_down = true;
                inputActions.PlayerActions.PickUp.performed += i => E_Input = true;
                inputActions.PlayerActions.Jump.performed += i => F_Input = true;
                inputActions.PlayerActions.UseItem.performed += i => R_Input = true;
                inputActions.PlayerActions.Menu.performed += i => esc_Input = true;
                inputActions.PlayerActions.LockOn.performed += i => lockon_Input = true;
                inputActions.PlayerActions.twoHand.performed += i => twoHand_Input = true;
                inputActions.PlayerActions.BackStab.performed += i => ct_Input = true;
                inputActions.PlayerActions.Block.performed += i => bl_Input = true;
                inputActions.PlayerActions.Block.canceled += i => bl_Input = false;

                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => leftlock_Input = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => rightlock_Input = true;
            }
            inputActions.Enable();
        }

        public void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta) 
        {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleCombatInput(delta);
            HandleQuickSlotsInput();
            HandleMenuInput();
            HandleLockOnInput();
            HandleTwoHandInput();
            HandleCriticalInput();
            HandleUseConsumable();
        }

        private void MoveInput(float delta) 
        {
            alt_Input = inputActions.PlayerActions.Walk.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            

            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            space_Input = inputActions.PlayerActions.RollSprint.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            sprintFlag = space_Input;

            if (space_Input)
            {
                rollInputTimer += delta;
            
            }
            else
            { 
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleCombatInput(float delta)
        {


            shift_Input = inputActions.PlayerActions.Alter.phase == UnityEngine.InputSystem.InputActionPhase.Performed;



            if (la_Input)
            {

                if (menuFlag == true || windowFlag == true)
                {
                    return;
                }
                else
                {
                    playerAttacker.HandleAttackAction();
                }
            }
           

            if (ha_Input && shift_Input)
            {
                if (playerManager.isInteracting)
                    return;

                playerAnimatorManager.anim.SetBool("isUsingRightHand", true);
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }

            if (bl_Input)
            {
                
                playerAttacker.HandleBlockAction();
            }
            else
            {
                playerManager.isBlocking = false;

                if (blockingCollider.blockingCollider.enabled)
                {
                    blockingCollider.DisableBlockingCollider();
                }
            }



            if (sk_Input && shift_Input)
            {
                if (playerManager.isInteracting)
                    return;

                if (twoHandFlag)
                {
                    playerAnimatorManager.anim.SetBool("isUsingRightHand", true);
                    playerAttacker.HandleSkill(playerInventory.rightWeapon);
                }
                else if (playerInventory.leftWeapon.isShield == false)
                {
                    playerAnimatorManager.anim.SetBool("isUsingRightHand", true);
                    playerAttacker.HandleSkill(playerInventory.rightWeapon);
                }
                else
                {
                    playerAttacker.HandleParryAction();
                }
            }
        }

        private void HandleQuickSlotsInput()
        {
            if (windowFlag)
                return;

            if (d_pad_right)
            {
                playerInventory.ChangeRightWeapon();
            }
            else if (d_pad_left)
            {
                playerInventory.ChangeLeftWeapon();
            }
            else if (d_pad_up)
            {
                playerInventory.ChangeCurrentSpell();
            }
            else if(d_pad_down)
            {
                playerInventory.ChangeCurrentItem();
               
            }
        }

        private void HandleMenuInput()
        {


            if (esc_Input)
            {
                uiManager.inventoryCheck();

                if (windowFlag)
                {
                    uiManager.ResetAllSelectedSlots();
                    uiManager.weaponInventoryWindow.SetActive(false);
                    uiManager.helmetInventoryWindow.SetActive(false);
                    uiManager.handInventoryWindow.SetActive(false);
                    uiManager.torsoInventoryWindow.SetActive(false);
                    uiManager.legInventoryWindow.SetActive(false);
                    uiManager.equipmentWindow.SetActive(false);

                    uiManager.levelWindow.SetActive(false);
                    uiManager.OpenHUD();
                    menuFlag = false;
                    windowFlag = false;
                    playerManager.playerAnimatorManager.anim.SetBool("isLeveling", false);
                }
                else
                {
                    if (!menuFlag)
                    {
                        uiManager.OpenMenu(); 
                        uiManager.CloseHUD();
                        menuFlag = true;
                    }
                    else
                    {
                        uiManager.CloseMenu();
                        uiManager.OpenHUD();
                        menuFlag = false;
                    }
                }
                

                
            }

        }

        private void HandleLockOnInput()
        {
            if (lockon_Input && lockOnFlag == false)
            {
                lockon_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if (lockon_Input && lockOnFlag)
            {
                lockon_Input = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }

            if (lockOnFlag && leftlock_Input)
            {
                leftlock_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }
            }

            if (lockOnFlag && rightlock_Input)
            {
                rightlock_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.rightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }

            cameraHandler.SetCameraHeight();
        }

        private void HandleTwoHandInput()
        {
            if (twoHand_Input)
            {
                twoHand_Input = false;

                twoHandFlag = !twoHandFlag;

                if (twoHandFlag)
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
                }
                else
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);
                }
            }
        }

        private void HandleCriticalInput() 
        {
            if (ct_Input)
            {
                ct_Input = false;
                playerAttacker.AttemptBackStabOrRiposte();
            }
        }

        private void HandleUseConsumable()
        {
            if (R_Input)
            {
                R_Input = false;

                if (playerEffectsManager.isDrinking)
                    return;

                playerEffectsManager.isDrinking = true;
                playerInventory.currentConsumableItem.AttemptToConsumeItem(playerAnimatorManager,weaponSlotManager,playerEffectsManager);
            }
        }
    }
}
