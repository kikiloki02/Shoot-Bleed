using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class NewAudioManager : MonoBehaviour
{
    public AudioMixerGroup Music;
    public AudioMixerGroup Sounds;
    public AudioMixerGroup Master;

    public Sound[] sounds;
    [SerializeField]
    private List<Sound> playingSounds;

    private bool keepFadingIn = false;
    private bool keepFadingOut = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void FadeOutMusic(string mixer)
    {
        float volume;
        Sounds.audioMixer.GetFloat(mixer, out volume);
        StartCoroutine(FadeOutCorrutine(volume, mixer));
    }

    public void FadeInMusic(string mixer)
    {
        float volume;
        Sounds.audioMixer.GetFloat(mixer, out volume);
        StartCoroutine(FadeInCorrutine(volume, mixer));
    }    
    
    public void FadeOutMaster(string mixer) //To use for menus
    {
        float volume;
        Master.audioMixer.GetFloat(mixer, out volume);
        StartCoroutine(FadeOutCorrutine(volume, mixer));
    }

    public void FadeInMaster(string mixer) //To use for menus
    {
        float volume;
        Master.audioMixer.GetFloat(mixer, out volume);
        StartCoroutine(FadeInCorrutine(volume, mixer));
    }

    public void LerpPitch(float pitch, float time)
    {
        float actualPitch;
        Master.audioMixer.GetFloat("MasterPitch", out actualPitch);
        Mathf.Lerp(actualPitch, pitch, time);
        Master.audioMixer.SetFloat("MasterPitch", pitch);
    }

    IEnumerator FadeOutCorrutine(float vol, string mixer)
    {
        keepFadingIn = false;
        keepFadingOut = true;

        while (vol >= -78.0f && keepFadingOut)
        {
            vol -= 2.0f;
            Sounds.audioMixer.SetFloat(mixer, vol);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeInCorrutine(float vol, string mixer)
    {
        keepFadingIn = true;
        keepFadingOut = false;

        while (vol < 0.0f && keepFadingIn)
        {
            vol += 4.0f;
            Sounds.audioMixer.SetFloat(mixer, vol);
            yield return new WaitForSeconds(0.1f);
        }
    }
}