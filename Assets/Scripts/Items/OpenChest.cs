using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class OpenChest : Interactable
    {
        Animator animator;
        OpenChest openChest;

        public Transform playerStandingPosition;
        public GameObject itemSpawner;
        public WeaponItem weaponInChest;
        public SpellItem spellInChest;
        public HelmetEquipment helmetInChest;
        public TorsoEquipment torsoInChest;
        public HandEquipment handInChest;
        public LegEquipment legInChest;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            openChest = GetComponent<OpenChest>();
        }
        public override void Interact(PlayerManager playerManager)
        {

            playerManager.OpenChestInteraction(playerStandingPosition);
            Vector3 rotationDirection = transform.position - playerManager.transform.position;
            rotationDirection.y = 0;
            rotationDirection.Normalize();

            Quaternion tr = Quaternion.LookRotation(rotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 300 * Time.deltaTime);
            playerManager.transform.rotation = targetRotation;

            
            animator.Play("ChestOpen");
            StartCoroutine(SpawnItemInChest());

            WeaponPickUp weaponPickUp = itemSpawner.GetComponent<WeaponPickUp>();
            SpellPickUp spellPickUp = itemSpawner.GetComponent<SpellPickUp>();
            EquipmentPickUp equipmentPickUp = itemSpawner.GetComponent<EquipmentPickUp>();


            if (weaponPickUp != null)
            {
                weaponPickUp.weapon = null;

                

                weaponPickUp.weapon = weaponInChest;
            }
            else if (spellPickUp != null)
            {
                spellPickUp.spell = null;

                spellPickUp.spell = spellInChest;
            }
            else if (equipmentPickUp != null)
            {
                equipmentPickUp.helmet = null;
                equipmentPickUp.torso = null;
                equipmentPickUp.hand = null;
                equipmentPickUp.leg = null;

                if (helmetInChest != null)
                {
                    equipmentPickUp.helmet = helmetInChest;
                }
                else if (torsoInChest != null)
                {
                    equipmentPickUp.torso = torsoInChest;
                }
                else if (handInChest != null)
                {
                    equipmentPickUp.hand= handInChest;
                }
                else if (legInChest != null)
                {
                    equipmentPickUp.leg = legInChest;
                }
            }
        }

        private IEnumerator SpawnItemInChest()
        {
            
            yield return new WaitForSeconds(2f);
            Instantiate(itemSpawner, transform);
            this.tag = "Untagged";
            Destroy(openChest);
        }
    }
}

