using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource SfxSource;
    [SerializeField] private AudioSource musicSource;
    public AudioSource laser;

    [Header("AudioClip")]
    public AudioClip background;
    public AudioClip bulletImpact;
    public AudioClip UpgradeEffect;
    public AudioClip SellEffect;
    public AudioClip BuildingEffect;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensures AudioManager persists across scenes
        }
    }

    private void Start()
    {
        PlayBackgroundMusic(); // Play initial background music
    }

    private void OnEnable()
    {
        // Resume background music if it's stopped when returning to the scene
        if (!musicSource.isPlaying)
        {
            PlayBackgroundMusic();
        }
    }

    // Play background music
    public void PlayBackgroundMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = background; // Use the predefined background clip
            musicSource.Play();
        }
    }



    // Stop background music
    public void StopBackgroundMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    // Change background music if different from current
    public void ChangeBackgroundMusic(AudioClip newClip)
    {
        if (musicSource.clip != newClip)
        {
            musicSource.clip = newClip;
            musicSource.Play();
        }
    }

    // Play sound effects
    public void PlaySfx(AudioClip clip, float volume = 1.0f)
    {
        SfxSource.PlayOneShot(clip, volume);
    }

    // Stop specific sound effect if playing
    public void StopSfx(AudioClip clip)
    {
        if (SfxSource.isPlaying && SfxSource.clip == clip)
        {
            SfxSource.Stop();
        }
    }
}
