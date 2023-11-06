using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundFX
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] public AudioSource musicSource;
        [SerializeField] public AudioSource SFX;

        [Header("-----  Music   -----")]
        public AudioClip music;

        [Header("-----  SFX    -----")]
        public AudioClip clickSelect;
        public AudioClip clickBack;

        private void Start()
        {
            musicSource.clip = music;
            musicSource.Play();
        }
        public void onClick()
        {
            SFX.PlayOneShot(clickSelect);
        }
        public void onClickBack()
        {
            SFX.PlayOneShot(clickBack);
        }

    }
}

