using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else 
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        if (musicSlider == null) return;
        float volume = musicSlider.value;
        myMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);

    }
    public void SetSFXVolume()
    {
        if (sfxSlider == null) return;
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);

    }
    private void LoadVolume()
    {
        GetMusicVolValFromPrefs();
        GetSFXValFromPrefs();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void GetMusicVolValFromPrefs()
    {
        if (musicSlider == null) return;
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void GetSFXValFromPrefs()
    {
        if (sfxSlider == null) return;
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
}
