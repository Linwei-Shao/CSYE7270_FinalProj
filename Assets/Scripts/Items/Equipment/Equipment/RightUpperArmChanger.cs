using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class RightUpperArmChanger : MonoBehaviour
    {
        public List<GameObject> rightUpperArmModels;

        private void Awake()
        {
            GetAllRightUpperArmModels();

        }

        private void GetAllRightUpperArmModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                rightUpperArmModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllRightUpperArmModels()
        {
            foreach (GameObject rightUpperArmModel in rightUpperArmModels)
            {
                rightUpperArmModel.SetActive(false);
            }
        }

        public void EquipmentRightUpperArmModelByName(string rightUpperArmName)
        {
            for (int i = 0; i < rightUpperArmModels.Count; i++)
            {
                if (rightUpperArmModels[i].name == rightUpperArmName)
                {
                    rightUpperArmModels[i].SetActive(true);
                }

            }
        }
    }

}