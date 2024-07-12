using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS {
    public class PuzzleInteractable : Interactable
    {
        Puzzle puzzle;
        PlayerAnimatorManager playerAnimatorManager;

        public bool isActived;

        public int thisOrder;

        private void Awake()
        {
            puzzle = GetComponentInParent<Puzzle>();
            playerAnimatorManager = FindObjectOfType<PlayerAnimatorManager>();
        }
        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            if (isActived)
                return;

            isActived = true;
            playerAnimatorManager.PlayTargetAnimation("Kneel", true);

            puzzle.interactOrder = puzzle.interactOrder + thisOrder;

            puzzle.HandlePuzzle();
        }
    }
}

