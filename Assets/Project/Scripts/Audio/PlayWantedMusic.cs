using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWantedMusic : MonoBehaviour
{
    public string musicName;
    private NewAudioManager newAudioManager;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<NewAudioManager>();
        newAudioManager.Play(musicName);
    }
}
