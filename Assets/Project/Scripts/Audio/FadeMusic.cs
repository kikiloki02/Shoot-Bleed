using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMusic : MonoBehaviour
{
    enum Fade { IN, OUT }

    private NewAudioManager newAudioManager;
    [SerializeField]
    private Fade fade;
    // Start is called before the first frame update
    void Start()
    {
        newAudioManager = FindObjectOfType<NewAudioManager>();
        if (fade == Fade.IN) { newAudioManager.FadeInMusic("MusicVolume"); }
        else if (fade == Fade.OUT) { newAudioManager.FadeOutMusic("MusicVolume"); }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
