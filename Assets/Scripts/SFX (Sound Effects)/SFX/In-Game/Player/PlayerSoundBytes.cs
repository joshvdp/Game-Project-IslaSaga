using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class PlayerSoundBytes : MonoBehaviour
    {
        [SerializeField] 
        public AudioClip[] clips;

        public AudioSource audioSource;
        public void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void Start()
        {
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
            Debug.Log("CHARACTER VOICE");
        }

        public void Interact()
        {
            Debug.Log("Interact");
        }

        public AudioClip GetRandomClip()
        {
            return clips[UnityEngine.Random.Range(0, clips.Length)];
        }
    }
}