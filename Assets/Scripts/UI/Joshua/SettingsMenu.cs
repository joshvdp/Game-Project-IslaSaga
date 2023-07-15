using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void MainVol(float mainVolume)
    {
        audioMixer.SetFloat("VolMain", mainVolume);
    }
    
    public void MusicVol(float musicVolume)
    {
        audioMixer.SetFloat("MusVol", musicVolume);
    }
    public void SFXVol(float volumeSfx)
    {
        audioMixer.SetFloat("SfxVol", volumeSfx);
    }
}
