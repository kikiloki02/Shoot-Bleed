using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<string> constMusicNames;

    [SerializeField]
    private List<string> musicNames;

    private string currentMusic;
    private int currentMusicIdx;
    NewAudioManager newAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        newAudioManager = FindObjectOfType<NewAudioManager>();
        FillMusicNames();
        Debug.Log(musicNames.Count);
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

    void FillMusicNames()
    {
        for(int i = 0; i < constMusicNames.Count; i++)
        {
            musicNames.Insert(i, constMusicNames[i]);
        }
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
                if(musicNames.Count <= 0) { FillMusicNames(); }
                currentMusic = GetRandomMusicName();
                newAudioManager.Play(currentMusic);
            }

            yield return 1.0f;

        } while (true);

    }

}
