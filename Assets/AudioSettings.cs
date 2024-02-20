using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider audioSlider;

    public void UpdateMasterVolume()
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(masterSlider.value) * 20);
    }
    public void UpdateAudienceVolume()
    {
        audioMixer.SetFloat("AudienceVol", Mathf.Log10(masterSlider.value) * 20);
    }
    public void UpdateMusicGroup()
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(musicSlider.value) * 20);
    }
}
