using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class SpellItem : Item
    {
        public GameObject spellWarmUpFX;
        public GameObject spellCastFX;

        public string spellAnimation;

        [Header("Spell Type")]
        public bool isFaithSpell;
        public bool isMagicSpell;
        public bool isPyroSpell;

        [Header("Spell Description")]
        [TextArea]
        public string spellDesc;

        public int manaCost;

        public int staminaCost;

        public virtual void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, WeaponSlotManager weaponSlotManager)
        {
            Debug.Log("attempt");
        }

        public virtual void DestroyWarmUpEffect()
        {
            Debug.Log("warmup destroyed");
        }
        public virtual void DestroySpellEffect()
        {
            Debug.Log("spell destroyed");
        }

        public virtual void SuccessfullyCasted(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, CameraHandler cameraHandler, WeaponSlotManager weaponSlotManager)
        {
            Debug.Log("casted");
        }
    }
}

