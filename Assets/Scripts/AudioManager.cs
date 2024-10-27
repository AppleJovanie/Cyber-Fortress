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
            DontDestroyOnLoad(gameObject); // Makes sure AudioManager is not destroyed on scene load
        }
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySfx(AudioClip clip, float volume = 1.0f)
    {
        SfxSource.PlayOneShot(clip, volume);
    }

    public void StopSfx(AudioClip clip)
    {
        if (SfxSource.isPlaying && SfxSource.clip == clip)
        {
            SfxSource.Stop();
        }
    }
}
