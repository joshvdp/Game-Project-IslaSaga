using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BGMusic : MonoBehaviour
{
   static public BGMusic instance;
   private AudioSource source;

   private void Awake()
   {
      instance = this;
      DontDestroyOnLoad(gameObject);
   }

   private void Start()
   {
      source = GetComponent<AudioSource>();
      source.volume = 0f;
      StartCoroutine(Fade(true, source, 2f, 1f));
      StartCoroutine(Fade(false, source, 2f, 0f));
      
   }

   private void Update()
   {
      if (!source.isPlaying)
      {
         source.Play();
         StartCoroutine(Fade(true, source, 2f, 1f));
         StartCoroutine(Fade(false, source, 2f, 0f));
      }
   }

   public IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVolume)
   {
      if (!fadeIn)
      {
         double lengthOfSource = (double)source.clip.samples / source.clip.frequency;
         yield return new WaitForSecondsRealtime((float)(lengthOfSource - duration));
      }

      float time = 0f;
      float startVol = source.volume;
      while (time < duration)
      {
         //string fadeSituation = fadeIn ? "fadeIn" : "fadeOut";
         //Debug.Log(fadeSituation);
         time += Time.deltaTime;
         source.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
         yield return null;

      }
      yield break;
   }

   public void ChangeTrack(AudioClip clip)
   {
      StopAllCoroutines();
      source.clip = clip;
      source.volume = 0f;
      StartCoroutine(Fade(true, source, 2f, 1f));
      StartCoroutine(Fade(false, source, 2f, 0f));
   }

   public AudioClip GetTrack()
   {
      return source.clip;
   }
}
