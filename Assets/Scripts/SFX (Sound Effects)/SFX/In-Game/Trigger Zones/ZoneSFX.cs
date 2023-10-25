using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace AudioSoundEvents
{
    public class ZoneSFX : MonoBehaviour
    {
        public AudioClip newTrack;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BGMusic.instance.SwapTrack(newTrack);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BGMusic.instance.ReturnToDefault();
            }
        }
    }
}