using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class FootSteps : MonoBehaviour
    {
        [SerializeField] 
        public AudioClip[] clips;

        public AudioSource audioSource;
        public void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void Step()
        {
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
            Debug.Log(clip);
        }

        public void RunStep()
        {
            Debug.Log("runSteps");
        }

        public AudioClip GetRandomClip()
        {
            return clips[UnityEngine.Random.Range(0, clips.Length)];
        }
    }
}

