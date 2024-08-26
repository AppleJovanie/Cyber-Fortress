using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] AudioSource SfxSource;
    [SerializeField] AudioSource musicSource;


    [Header("AudioClip")]
    public AudioClip background;
    public AudioClip bulletImpact;
    public AudioClip LaserEffect;
    public AudioClip UpgradeEffect;
    public AudioClip SellEffect;
    public AudioClip BuildingEffect;

    private void Start()
    {
        musicSource.clip= background;
        musicSource.Play();
    }

    public void PlaySfx(AudioClip clip, float volume = 1.0f)
    {
        SfxSource.PlayOneShot(clip);
    }
    
}
