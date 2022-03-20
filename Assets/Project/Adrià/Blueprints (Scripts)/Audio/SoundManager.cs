using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }



    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
       // volumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        ChangeVolume();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    
    
}
