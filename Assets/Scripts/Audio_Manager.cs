using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SoundName
{
    ButtonClick,
    Background,
    FoodGainer,
    FoodBurner,
    PowerUps,
    GameOver

}

[Serializable]
public class AudioType
{
    public SoundName soundName;
    public AudioClip soundClip;
}



public class Audio_Manager : MonoBehaviour
{
    [SerializeField] private AudioSource BG;
    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioType[] Audio;
    

    public static Audio_Manager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        PlayBG(SoundName.Background);
        
    }

    public void PlayBG(SoundName sound)
    {
        AudioClip audioClip = getAudioClip(sound);
        if (audioClip != null)
        {
            BG.clip = audioClip;
            BG.Play();
        }
        else
        {
            Debug.Log("Audioclip not found");
        }

    }

    public void Play(SoundName sound)
    {
        AudioClip audioClip = getAudioClip(sound);
        if (audioClip != null)
        {
            SFX.clip = audioClip;
            SFX.PlayOneShot(audioClip);
        }
        else
        {
            Debug.Log("AudioClip not found");

        }

    }



    private AudioClip getAudioClip(SoundName sound)
    {
        AudioType audioClip = Array.Find(Audio, item => item.soundName == sound);
        if (audioClip != null)
            return audioClip.soundClip;
        return null;
    }

    public void SetBGVolume(float volume)
    {
        BG.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        SFX.volume = volume;
    }
}
