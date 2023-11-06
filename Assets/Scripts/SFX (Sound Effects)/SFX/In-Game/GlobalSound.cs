using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class GlobalSound : MonoBehaviour
    {
        [Header("MIXER CATEGORY: ")]
        [SerializeField] public AudioSource Music;
        [SerializeField] public AudioSource SFX;
        [SerializeField] public AudioSource FootSteps;
        

        #region IN-GAME MUSIC
        [Header("IN-GAME MUSIC: ")]
        public AudioClip music1;
        public AudioClip music2;

        public void Start()
        {
            
        }

        #endregion
        
        #region MAP



        #endregion

        #region ENEMY



        #endregion

    }
}

