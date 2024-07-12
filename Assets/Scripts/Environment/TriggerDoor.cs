using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class TriggerDoor : MonoBehaviour
    {
        public GameObject saw;
        Animator anim;

        private void Awake()
        {
            anim = saw.GetComponent<Animator>();
        }
       


        private void OnTriggerEnter(Collider other)
        {
            anim.Play("SawShoot");
            

        }

        
    }

}
