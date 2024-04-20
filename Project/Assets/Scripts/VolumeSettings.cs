using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour // This class manages the audio settings for music and sound effects (SFX) through a user interface.
{
    public Slider volumeSlider; // UI slider for adjusting music volume.
    public Slider sfxSlider; // UI slider for adjusting SFX volume.
    public GameObject settingsPanel; // Panel that contains the settings UI.
    public GameObject settingsBTN; // Button to open the settings panel.
    public List<AudioSource> sfxAudioSources = new List<AudioSource>(); // List of all SFX audio sources.
    public List<AudioSource> musicAudioSources = new List<AudioSource>(); // List of all music audio sources.
    public GameObject PauseMenu; // Reference to the pause menu object.


    void Start()
    { // Retrieve and set the saved music volume from PlayerPrefs or default to 1 if not set.
        float savedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);  // sets music volume to what player adjusted it to
        volumeSlider.value = savedMusicVolume;
        foreach (AudioSource musicSource in musicAudioSources)
        {
            musicSource.volume = savedMusicVolume; // Apply the volume to each music audio source.
        }
        // Retrieve and set the saved SFX volume from PlayerPrefs or default to 1 if not set.
        float savedSFXVolume = PlayerPrefs.GetFloat("sfxVolume", 1f); // sets SFX volume to what player adjusted it to
        sfxSlider.value = savedSFXVolume;
        foreach (AudioSource sfxSource in sfxAudioSources)
        {
            sfxSource.volume = savedSFXVolume; // Apply the volume to each SFX audio source.
        }
        // Initially hide the settings panel and show the settings button.
        settingsPanel.SetActive(false);
        settingsBTN.SetActive(true);
    }


    public void OnMusicVolumeChanged() // updates music volume
    {
        float newVolume = volumeSlider.value; // Get the new volume from the slider.
        Debug.Log("Music volume changed to: " + newVolume);
        foreach (AudioSource musicSource in musicAudioSources)
        {
            if (musicSource != null)
            {
                musicSource.volume = newVolume; // Set the new volume for each music source.
                Debug.Log("Setting volume for source: " + musicSource.gameObject.name);
            }
        }
        PlayerPrefs.SetFloat("musicVolume", newVolume); // Save the new volume setting.
    }

    public void OnSFXVolumeChanged() // updates the SFX volume
    {
        foreach (AudioSource sfxSource in sfxAudioSources)
        {
            if (sfxSource != null)
            {
                sfxSource.volume = sfxSlider.value; // Set the new volume for each SFX source.
                PlayerPrefs.SetFloat("sfxVolume", sfxSource.volume); // Save the new volume setting.
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
