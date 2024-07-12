using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class BossRoomCollider : MonoBehaviour
    {
        WorldEventManager worldEventManager;

        private void Awake()
        {
            worldEventManager = FindObjectOfType<WorldEventManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                worldEventManager.ActiveBossFight();
                this.gameObject.SetActive(false);
            }
        }
    }

}
