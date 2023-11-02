using System;
using System.Collections;
using AudioSoundEvents;
using UnityEngine;
using UnityEngine.Events;

public class ZoneSFX : MonoBehaviour
{
   
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnExit.Invoke();
        }
    }

    
}