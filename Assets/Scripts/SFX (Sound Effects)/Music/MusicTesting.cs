using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTesting : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 1.0f;

    private float initialVolume;

    
    public void StartFadeIn()
    {
        initialVolume = audioSource.volume;
        audioSource.volume = 0.0f;
        audioSource.Play();
        StartCoroutine(FadeAudioIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeAudioOut());
    }

    IEnumerator FadeAudioIn()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(0, initialVolume, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = initialVolume;
    }

    IEnumerator FadeAudioOut()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(initialVolume, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0;
    }
}
