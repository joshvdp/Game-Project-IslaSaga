using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundFX
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] public AudioSource SFX;
        
        [Header("-----  Buttons SFX    -----")]
        public AudioClip clickSelect;
        public AudioClip clickBack;
        
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

