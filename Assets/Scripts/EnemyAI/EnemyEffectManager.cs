using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class EnemyEffectManager : MonoBehaviour
    {
        public GameObject bloodFX;
        public GameObject rockFX;

        public void PlayBloodFX(Vector3 bloodLocation)
        {
            GameObject blood = Instantiate(bloodFX, bloodLocation, Quaternion.identity);
        }
        public void PlayRockFX(Vector3 rockLocation)
        {
            GameObject rock = Instantiate(rockFX, rockLocation, Quaternion.identity);
        }

    }
}