using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class PushHeavyDoor : Interactable
    {
        WorldEventManager worldEventManager;
        Animator anim;
        public Transform playerStandsWhenPushDoor;

        private void Awake()
        {
            worldEventManager = FindObjectOfType<WorldEventManager>();
            anim = GetComponentInParent<Animator>();
        }

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            playerManager.PushHeavyDoor(transform, playerStandsWhenPushDoor);




            if (anim != null && anim.CompareTag("Entrance"))
            {
                anim.Play("EntranceOpen");
                this.gameObject.SetActive(false);
            }

        }
    }
}


