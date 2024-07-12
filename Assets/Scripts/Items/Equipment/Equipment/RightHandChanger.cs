using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class RightHandChanger : MonoBehaviour
    {
        public List<GameObject> rightHandModels;

        private void Awake()
        {
            GetAllRightHandModels();

        }

        private void GetAllRightHandModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                rightHandModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllRightHandModels()
        {
            foreach (GameObject rightHandModel in rightHandModels)
            {
                rightHandModel.SetActive(false);
            }
        }

        public void EquipmentRightHandModelByName(string rightHandName)
        {
            for (int i = 0; i < rightHandModels.Count; i++)
            {
                if (rightHandModels[i].name == rightHandName)
                {
                    rightHandModels[i].SetActive(true);
                }

            }
        }
    }

}