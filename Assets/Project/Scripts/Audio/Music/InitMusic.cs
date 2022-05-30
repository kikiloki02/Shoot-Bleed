using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMusic : MonoBehaviour
{
    private MusicManager musicManager;

    // Start is called before the first frame update
    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        musicManager.StartMusicPlaying();
    }

    // Update is called once per frame

}
