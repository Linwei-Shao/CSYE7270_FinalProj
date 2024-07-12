using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS 
{
    [CreateAssetMenu(menuName = "Spells/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public float healAmount;
        public GameObject instantiatedWarmUpSpellFX;
        public GameObject instantiatedSpellFX; 

        public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, WeaponSlotManager weaponSlotManager)
        {
                instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManager.rightHandSlot.transform);
                animatorHandler.PlayTargetAnimation(spellAnimation, true);
        }

        public override void DestroyWarmUpEffect()
        {
            Destroy(instantiatedWarmUpSpellFX);
        }

        public override void DestroySpellEffect()
        {

            Destroy(instantiatedSpellFX);
        }



        public override void SuccessfullyCasted(PlayerAnimatorManager animatorHandler, PlayerStats playerStats, CameraHandler cameraHandle, WeaponSlotManager weaponSlotManager)
        {
            instantiatedSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            playerStats.HealPlayer(healAmount);
        }
    }
}

