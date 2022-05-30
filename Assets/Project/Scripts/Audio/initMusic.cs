using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initMusic : MonoBehaviour
{

    MusicManager musicManager;
    // Start is called before the first frame update
    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        musicManager.StartMusicPlaying();
    }


}
