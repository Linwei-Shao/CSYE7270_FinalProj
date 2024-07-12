using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("Lock On Transform")]
        public Transform lockOnTransform;

        [Header("Combat Collider")]
        public CriticalCollider backStabCollider;
        public CriticalCollider riposteCollider;

        [Header("Combat Flags")]
        public bool canBeRiposted;
        public bool isParrying;
        public bool canBeParried;
        public bool isBlocking;

        public bool isRotatingWithRoot;
        public bool canRotate;

        public int pendingCriticalDamage;

        public bool isGolem = false;
    }

}
