using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class NewAudioManager : MonoBehaviour
{
    public AudioMixerGroup Music;
    public AudioMixerGroup Sounds;

    public Sound[] sounds;

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

    public void FadeOutMusic()
    {
        float volume;
        Sounds.audioMixer.GetFloat("MusicVolume", out volume);
        StartCoroutine(FadeOutCorrutine(volume));
    }

    public void FadeInMusic()
    {
        float volume;
        Sounds.audioMixer.GetFloat("MusicVolume", out volume);
        StartCoroutine(FadeInCorrutine(volume));
    }

    IEnumerator FadeOutCorrutine(float vol)
    {
        keepFadingIn = false;
        keepFadingOut = true;

        while (vol >= -78.0f && keepFadingOut)
        {
            vol -= 2.0f;
            Sounds.audioMixer.SetFloat("MusicVolume", vol);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeInCorrutine(float vol)
    {
        keepFadingIn = true;
        keepFadingOut = false;

        while (vol < 0.0f && keepFadingIn)
        {
            vol += 4.0f;
            Sounds.audioMixer.SetFloat("MusicVolume", vol);
            yield return new WaitForSeconds(0.1f);
        }
    }
}