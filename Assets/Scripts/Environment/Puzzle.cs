using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS{
    public class Puzzle : MonoBehaviour
    {
        public GameObject angleFire;
        public GameObject deathFire;
        public GameObject chest;
        public PuzzleInteractable knight01;
        public PuzzleInteractable knight02;
        public PuzzleInteractable knight03;
        public PuzzleInteractable knight04;
        public PuzzleInteractable angel;
        public PuzzleInteractable death;

        public int interactOrder;


        public void HandlePuzzle()
        {
            if (interactOrder != 0 && interactOrder != 1 && interactOrder != 3 && interactOrder != 6 && interactOrder != 10 && interactOrder != 15 && interactOrder != 21)
            {
                ResetPuzzle();
            }
            else if(interactOrder == 15)
            {
                angleFire.SetActive(true);
                return;
            }
            else if (interactOrder == 21)
            {
                deathFire.SetActive(true);
                chest.SetActive(true);
            }
        }

        public void ResetPuzzle()
        {
            interactOrder = 0;
            knight01.isActived = false;
            knight02.isActived = false;
            knight03.isActived = false;
            knight04.isActived = false;
            angel.isActived = false;
            death.isActived = false;
        }
    }

}
