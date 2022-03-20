using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    CinemachineVirtualCamera virtualCam;
    private float timeToShake;
    private void Awake()
    {
        Instance = this;
        virtualCam = GetComponent<CinemachineVirtualCamera>();
    }
    public void ShakeCamera(float intensity, float time)
    {
        timeToShake = time;
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }

    private void Update()
    {
        timeToShake -= Time.deltaTime;
        if (timeToShake > 0)
        {
            timeToShake -= Time.deltaTime;
        }
        if (timeToShake <= 0)
        {
            //time over
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        }

    }
}
