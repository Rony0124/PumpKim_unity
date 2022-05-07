using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMng : MonoBehaviour
{
    public float volume = 0.9f;

    #region Static Instance
    private static AudioMng instance;
    public static AudioMng Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<AudioMng>();
                if(instance == null)
                {
                    instance = new GameObject("Spawned AudioMng", typeof(AudioMng)).GetComponent<AudioMng>();
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    #endregion

    #region Fields
    public AudioSource musicSource;
    public AudioSource musicSource2;
    public AudioSource sfxSource;
    
    private bool firstMusicSourcePlaying;
    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
        }else if(instance != null)
        {
            Destroy(gameObject); 
        }
       
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource2.loop = true;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource = (firstMusicSourcePlaying) ? musicSource : musicSource2;
        activeSource.clip = musicClip;
        activeSource.volume = musicSource.volume;
        activeSource.Play();
        SetMusicVolume(volume);
    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (firstMusicSourcePlaying) ? musicSource : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (firstMusicSourcePlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourcePlaying) ? musicSource2 : musicSource;

        firstMusicSourcePlaying = !firstMusicSourcePlaying;

        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }
    IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if (!activeSource.isPlaying)
        {
            activeSource.Play();
        }
        float t = 0.0f;
        for (t = 0; t < transitionTime; t+=Time.deltaTime)
        {
            activeSource.volume = (1-(t/ transitionTime));
            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume =  (t / transitionTime) * musicSource.volume;
            yield return null;
        }
    }
    IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
       
        float t = 0.0f;
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }
        original.Stop();
        
    }
    public void StopMusic ()
    {
        AudioSource activeSource = (firstMusicSourcePlaying) ? musicSource : musicSource2;
        //activeSource.clip = clip;
        activeSource.volume = musicSource.volume;
        activeSource.Stop();
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicSource2.volume = volume;
    }    
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
