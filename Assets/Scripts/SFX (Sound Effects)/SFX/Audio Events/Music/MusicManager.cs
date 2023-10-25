using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] public AudioSource musicSource;

        [Header("-----  Music List   -----")]
        public AudioClip Track1;

        private void Start()
        {
            musicSource.clip = Track1;
            musicSource.Play();
        }
    
    }
}

