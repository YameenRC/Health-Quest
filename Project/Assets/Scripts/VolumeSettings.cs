using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sfxSlider;
    public GameObject settingsPanel;
    public GameObject settingsBTN;
    public List<AudioSource> sfxAudioSources = new List<AudioSource>();
    public List<AudioSource> musicAudioSources = new List<AudioSource>();
    public GameObject PauseMenu;


    void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);  // sets music volume to what player adjusted it to
        volumeSlider.value = savedMusicVolume;
        foreach (AudioSource musicSource in musicAudioSources)
        {
            musicSource.volume = savedMusicVolume;
        }

        float savedSFXVolume = PlayerPrefs.GetFloat("sfxVolume", 1f); // sets SFX volume to what player adjusted it to
        sfxSlider.value = savedSFXVolume;
        foreach (AudioSource sfxSource in sfxAudioSources)
        {
            sfxSource.volume = savedSFXVolume;
        }

        settingsPanel.SetActive(false);
        settingsBTN.SetActive(true);
    }


    public void OnMusicVolumeChanged() // updates music volume
    {
        float newVolume = volumeSlider.value;
        Debug.Log("Music volume changed to: " + newVolume);
        foreach (AudioSource musicSource in musicAudioSources)
        {
            if (musicSource != null)
            {
                musicSource.volume = newVolume;
                Debug.Log("Setting volume for source: " + musicSource.gameObject.name);
            }
        }
        PlayerPrefs.SetFloat("musicVolume", newVolume);
    }

    public void OnSFXVolumeChanged() // updates the SFX volume
    {
        foreach (AudioSource sfxSource in sfxAudioSources)
        {
            if (sfxSource != null)
            {
                sfxSource.volume = sfxSlider.value;
                PlayerPrefs.SetFloat("sfxVolume", sfxSource.volume);
            }
        }
    }

    public void ShowSettingsPage() // shows setting page
    {
        settingsPanel.SetActive(true);
        settingsBTN.SetActive(false);
        PauseMenu.SetActive(false);
    }
    public void CloseSettingsPage() // closes setting page
    {
        settingsPanel.SetActive(false);
        settingsBTN.SetActive(true);
        PauseMenu.SetActive(true);
    }

}
