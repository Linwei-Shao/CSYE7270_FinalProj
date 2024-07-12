using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS 
{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        Animator anim;
        public PlayerAnimatorManager playerAnimatorManager;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        public PlayerStats playerStats;

        InteractableUI interactableUI;
        public GameObject interactableUIGameObject;
        public GameObject itemInteratableGameObject;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir = false;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isUsingRightHand;
        public bool isUsingLeftHand;
        public bool isInvulnerable;
        public bool isCombating;

        public float combatTimer = 0;

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            backStabCollider = GetComponentInChildren<CriticalCollider>();
            playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerStats = GetComponent<PlayerStats>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            interactableUI = FindObjectOfType<InteractableUI>();
        }

        

        void Update()
        {
            float delta = Time.deltaTime;

            

            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            
            isUsingRightHand = anim.GetBool("isUsingRightHand");
            isUsingLeftHand = anim.GetBool("isUsingLeftHand");
            isInvulnerable = anim.GetBool("isInvulnerable");

            anim.SetBool("isBlocking", isBlocking);
            anim.SetBool("isInAir", isInAir);
            anim.SetBool("isDead",playerStats.isDead);

            inputHandler.TickInput(delta);

            playerLocomotion.HandleRollingAndSprinting(delta);
            playerAnimatorManager.canRotate = anim.GetBool("canRotate");
            playerLocomotion.HandleJumping();
            CheckForInteractableObject();
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRotation(delta);



        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.F_Input = false;
            inputHandler.E_Input = false;
            inputHandler.la_Input = false;
            inputHandler.ha_Input = false;
            inputHandler.sk_Input = false;
            inputHandler.d_pad_up = false;
            inputHandler.d_pad_down = false;
            inputHandler.d_pad_left = false;
            inputHandler.d_pad_right = false;
            inputHandler.esc_Input = false;

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }

            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        

        #region Player Interactions
        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            Vector3 rayOrigin = transform.position;
            rayOrigin.y = rayOrigin.y + 2f;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers)
                 ||
                Physics.SphereCast(rayOrigin, 0.3f, Vector3.down, out hit, 1.5f, cameraHandler.ignoreLayers))
            {
                if (hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);

                        if (inputHandler.E_Input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                            interactableUIGameObject.SetActive(false);
                            StartCoroutine(Wait());
                        }
                    }
                }
            }
            else
            {
                if (interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }
            }
         }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(3f);
            itemInteratableGameObject.SetActive(false);
        }

        public void OpenChestInteraction(Transform playerStandsWhenOpeningChest)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero;

            transform.position = playerStandsWhenOpeningChest.transform.position;

           playerAnimatorManager.PlayTargetAnimation("OpenLid", true);
        }

        public void PassThroughFogWallInteraction(Transform fogWallEntrance)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero;

            Vector3 rotationDirection = fogWallEntrance.transform.right;
            Quaternion turnRotation = Quaternion.LookRotation(rotationDirection);
            transform.rotation = turnRotation;

            playerAnimatorManager.PlayTargetAnimation("PushFog", true);

        }

        public void PushHeavyDoor(Transform HeavyDoorTransform, Transform playerStandsWhenPushDoor)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero;

            Vector3 rotationDirection = HeavyDoorTransform.transform.right;
            Quaternion turnRotation = Quaternion.LookRotation(rotationDirection);
            transform.rotation = turnRotation;

            transform.position = playerStandsWhenPushDoor.transform.position;

            playerAnimatorManager.PlayTargetAnimation("Push_Start", true);

        }
        #endregion
    }

}
