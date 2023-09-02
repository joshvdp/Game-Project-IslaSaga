using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeMan : MonoBehaviour
{

    public delegate void VolumeEvent();
    public static VolumeEvent save;
    public static VolumeEvent load;

    public delegate void VolumeChangeSearch(string name, float value);
    public static VolumeChangeSearch volumeChangeSearch;

    public AudioMixer audioMixer;
    public const string MusicVol = "MusVol";

    private int music;


    private void OnEnable()
    {
        save += SaveVolume;
        load += LoadVolume;
        volumeChangeSearch += changeVolume;
    }
    private void OnDisable()
    {
        save -= SaveVolume;
        load -= LoadVolume;
        volumeChangeSearch -= changeVolume;
    }

    private void Start()
    {
        LoadVolume();
        Debug.Log("Load");
    }

    private void OnApplicationQuit()
    {
        SaveVolume();
    }

    private void SetMusicVolume(float value)
    {
        music = Mathf.RoundToInt(value * 10);
        audioMixer.SetFloat(MusicVol, Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20);
    }
    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(MusicVol, music);
    }
    private void LoadVolume()
    {
        SetMusicVolume(PlayerPrefs.GetFloat(MusicVol, 1f) / 10);
    }
    private void changeVolume(string mixerName, float value)
    {
        switch (mixerName)
        {
            case MusicVol:
                SetMusicVolume(value);
                Debug.Log("Work");
                break;
        }
    }
}
