using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class InGameMusic : MonoBehaviour
{
    public AudioSource CurrentMusic;
    public AudioSource NextMusic;

    private void Start()
    {
        CurrentMusic.Play();
    }

    public void OnEntry ()
    {
        CurrentMusic.Stop();
        NextMusic.Play();
    }

    public void OnExit()
    {
        NextMusic.Stop();
        CurrentMusic.Play();
    }
    
}
