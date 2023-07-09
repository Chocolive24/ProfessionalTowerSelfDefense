using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Cinemashake : MonoBehaviour
{
   public static Cinemashake instance { get; private set; }
   private CinemachineVirtualCamera cinemachineVirtualCamera;
   private float shakeTimer;
    private float shakeTimeTotal;
   private float startingIntensity;
   
   private void Awake()
   {
        instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

    }
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimer = time;
        shakeTimeTotal = time;
    }
    private void Update ()
    {
        if (shakeTimer < 0)
        {
            shakeTimer -= Time.deltaTime;
            if ( shakeTimer <= 0f )
            {
                // Time Over
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 -
                    (shakeTimer / shakeTimeTotal));
            }
        } 
    }
}
