using UnityEngine;
using UnityEngine.UI;

public class AudioManagerMenu : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic; // Reference to the background music AudioSource
    [SerializeField] private Slider volumeSlider;         // Reference to the UI slider

    private const string VolumePrefKey = "BackgroundVolume"; // Key to save and load volume

    void Start()
    {
        // Load the saved volume or set default to 0.5 (50%)
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 0.5f);
        backgroundMusic.volume = savedVolume;

        // Set slider value to the loaded volume
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;

            // Add listener to handle slider value changes
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    /// <summary>
    /// Updates the volume of the background music and saves it.
    /// </summary>
    /// <param name="volume">New volume value from the slider</param>
    public void SetVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
        }

        // Save the new volume setting
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
    }

    void OnDestroy()
    {
        // Ensure to remove listener to avoid memory leaks
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }
    }
}
