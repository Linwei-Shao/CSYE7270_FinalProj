using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class SoulCount : MonoBehaviour
    {
        public Text soulCount;
        public void SetCurrentSoul(int currentSoul)
        {
            soulCount.text = currentSoul.ToString() + " ";
        }
    }
}

