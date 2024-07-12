using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class LeftHandChanger : MonoBehaviour
    {
        public List<GameObject> leftHandModels;

        private void Awake()
        {
            GetAllLeftHandModels();

        }

        private void GetAllLeftHandModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                leftHandModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllLeftHandModels()
        {
            foreach (GameObject leftHandModel in leftHandModels)
            {
                leftHandModel.SetActive(false);
            }
        }

        public void EquipmentLeftHandModelByName(string leftHandName)
        {
            for (int i = 0; i < leftHandModels.Count; i++)
            {
                if (leftHandModels[i].name == leftHandName)
                {
                    leftHandModels[i].SetActive(true);
                }

            }
        }
    }

}