using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LS
{
    public class BGMController : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip normalBGM;
        public AudioClip bossBGM;
        public AudioClip enemyFelledSound;
        public AudioClip diedSound;

        private void Start()
        {
            audioSource.clip = normalBGM;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void PlayDiedSound()
        {
            audioSource.Stop();
            audioSource.PlayOneShot(diedSound);
        }

        public void PlayBossBGM()
        {
            audioSource.Stop();
            audioSource.clip = bossBGM;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void PlayEnemyFelledSound()
        {
            audioSource.Stop();
            audioSource.PlayOneShot(enemyFelledSound);
            audioSource.clip = normalBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
