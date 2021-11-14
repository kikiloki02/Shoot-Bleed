using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public AudioSource[] bgm;

    public static AudioManager instance;

    Coroutine fadeIn = null;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySFX(int soundToPlay)
    {
        if (soundToPlay < sfx.Length) { sfx[soundToPlay].Play(); }
    }

    public void PlayBGM(int musicToPlay)
    {
        if (!bgm[musicToPlay].isPlaying)
        {
            StopMusic(musicToPlay);

            if (musicToPlay < bgm.Length)
            {
                fadeIn = StartCoroutine(FadeIn(musicToPlay, 0.05f, 0.5f));
            }
        }
    }

    public void StopMusic(int musicToPlay)
    {
        if (fadeIn != null)
        {
            StopCoroutine(fadeIn);
        }

        for (int i = 0; i < bgm.Length; i++)
        {
            if (i != musicToPlay)
            {
                StartCoroutine(FadeOut(i, 0.05f));
            }
        }
    }

    IEnumerator FadeIn(int track, float speed, float maxVolume)
    {
        bgm[track].volume = 0.0f;
        bgm[track].Play();

        float audioVolume = bgm[track].volume;

        while (bgm[track].volume <= maxVolume)
        {
            audioVolume += speed;
            bgm[track].volume = audioVolume;

            if (bgm[track].volume >= maxVolume)
            {
                bgm[track].volume = maxVolume;

                break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOut(int track, float speed)
    {
        float audioVolume = bgm[track].volume;

        while (bgm[track].volume >= 0.0f)
        {
            audioVolume -= speed;
            bgm[track].volume = audioVolume;

            if (bgm[track].volume <= 0.0f)
            {
                bgm[track].volume = 0.0f;
                bgm[track].Stop();

                break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}