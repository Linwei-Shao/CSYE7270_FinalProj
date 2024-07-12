using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class LevelUpUI : MonoBehaviour
    {
        public PlayerStats playerStats;
        InputHandler inputHandler;
        UIManager uiManager;
        PlayerManager playerManager;
        SoulCount soulCount;

        public HealthBar healthBar;
        public ManaBar manaBar;
        public StaminaBar staminaBar;

        public Button confirmButton;

        public int currentPlayerLevel;
        public int projectedPlayerLevel;
        public Text currentPlayerLevelText;
        public Text projectedPlayerLevelText;

        public int currentSoul;
        public int projectedSoul;
        public int soulRequired;
        public Text currentSoulText;
        public Text projectedSoulText;
        public Text soulRequiredText;
        public int baseLevelUpCost = 60;

        public Slider healthSlider;
        public Text currentHealthLevelText;
        public Text projectedHealthLevelText;

        public Slider staminaSlider;
        public Text currentStaminaLevelText;
        public Text projectedStaminaLevelText;

        public Slider mindSlider;
        public Text currentMindLevelText;
        public Text projectedMindLevelText;

        public Slider strengthSlider;
        public Text currentStrengthLevelText;
        public Text projectedStrengthLevelText;

        public Slider dexSlider;
        public Text currentDexLevelText;
        public Text projectedDexLevelText;

        public Slider faithSlider;
        public Text currentFaithLevelText;
        public Text projectedFaithLevelText;

        public Slider intSlider;
        public Text currentIntLevelText;
        public Text projectedIntLevelText;

        public Slider arcaneSlider;
        public Text currentArcaneLevelText;
        public Text projectedArcaneLevelText;

        public int projectedHealth;
        public int projectedMana;
        public int projectedStamina;
        public Text projectedHealthText;
        public Text projectedManaText;
        public Text projectedStaminaText;
        public Text healthText;
        public Text manaText;
        public Text staminaText;

        public float currentATK;
        public float currentFireATK;
        public float projectedATK;
        public float projectedFireATK;
        public Text projectedATKText;
        public Text projectedFireATKText;
        public Text atkText;
        public Text fireatkText;

        public float currentDEF;
        public float currentFireDEF;
        public float projectedDEF;
        public float projectedFireDEF;
        public Text projectedDEFText;
        public Text projectedFireDEFText;
        public Text defText;
        public Text firedefText;

        private void Awake()
        {
            inputHandler = FindObjectOfType<InputHandler>();
            playerManager = FindObjectOfType<PlayerManager>();
            uiManager = FindObjectOfType<UIManager>();
            soulCount = FindObjectOfType<SoulCount>();
            healthBar = FindObjectOfType<HealthBar>();
            manaBar = FindObjectOfType<ManaBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
        }
        private void OnEnable()
        {
            currentSoul = playerStats.soulCount;
            currentSoulText.text = currentSoul.ToString();

            currentPlayerLevel = playerStats.playerLevel;
            currentPlayerLevelText.text = currentPlayerLevel.ToString();
            projectedPlayerLevel = playerStats.playerLevel;
            projectedPlayerLevelText.text = projectedPlayerLevel.ToString();

            healthSlider.value = playerStats.vigorLevel;
            currentHealthLevelText.text = playerStats.vigorLevel.ToString();
            projectedHealthLevelText.text = playerStats.vigorLevel.ToString();
            healthSlider.minValue = playerStats.vigorLevel;
            healthSlider.maxValue = 99;

            staminaSlider.value = playerStats.enduranceLevel;
            currentStaminaLevelText.text = playerStats.enduranceLevel.ToString();
            projectedStaminaLevelText.text = playerStats.enduranceLevel.ToString();
            staminaSlider.minValue = playerStats.enduranceLevel;
            staminaSlider.maxValue = 99;

            mindSlider.value = playerStats.mindLevel;
            currentMindLevelText.text = playerStats.mindLevel.ToString();
            projectedMindLevelText.text = playerStats.mindLevel.ToString();
            mindSlider.minValue = playerStats.mindLevel;
            mindSlider.maxValue = 99;

            strengthSlider.value = playerStats.strengthLevel;
            currentStrengthLevelText.text = playerStats.strengthLevel.ToString();
            projectedStrengthLevelText.text = playerStats.strengthLevel.ToString();
            strengthSlider.minValue = playerStats.strengthLevel;
            strengthSlider.maxValue = 99;

            dexSlider.value = playerStats.dexterityLevel;
            currentDexLevelText.text = playerStats.dexterityLevel.ToString();
            projectedDexLevelText.text = playerStats.dexterityLevel.ToString();
            dexSlider.minValue = playerStats.dexterityLevel;
            dexSlider.maxValue = 99;

            intSlider.value = playerStats.intelligenceLevel;
            currentIntLevelText.text = playerStats.intelligenceLevel.ToString();
            projectedIntLevelText.text = playerStats.intelligenceLevel.ToString();
            intSlider.minValue = playerStats.intelligenceLevel;
            intSlider.maxValue = 99;

            faithSlider.value = playerStats.faithLevel;
            currentFaithLevelText.text = playerStats.faithLevel.ToString();
            projectedFaithLevelText.text = playerStats.faithLevel.ToString();
            faithSlider.minValue = playerStats.faithLevel;
            faithSlider.maxValue = 99;

            arcaneSlider.value = playerStats.arcaneLevel;
            currentArcaneLevelText.text = playerStats.arcaneLevel.ToString();
            projectedArcaneLevelText.text = playerStats.arcaneLevel.ToString();
            arcaneSlider.minValue = playerStats.arcaneLevel;
            arcaneSlider.maxValue = 99;

            soulRequired = 0;


            projectedPlayerLevel = currentPlayerLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(healthSlider.value) - playerStats.vigorLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(staminaSlider.value) - playerStats.enduranceLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(mindSlider.value) - playerStats.mindLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(strengthSlider.value) - playerStats.strengthLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(dexSlider.value) - playerStats.dexterityLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(intSlider.value) - playerStats.intelligenceLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(faithSlider.value) - playerStats.faithLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(arcaneSlider.value) - playerStats.arcaneLevel;

            projectedPlayerLevelText.text = projectedPlayerLevel.ToString();

            projectedSoul = currentSoul - soulRequired;

            currentSoulText.text = playerStats.soulCount.ToString();
            projectedSoulText.text = projectedSoul.ToString();
            soulRequiredText.text = soulRequired.ToString();

        }
        public void ConfirmLevelUp()
        {
            playerStats.playerLevel = projectedPlayerLevel;
            playerStats.vigorLevel = Mathf.RoundToInt(healthSlider.value);

            playerStats.enduranceLevel = Mathf.RoundToInt(staminaSlider.value);
            playerStats.mindLevel = Mathf.RoundToInt(mindSlider.value);
            playerStats.strengthLevel = Mathf.RoundToInt(strengthSlider.value);
            playerStats.dexterityLevel = Mathf.RoundToInt(dexSlider.value);
            playerStats.intelligenceLevel = Mathf.RoundToInt(intSlider.value);
            playerStats.faithLevel = Mathf.RoundToInt(faithSlider.value);
            playerStats.arcaneLevel = Mathf.RoundToInt(arcaneSlider.value);

            playerStats.maxHealth = playerStats.SetMaxHealthFromHealthLevel();
            playerStats.currentHealth = playerStats.maxHealth;
            playerStats.maxStamina = playerStats.SetMaxStaminaFromStaminaLevel();
            playerStats.currentStamina = playerStats.maxStamina;
            playerStats.maxMana = playerStats.SetMaxManaFromManaLevel();
            playerStats.currentMana = playerStats.maxMana;

            playerStats.AdjustHealthBar(playerStats.maxHealth);
            playerStats.AdjustManaBar(playerStats.maxMana);
            playerStats.AdjustStaminaBar(playerStats.maxStamina);

            healthBar.SetMaxHealth(playerStats.maxHealth);
            healthBar.SetCurrentHealth(playerStats.maxHealth);
            manaBar.SetMaxMana(playerStats.maxMana);
            manaBar.SetCurrentMana(playerStats.maxMana);
            staminaBar.SetMaxStamina(playerStats.maxStamina);
            staminaBar.SetCurrentStamina(playerStats.maxStamina);

            playerStats.soulCount = playerStats.soulCount - soulRequired;

            soulCount.SetCurrentSoul(playerStats.soulCount);

            uiManager.levelWindow.SetActive(false);
            uiManager.OpenHUD();
            inputHandler.menuFlag = false;
            inputHandler.windowFlag = false;
            playerManager.playerAnimatorManager.anim.SetBool("isLeveling", false);
            soulRequired = 0;
        }

        public void CalculateSoulCost()
        {
            for (int i = 1; i < projectedPlayerLevel; i++)
            {
                soulRequired = soulRequired + Mathf.RoundToInt((projectedPlayerLevel * baseLevelUpCost) * 1.5f);
            }

            if (projectedPlayerLevel == currentPlayerLevel)
            {
                soulRequired = 0;
            }

            projectedSoul = currentSoul - soulRequired;

            if (projectedSoul < 0)
            {
                projectedSoul = currentSoul;
            }
            else
            {
                projectedSoul = currentSoul - soulRequired;
            }


            currentSoulText.text = playerStats.soulCount.ToString();
            projectedSoulText.text = projectedSoul.ToString();
            soulRequiredText.text = soulRequired.ToString();
        }

        #region update level slider
        private void UpdateProjectedPlayerLevel()
        {
            soulRequired = 0;


            projectedPlayerLevel = currentPlayerLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(healthSlider.value) - playerStats.vigorLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(staminaSlider.value) - playerStats.enduranceLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(mindSlider.value) - playerStats.mindLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(strengthSlider.value) - playerStats.strengthLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(dexSlider.value) - playerStats.dexterityLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(intSlider.value) - playerStats.intelligenceLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(faithSlider.value) - playerStats.faithLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(arcaneSlider.value) - playerStats.arcaneLevel;

            projectedPlayerLevelText.text = projectedPlayerLevel.ToString();

            healthText.text = playerStats.maxHealth.ToString();
            manaText.text = playerStats.maxMana.ToString();
            staminaText.text = playerStats.maxStamina.ToString();

            projectedHealth = Mathf.RoundToInt(healthSlider.value) * 18 + 318;
            projectedMana = Mathf.RoundToInt(mindSlider.value) * 4 + 54;
            projectedStamina = Mathf.RoundToInt(staminaSlider.value) + 81;

            projectedHealthText.text = projectedHealth.ToString();
            projectedManaText.text = projectedMana.ToString();
            projectedStaminaText.text = projectedStamina.ToString();

            currentATK = 1f + (playerStats.strengthLevel / 30f) * (playerStats.strengthLevel / 30f);
            currentFireATK = playerStats.faithLevel * 5.88f;

            atkText.text = currentATK.ToString("F2");
            fireatkText.text = currentFireATK.ToString("F2");

            projectedATK = 1f + (Mathf.RoundToInt(strengthSlider.value) / 30f) * (Mathf.RoundToInt(strengthSlider.value) / 30f);
            projectedFireATK = Mathf.RoundToInt(faithSlider.value) * 5.88f;

            projectedATKText.text = projectedATK.ToString("F2");
            projectedFireATKText.text = projectedFireATK.ToString("F2");



            currentDEF = 0.4f * playerStats.strengthLevel;
            currentFireDEF = playerStats.vigorLevel * 0.7f;

            defText.text = currentDEF.ToString("F2");
            firedefText.text = currentFireDEF.ToString("F2");

            projectedDEF = 0.4f * Mathf.RoundToInt(strengthSlider.value);
            projectedFireDEF = Mathf.RoundToInt(healthSlider.value) * 0.7f;

            projectedDEFText.text = projectedDEF.ToString("F2");
            projectedFireDEFText.text = projectedFireDEF.ToString("F2");


            CalculateSoulCost();

            if (playerStats.soulCount < soulRequired)
            {
                soulRequiredText.color = new Color(204, 0, 0, 1);
                confirmButton.interactable = false;
            }
            else
            {
                soulRequiredText.color = new Color(255, 255, 255, 1);
                confirmButton.interactable = true;
            }
        }

        public void UpdateHealthLevelSlider()
        {
            projectedHealthLevelText.text = healthSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        public void UpdateStaminaLevelSlider()
        {
            projectedStaminaLevelText.text = staminaSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        public void UpdateMindLevelSlider()
        {
            projectedMindLevelText.text = mindSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        public void UpdateStrengthLevelSlider()
        {
            projectedStrengthLevelText.text = strengthSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        public void UpdateDexLevelSlider()
        {
            projectedDexLevelText.text = dexSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        public void UpdateIntLevelSlider()
        {
            projectedIntLevelText.text = intSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        public void UpdateFaithLevelSlider()
        {
            projectedFaithLevelText.text = faithSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        public void UpdateArcaneLevelSlider()
        {
            projectedArcaneLevelText.text = arcaneSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

        #endregion
    }
}

