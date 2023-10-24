using System;
using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource CurrentMusic;
    public AudioSource NextMusic;
    public float fadeTime = 2f;

    private void Start()
    {
        CurrentMusic.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeOut(CurrentMusic, fadeTime));
            StartCoroutine(FadeIn(NextMusic, fadeTime));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeOut(NextMusic, fadeTime));
            StartCoroutine(FadeIn(CurrentMusic, fadeTime));
        }
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float endVolume = audioSource.volume;
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < endVolume)
        {
            audioSource.volume += endVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}