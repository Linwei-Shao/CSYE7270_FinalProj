using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class BonfireInteractable : Interactable
    {
        public Transform bonfireTeleportTransform;

        public bool hasActivated;

        public bool isBossBonfire;

        public ParticleSystem activationFX;
        public ParticleSystem fireFX;

        public GameObject bonfireLight;

        public GameObject bonfireInteractText;

        AudioSource audioSource;
        public AudioClip bonfireActivation;

        public UIManager uiManager;
        public PlayerInventory playerInventory;

        public GameObject levelWindow;

        public FlaskItem estus;
        public FlaskItem ashen;

        HealthBar healthBar;
        ManaBar manaBar;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
            playerInventory = FindObjectOfType<PlayerInventory>();
            healthBar = FindObjectOfType<HealthBar>();
            manaBar = FindObjectOfType<ManaBar>();

            if (hasActivated)
            {
                fireFX.gameObject.SetActive(true);
                fireFX.Play();
                interactableText = "Rest";
            }
            else
            {
                interactableText = "Light";
            }

            audioSource = GetComponent<AudioSource>();
        }
        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            if (hasActivated)
            {
                playerManager.playerStats.currentHealth = playerManager.playerStats.maxHealth;
                playerManager.playerStats.currentMana = playerManager.playerStats.maxMana;
                healthBar.SetCurrentHealth(playerManager.playerStats.maxHealth);
                manaBar.SetCurrentMana(playerManager.playerStats.maxMana);


                estus.currentItemAmount = estus.maxItemAmount;
                ashen.currentItemAmount = ashen.maxItemAmount;
                playerManager.playerAnimatorManager.anim.SetBool("isLeveling", true);

                playerManager.playerAnimatorManager.PlayTargetAnimation("Bonfire_Start", true);
                uiManager.currentConsumableItemAmount.text = playerInventory.currentConsumableItem.currentItemAmount.ToString();
                levelWindow.SetActive(true);
                uiManager.CloseHUD();
                bonfireInteractText.SetActive(false);
            }
            else
            {
                playerManager.playerAnimatorManager.PlayTargetAnimation("Bonfire_Ignite", true);
                hasActivated = true;
                interactableText = "Rest";
                activationFX.gameObject.SetActive(true);
                activationFX.Play();
                fireFX.gameObject.SetActive(true);
                fireFX.Play();
                bonfireLight.SetActive(true);
                uiManager.BonfireLitUI();
                audioSource.PlayOneShot(bonfireActivation);
            }
        }
    }
}

