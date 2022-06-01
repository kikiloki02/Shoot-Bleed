using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNewAudioManager : MonoBehaviour
{
    private NewAudioManager newAudioManager;
    // Start is called before the first frame update
    void Start()
    {
        newAudioManager = FindObjectOfType<NewAudioManager>();
        if (newAudioManager != null) { Destroy(newAudioManager.gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
