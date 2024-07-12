using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS {
    public class CharacterStats : MonoBehaviour
    {
        public bool isDead;

        public int vigorLevel = 1;
        public int maxHealth;
        public int currentHealth;

        public int enduranceLevel = 1;
        public int maxStamina;
        public float currentStamina;

        public int mindLevel = 1;
        public int maxMana;
        public int currentMana;

        public int targetHealth;
        public int targetMana;

        public int soulCount = 0;

        [Header("Armor Absorptions")]
        public float physicalDamaageAbsorptionHead;
        public float physicalDamaageAbsorptionBody;
        public float physicalDamaageAbsorptionLeg;
        public float physicalDamaageAbsorptionHand;

        public float fireDamaageAbsorptionHead;
        public float fireDamaageAbsorptionBody;
        public float fireDamaageAbsorptionLeg;
        public float fireDamaageAbsorptionHand;

        public int playerLevel;
        public int faithLevel = 1;
        public int strengthLevel = 1;
        public int dexterityLevel = 1;
        public int intelligenceLevel = 1;
        public int arcaneLevel = 1;

        public virtual void TakeDamage(int physicalDamage, string damageAnimation = "Damage")
        {
            if (isDead)
                return;

            float totalPhysicalDamageAbsorption = 1 - (1 - physicalDamaageAbsorptionHead / 100) *
                (1 - physicalDamaageAbsorptionHand / 100) *
                (1 - physicalDamaageAbsorptionBody / 100) *
                (1 - physicalDamaageAbsorptionLeg / 100) * (1 - (strengthLevel * 0.4f) / 100);

            physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

            float finalDamage = physicalDamage;

            currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public virtual void TakeFireDamage(int fireDamage, string damageAnimation = "Damage")
        {
            if (isDead)
                return;

            float totalFireDamageAbsorption = 1 - (1 - fireDamaageAbsorptionHead / 100) *
                (1 - fireDamaageAbsorptionHand / 100) *
                (1 - fireDamaageAbsorptionBody / 100) *
                (1 - fireDamaageAbsorptionLeg / 100) * (1 - (vigorLevel * 0.7f)/100);

            fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalFireDamageAbsorption));

            float finalDamage = fireDamage;

            currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }
    }
}

