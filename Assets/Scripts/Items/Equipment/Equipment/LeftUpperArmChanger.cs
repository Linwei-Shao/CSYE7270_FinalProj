using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class LeftUpperArmChanger : MonoBehaviour
    {
        public List<GameObject> leftUpperArmModels;

        private void Awake()
        {
            GetAllLeftUpperArmModels();

        }

        private void GetAllLeftUpperArmModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                leftUpperArmModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllLeftUpperArmModels()
        {
            foreach (GameObject leftUpperArmModel in leftUpperArmModels)
            {
                leftUpperArmModel.SetActive(false);
            }
        }

        public void EquipmentLeftUpperArmModelByName(string leftUpperArmName)
        {
            for (int i = 0; i < leftUpperArmModels.Count; i++)
            {
                if (leftUpperArmModels[i].name == leftUpperArmName)
                {
                    leftUpperArmModels[i].SetActive(true);
                }

            }
        }
    }

}