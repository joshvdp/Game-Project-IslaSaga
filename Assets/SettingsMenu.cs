using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void MusicVol(float musicVolume)
    {
        audioMixer.SetFloat("MusVol", musicVolume);
        Debug.Log(musicVolume);
    }
    public void SFXVol(float volumeSfx)
    {
        audioMixer.SetFloat("SfxVol", volumeSfx);
        Debug.Log(volumeSfx);
    }
}
