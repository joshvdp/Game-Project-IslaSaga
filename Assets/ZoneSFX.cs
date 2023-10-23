using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ZoneSFX : MonoBehaviour
{
    [SerializeField]
    private AudioClip theme, previousTheme;

    private void OnTriggerEnter(Collider other)
    {
        previousTheme = BGMusic.instance.GetTrack();
        if (other.CompareTag("Player"))
        {
            BGMusic.instance.ChangeTrack(theme);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BGMusic.instance.ChangeTrack(previousTheme);
        }
    }
}
