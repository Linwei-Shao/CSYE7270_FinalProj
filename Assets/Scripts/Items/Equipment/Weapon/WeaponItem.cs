using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS {
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefeb;
        public bool isUnarmed;

        [Header("Idle Animations")]
        public string right_Hand_Idle;
        public string left_Hand_Idle;
        public string two_Hand_Idle;

        [Header("One Handed Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Heavy_Attack_1;
        public string OH_Light_Attack_2;
        public string Skill_1;
        public string TH_Light_Attack_1;
        public string TH_Light_Attack_2;
        public string TH_Heavy_Attack_1;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;

        [Header("Absorption")]
        public float damageAbsorption;

        [Header("Damage")]
        public int light1damage;
        public int light2damage;
        public int heavydamage;
        public int skilldamage;
        public int criticalDamageMultiplier;

        [Header("Mana Cost")]
        public int manaCost;

        [Header("Weapon Type")]
        public bool isSpellCaster;
        public bool isFaithCaster;
        public bool isPyroCaster;
        public bool isMeleeWeapon;
        public bool isShield;
    }
}

