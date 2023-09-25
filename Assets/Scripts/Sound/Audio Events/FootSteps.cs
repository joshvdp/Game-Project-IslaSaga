using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    public AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
