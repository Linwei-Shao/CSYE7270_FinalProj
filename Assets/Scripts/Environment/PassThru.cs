using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class PassThru : Interactable
    {
        WorldEventManager worldEventManager;
        Animator anim;

        private void Awake()
        {
            worldEventManager = FindObjectOfType<WorldEventManager>();
            anim = GetComponentInParent<Animator>();
        }

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            playerManager.PassThroughFogWallInteraction(transform);




            if (anim != null && anim.CompareTag("SewerDoor"))
            {
                anim.Play("SewerDoorOpen");
                this.gameObject.SetActive(false);
            }
            

            
        }
    }
}

