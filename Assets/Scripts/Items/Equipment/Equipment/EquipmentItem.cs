using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class EquipmentItem : Item
    {
        [Header("Defense Bonus")]
        public float physicalDef;
        public float fireDef;

        public bool isHelmet;
        public bool isTorso;
        public bool isHand;
        public bool isLeg;

    }
}

