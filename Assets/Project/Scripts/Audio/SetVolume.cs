using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SetVolume : MonoBehaviour
{

    public AudioMixerGroup Master;
    public AudioMixerGroup Music;
    public AudioMixerGroup ExtraMusic;

    // Start is called before the first frame update
    void Start()
    {
        Master.audioMixer.SetFloat("MasterVolume", 0.0f);
        Music.audioMixer.SetFloat("MusicVolume", 0.0f);
        ExtraMusic.audioMixer.SetFloat("ExtraMusicVolume", 0.0f);
    }
}
