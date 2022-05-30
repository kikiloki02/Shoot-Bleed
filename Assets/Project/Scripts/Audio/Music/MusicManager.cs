using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<string> musicNames;
    private string currentMusic;
    private int currentMusicIdx;
    NewAudioManager newAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        newAudioManager = FindObjectOfType<NewAudioManager>();
    }

    public void StartMusicPlaying()
    {
        currentMusic = GetRandomMusicName();
        newAudioManager.Play(currentMusic);
        StartCoroutine(PlayMusic());
    }

    string GetRandomMusicName()
    {
        currentMusicIdx = Random.Range(0, musicNames.Count);
        return musicNames[currentMusicIdx];
    }
    IEnumerator PlayMusic()
    {
        do
        {
            int currentMusicIdxSound = 0;
            for (int i = 0; i < newAudioManager.sounds.Length; i++)
            {
                if (newAudioManager.sounds[i].name == currentMusic)
                {
                    currentMusicIdxSound = i;
                    break;
                }
            }
            if (!newAudioManager.sounds[currentMusicIdxSound].source.isPlaying)
            {
                musicNames.Remove(currentMusic);
                currentMusic = GetRandomMusicName();
                newAudioManager.Play(currentMusic);
            }

            yield return 1.0f;

        } while (true);

    }

}
