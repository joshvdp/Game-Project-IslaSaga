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
        public void Awake()
        {
            Music.clip = music1;
            Music.Play();
        }

        #endregion
        #region PLAYER

        [Header("PLAYER: ")] 
        public AudioClip FootStep;
        public AudioClip Grunt;
        public AudioClip Death;
        public AudioClip Jump;
        public AudioClip Landed;

        public void Moving()
        {
            FootSteps.PlayOneShot(FootStep);
            Debug.Log("Im Walking");
        }

        #endregion

        #region MAP



        #endregion

        #region ENEMY



        #endregion

    }
}

