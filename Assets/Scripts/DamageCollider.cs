using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager characterManager;
        ParticleSystem particle;
        Collider damageCollider;

        public bool enableOnStartUp = false;

        public int currentWeaponDamage = 25;
        public bool isFireDamage = false;
        public Vector3 size;
        public Vector3 center;

        private void Start()
        {
            particle = GetComponentInChildren<ParticleSystem>();
            particle.Stop();

        }
        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = enableOnStartUp;
        }

        public void EnableParticleEffect()
        {
            particle.Play();
        }

        public void DisableParticleEffect()
        {
            particle.Stop();
        }
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                PlayerStats playerStats = collision.GetComponentInParent<PlayerStats>();
                CharacterManager playerCharacterManager = collision.GetComponentInParent<CharacterManager>();
                PlayerEffectsManager playerEffectManager = collision.GetComponentInParent<PlayerEffectsManager>();
                BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();
                InputHandler inputHandler = collision.GetComponentInParent<InputHandler>();
                PlayerAnimatorManager playerAnimatorManager = collision.GetComponentInParent<PlayerAnimatorManager>();

                Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                


                if (playerCharacterManager != null)
                {
                    if (playerCharacterManager.isParrying)
                    {
                        characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }
                    else if (shield != null && playerCharacterManager.isBlocking)
                    {
                        float damageAfterBlock = currentWeaponDamage - (currentWeaponDamage * shield.blockingDamageAbsorption) / 100;

                        if (playerStats != null)
                        {
                            if (playerStats.currentStamina >= currentWeaponDamage)
                            {
                                if (inputHandler.twoHandFlag)
                                {
                                    playerStats.TakeStaminaDrain(currentWeaponDamage);
                                    playerStats.TakeDamage(Mathf.RoundToInt(damageAfterBlock), "Block2H_Damage");
                                }
                                else
                                {
                                    playerStats.TakeStaminaDrain(currentWeaponDamage);
                                    playerStats.TakeDamage(Mathf.RoundToInt(damageAfterBlock), "Block_Damage");
                                }
                            }
                            else
                            {
                                playerCharacterManager.isBlocking = false;
                                inputHandler.bl_Input = false;
                                playerAnimatorManager.anim.SetBool("isBlocking", false);

                                playerEffectManager.PlayBrokenFX(contactPoint);

                                Vector3 targetPosition = playerStats.transform.position - playerStats.transform.forward * 5f;


                                playerStats.transform.position = Vector3.Lerp(playerStats.transform.position, targetPosition, Time.deltaTime * 10f);


                                playerStats.TakeDamage(currentWeaponDamage, "Guard_Broken");
                            }
                            return;
                        }
                    }
                }

                playerEffectManager.PlayBloodFX(contactPoint);

                if (playerStats != null)
                {
                    if(isFireDamage)
                    {
                        playerStats.TakeFireDamage(currentWeaponDamage);
                    }
                    else
                    {
                        playerStats.TakeDamage(currentWeaponDamage);
                    }
                    
                }
            }

            if (collision.tag == "Enemy")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
                CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
                EnemyEffectManager enemyEffectManager = collision.GetComponentInChildren<EnemyEffectManager>();
                BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();
                PlayerStats playerStats = FindObjectOfType<PlayerStats>();

                if (enemyCharacterManager != null)
                {
                    if (enemyCharacterManager.isParrying)
                    {
                        characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }
                    else if (shield != null && enemyCharacterManager.isBlocking)
                    {
                        float damageAfterBlock = currentWeaponDamage - (currentWeaponDamage * shield.blockingDamageAbsorption) / 100;

                        if (enemyStats != null)
                        {
                            enemyStats.TakeDamage(Mathf.RoundToInt(damageAfterBlock), "Block_Damage");
                            return;
                        }
                    }
                }

                Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                if (enemyCharacterManager.isGolem)
                {
                    enemyEffectManager.PlayRockFX(contactPoint);
                }
                else
                {
                    enemyEffectManager.PlayBloodFX(contactPoint);
                }

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(Mathf.RoundToInt(currentWeaponDamage * (1f + (playerStats.strengthLevel / 30f) * (playerStats.strengthLevel / 30f))));
                }
            }

            if (collision.tag == "IllusionaryWall")
            { 
                IllusionaryWall illusionaryWall = collision.GetComponent<IllusionaryWall>();

                illusionaryWall.beHit = true;
            }
        }
    }
}

