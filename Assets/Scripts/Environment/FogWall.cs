using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class FogWall : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(true);
        }

        public void ActiveFogWall()
        {
            gameObject.SetActive(true);
        }

        public void DeactiveFogWall()
        {
            gameObject.SetActive(false);
        }
    }
}
