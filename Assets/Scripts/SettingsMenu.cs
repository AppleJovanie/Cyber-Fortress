using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource backgroundMusic; // Reference to the background music AudioSource
    [SerializeField] private Slider musicSlider; // Slider to control music volume

    private void Start()
    {
        // Initialize the slider value based on the current background music volume
        musicSlider.value = backgroundMusic.volume;

        // Add a listener to the slider to adjust volume in real-time
        musicSlider.onValueChanged.AddListener(delegate { SetVolume(); });

        // Ensure background music plays
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    public void SetVolume()
    {
        // Adjust the background music volume based on the slider's value (0 to 1)
        backgroundMusic.volume = musicSlider.value;
    }
}
