using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeController : MonoBehaviour
{
    public string mixerName;

    [SerializeField] Slider musicSlider;

    private int volume = 5;

    private int maxVolume = 10;
    public float Volume => Muted ? 0 : (float)volume / 10;
    public bool Muted => volume == 0;

    private void OnEnable()
    {
        SetValue(Mathf.RoundToInt(PlayerPrefs.GetFloat(mixerName, 1)));
    }
    private void OnDisable()
    {
        VolumeMan.save?.Invoke();
    }
    public void Addby(int value)
    {
        volume += value;
        volume = Mathf.Clamp(volume, 0, maxVolume);
        Debug.Log("Add");
        VolumeMan.volumeChangeSearch?.Invoke(mixerName, Volume);
    }

    public void SetValue(int value)
    {
        volume = Mathf.Clamp(value, 0, maxVolume);
        VolumeMan.volumeChangeSearch?.Invoke(mixerName, Volume);
    }

}
