using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class HipChanger : MonoBehaviour
    {
        public List<GameObject> hipModels;

        private void Awake()
        {
            GetAllHipModels();

        }

        private void GetAllHipModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                hipModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllHipModels()
        {
            foreach (GameObject hipModel in hipModels)
            {
                hipModel.SetActive(false);
            }
        }

        public void EquipmentHipModelByName(string hipName)
        {
            for (int i = 0; i < hipModels.Count; i++)
            {
                if (hipModels[i].name == hipName)
                {
                    hipModels[i].SetActive(true);
                }

            }
        }
    }

}