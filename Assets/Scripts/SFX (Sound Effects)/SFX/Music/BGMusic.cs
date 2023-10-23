using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace AudioSoundEvents
{
    public class BGMusic : MonoBehaviour
    {
        public AudioClip defaultAmbience;

        private AudioSource track01, track02;
        private bool isPlayingTrack01;

        public static BGMusic instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            track01 = gameObject.AddComponent<AudioSource>();
            track02 = gameObject.AddComponent<AudioSource>();
            isPlayingTrack01 = true;

            SwapTrack(defaultAmbience);
        }

        public void SwapTrack(AudioClip newClip)
        {
            
           StopAllCoroutines();
           StartCoroutine(FadeTrack(newClip));

            isPlayingTrack01 = !isPlayingTrack01;
        }

        public void ReturnToDefault()
        {
            SwapTrack(defaultAmbience);
        }

        private IEnumerator FadeTrack(AudioClip newClip)
        {
            float timeToFade = 1.25f;
            float timeElapsed = 0;
            
            if (isPlayingTrack01)
            {
                track02.clip = newClip;
                track02.Play();
                while (timeElapsed < timeToFade)
                {
                    track02.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                    track01.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
                track01.Stop();
            }
            else
            {
                
                track01.clip = newClip;
                track01.Play();
                while (timeElapsed < timeToFade)
                {
                    track01.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                    track02.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
                track02.Stop();
            }
        }
    }
}
