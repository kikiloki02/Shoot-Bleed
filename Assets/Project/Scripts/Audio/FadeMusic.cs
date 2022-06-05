using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMusic : MonoBehaviour
{
    enum Fade { IN, OUT }
    enum Mixer { MUSIC, MASTER}

    private NewAudioManager newAudioManager;
    [SerializeField]
    private Fade fade;

    [SerializeField]
    private Mixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        newAudioManager = FindObjectOfType<NewAudioManager>();
        if (fade == Fade.IN)
        {
            if (mixer == Mixer.MASTER) { newAudioManager.FadeInMaster("MasterVolume"); }
            else if (mixer == Mixer.MUSIC) { newAudioManager.FadeInMusic("MusicVolume"); }
        }
        else if (fade == Fade.OUT)
        {
            if (mixer == Mixer.MASTER) { newAudioManager.FadeOutMaster("MasterVolume"); }
            else if (mixer == Mixer.MUSIC) { newAudioManager.FadeOutMusic("MusicVolume"); }
        }

    }
}
