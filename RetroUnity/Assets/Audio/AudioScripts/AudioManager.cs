using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioSource loseNoise;

    [SerializeField] private AudioClip[] bounceyFun;
    
    public static AudioManager instance = null;

    public float lowPitchRange = .9f;
    public float highPitchRange = 1.1f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[]clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        sfxSource.pitch = randomPitch;
        sfxSource.clip = clips[randomIndex];
        sfxSource.Play();
    }

    public void PlayWhiteNoise()
    {
        loseNoise.Play();
    }

    public void playBounceyFunOne()
    {
        sfxSource.PlayOneShot(bounceyFun[1]);
    }

}
