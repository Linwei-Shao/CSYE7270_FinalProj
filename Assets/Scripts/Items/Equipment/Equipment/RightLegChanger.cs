using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class RightLegChanger : MonoBehaviour
    {
        public List<GameObject> rightLegModels;

        private void Awake()
        {
            GetAllRightLegModels();

        }

        private void GetAllRightLegModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                rightLegModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllRightLegModels()
        {
            foreach (GameObject rightLegModel in rightLegModels)
            {
                rightLegModel.SetActive(false);
            }
        }

        public void EquipmentRightLegModelByName(string rightLegName)
        {
            for (int i = 0; i < rightLegModels.Count; i++)
            {
                if (rightLegModels[i].name == rightLegName)
                {
                    rightLegModels[i].SetActive(true);
                }

            }
        }
    }

}