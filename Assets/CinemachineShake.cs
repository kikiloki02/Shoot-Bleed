using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{

    public static CinemachineShake Instance { get; private set; }
    private CinemachineVirtualCamera virtualCamera;
    private float startingIntensity;
    private float timeToShake;
    private float totalTimeToShake;

    private void Awake()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        totalTimeToShake = time;
        timeToShake = time;
    }

    private void Update()
    {
        if(timeToShake > 0)
        {
            timeToShake -= Time.deltaTime;
        }
       // if (timeToShake <= 0f) 
       // {

            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =  Mathf.Lerp(startingIntensity, 0f, 1 - (timeToShake/totalTimeToShake));
       // }
    }
}
