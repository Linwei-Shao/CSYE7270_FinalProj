using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class PlayerStats : CharacterStats
    {
        PlayerAnimatorManager animatorHandler;
        PlayerManager playerManager;
        InputHandler inputHandler;
        Rigidbody rb;
        public CapsuleCollider collider;
        UIManager uiManager;

        public HealthBar healthbar;
        public ManaBar manabar;
        public StaminaBar staminabar;



        private void Awake()
        {
            animatorHandler = GetComponentInChildren<PlayerAnimatorManager>();
            playerManager = GetComponent<PlayerManager>();
            inputHandler = GetComponent<InputHandler>();
            rb = GetComponent<Rigidbody>();
            collider = GetComponentInChildren<CapsuleCollider>();
            uiManager = FindObjectOfType<UIManager>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
            AdjustHealthBar(maxHealth);
            healthbar.SetCurrentHealth(currentHealth);


            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminabar.SetMaxStamina(maxStamina);
            AdjustStaminaBar(maxStamina);
            staminabar.SetCurrentStamina(currentStamina);

            maxMana = SetMaxManaFromManaLevel();
            currentMana = maxMana;
            manabar.SetMaxMana(maxMana);
            AdjustManaBar(maxMana);
            manabar.SetCurrentMana(currentMana);
        }


        public float timer = 0;

        private float timerMax = 0;

        void Update()
        {
            if (playerManager.isSprinting)
            {
                timer = 0;
            }

            if (playerManager.isInteracting)
            {
                timer = 0.5f;
            }

            if (currentStamina < maxStamina && !playerManager.isInteracting && !playerManager.isSprinting && !playerManager.isBlocking)
            {
                if (!Waited(1)) return;
                StaminaRecovery(1 + Mathf.FloorToInt(enduranceLevel / 50));
            }

            if (currentHealth < targetHealth)
            {
                HealthRecovery(5);
            }
            else
            {
                targetHealth = 0;
            }

            if (currentMana < targetMana)
            {
                ManaRecovery(5);
            }
            else
            {
                targetMana = 0;
            }


        }

        private bool Waited(float seconds)
        {
            timerMax = seconds;

            timer += Time.deltaTime;

            if (timer >= timerMax)
            {
                return true;
            }

            return false;
        }


        

        public void TakeDamageNoAnim(int damage)
        {
            if (isDead)
                return;

            currentHealth = currentHealth - damage;
            healthbar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                animatorHandler.anim.SetBool("isDead", true);
                currentHealth = 0;
                rb.velocity = Vector3.zero;
                isDead = true;
                rb.constraints = RigidbodyConstraints.FreezePosition;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                uiManager.CloseHUD();
                uiManager.Died();
            }
        }

        public override void TakeDamage(int damage, string damageAnimation = "Damage")
        {
            if (isDead)
                return;

            if (playerManager.isInvulnerable)
                return;

            base.TakeDamage(damage, damageAnimation = "Damage");

            healthbar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0) 
            {
                animatorHandler.anim.SetBool("isDead", true);
                currentHealth = 0;
                rb.velocity = Vector3.zero;
                animatorHandler.PlayTargetAnimation("Damage_Die", true);
                isDead = true;
                rb.constraints = RigidbodyConstraints.FreezePosition;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                uiManager.CloseHUD();
                uiManager.Died();
            }
        }

        public override void TakeFireDamage(int damage, string damageAnimation = "Damage")
        {
            if (isDead)
                return;

            if (playerManager.isInvulnerable)
                return;

            base.TakeFireDamage(damage, damageAnimation = "Damage");

            healthbar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0)
            {
                animatorHandler.anim.SetBool("isDead", true);
                currentHealth = 0;
                rb.velocity = Vector3.zero;
                animatorHandler.PlayTargetAnimation("Damage_Die", true);
                isDead = true;
                rb.constraints = RigidbodyConstraints.FreezePosition;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                uiManager.CloseHUD();
                uiManager.Died();
            }
        }

        public void TakeStaminaDrain(float drain)
        {
            currentStamina = currentStamina - drain;
            staminabar.SetCurrentStamina(currentStamina);
        }

        public void TakeManaCost(int manacost)
        {
            currentMana = currentMana - manacost;
            manabar.SetCurrentMana(currentMana);
        }

        public void StaminaRecovery(float recovery)
        {
            currentStamina = currentStamina + recovery;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            staminabar.SetCurrentStamina(currentStamina);
        }

        public void HealthRecovery(int recovery)
        {
            currentHealth = currentHealth + recovery;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthbar.SetCurrentHealth(currentHealth);
        }

        public void ManaRecovery(int recovery)
        {
            currentMana = currentMana + recovery;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            manabar.SetCurrentMana(currentMana);
        }

        public int SetMaxHealthFromHealthLevel()
        {
            maxHealth = vigorLevel * 18 + 318;
            return maxHealth;
        }

        public int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = enduranceLevel + 81;
            return maxStamina;
        }

        public int SetMaxManaFromManaLevel()
        {
            maxMana = mindLevel * 4 + 54;
            return maxMana;
        }
        public void AdjustHealthBar(int maxStats)
        {
            RectTransform healthBarRectTransform = healthbar.GetComponent<RectTransform>();

            float healthSize = maxHealth / 2f;

            // Set the width of the health bar to be exactly the maxHealth
            healthBarRectTransform.sizeDelta = new Vector2(healthSize, healthBarRectTransform.sizeDelta.y);

            // Adjust the transform's x position
            float newPosX = (healthSize - 100) * 0.5f + 75;
            healthBarRectTransform.anchoredPosition = new Vector2(newPosX, healthBarRectTransform.anchoredPosition.y);

        }

        public void AdjustStaminaBar(int maxStats)
        {
            RectTransform staminaBarRectTransform = staminabar.GetComponent<RectTransform>();

            float staminaSize = maxStamina * 3f;

            staminaBarRectTransform.sizeDelta = new Vector2(staminaSize, staminaBarRectTransform.sizeDelta.y);

            float newPosX = (staminaSize - 100) * 0.5f + 75;
            staminaBarRectTransform.anchoredPosition = new Vector2(newPosX, staminaBarRectTransform.anchoredPosition.y);

        }

        public void AdjustManaBar(int maxStats)
        {
            RectTransform manaBarRectTransform = manabar.GetComponent<RectTransform>();

            float manaSize = maxMana * 2f;

            // Set the width of the health bar to be exactly the maxHealth
            manaBarRectTransform.sizeDelta = new Vector2(manaSize, manaBarRectTransform.sizeDelta.y);

            // Adjust the transform's x position
            float newPosX = (manaSize - 100) * 0.5f + 75;
            manaBarRectTransform.anchoredPosition = new Vector2(newPosX, manaBarRectTransform.anchoredPosition.y);

        }

        public void HealPlayer(float healAmount)
        {
            currentHealth = currentHealth + Mathf.RoundToInt(faithLevel * healAmount);

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            healthbar.SetCurrentHealth(currentHealth);
        }

        public void HealthRecoverPlayer(int healAmount)
        {
            targetHealth = currentHealth + healAmount;

            if (targetHealth > maxHealth)
            {
                targetHealth = maxHealth;
            }
        }

        public void ManaRecoverPlayer(int manaAmount)
        {
            targetMana = currentMana + manaAmount;

            if (targetMana > maxMana)
            {
                targetMana = maxMana;
            }
        }

        public void AddSouls(int souls)
        {
            soulCount += souls;
        }
    }

}
