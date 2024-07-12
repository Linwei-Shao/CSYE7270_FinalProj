using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class LeftLowerArmChanger : MonoBehaviour
    {
        public List<GameObject> leftLowerArmModels;

        private void Awake()
        {
            GetAllLeftLowerArmModels();

        }

        private void GetAllLeftLowerArmModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                leftLowerArmModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllLeftLowerArmModels()
        {
            foreach (GameObject leftLowerArmModel in leftLowerArmModels)
            {
                leftLowerArmModel.SetActive(false);
            }
        }

        public void EquipmentLeftLowerArmModelByName(string leftLowerArmName)
        {
            for (int i = 0; i < leftLowerArmModels.Count; i++)
            {
                if (leftLowerArmModels[i].name == leftLowerArmName)
                {
                    leftLowerArmModels[i].SetActive(true);
                }

            }
        }
    }

}