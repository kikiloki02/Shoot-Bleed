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


    void Awake()
    {

        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
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
    public void  Play (string name)
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
        FadeOutCorrutine(volume);
    }

    public void FadeInMusic()
    {
        float volume;
        Sounds.audioMixer.GetFloat("MusicVolume", out volume);
        FadeInCorrutine(volume);
    }

    IEnumerator FadeOutCorrutine(float vol)
    {
        vol -= 0.1f;
        Sounds.audioMixer.SetFloat("MusicVolume", Mathf.Log10(vol) * 20);
        yield return 0.1f;
        if(vol >= 0)
        {
            FadeOutCorrutine(vol);
        }
    }
    
    IEnumerator FadeInCorrutine(float vol)
    {
        vol += 0.1f;
        Sounds.audioMixer.SetFloat("MusicVolume", Mathf.Log10(vol) * 20);
        yield return 0.1f;
        if(vol < 1.0f)
        {
            FadeInCorrutine(vol);
        }
    }
}
