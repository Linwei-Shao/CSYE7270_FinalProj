using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class DestoryAfterTime : MonoBehaviour
    {
        public float timeUntilDestroyed = 3;

        private void Awake()
        {
            Destroy(gameObject, timeUntilDestroyed);
        }

    }
}

