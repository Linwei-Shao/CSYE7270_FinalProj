using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class PhaseShiftingCollider : MonoBehaviour
    {
        public float finalRadius = 5;
        public float increaseSpeed = 1;
        public float original_radius;

        private CapsuleCollider collider;
        EnemyManager enemyManager;
        Rigidbody rigidbody;

        private void Awake()
        {
            collider = GetComponent<CapsuleCollider>();
            enemyManager = GetComponent<EnemyManager>();
            rigidbody = GetComponent<Rigidbody>();
            
        }

        private void Start()
        {
            original_radius = collider.radius;
        }
        private void LateUpdate()
        {

            float newRadius = collider.radius;

            if (enemyManager.isPhaseShifting)
            {
                rigidbody.constraints = RigidbodyConstraints.FreezePosition;

                if (newRadius < finalRadius)
                {
                    newRadius += increaseSpeed * Time.deltaTime;

                    if (newRadius > finalRadius)
                    {
                        newRadius = finalRadius;
                    }

                    collider.radius = newRadius;
                }
            }
            else
            {
                rigidbody.constraints = RigidbodyConstraints.None;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                collider.radius = original_radius;
            }
        }
    }
}

