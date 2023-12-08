using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class Shake : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;
    private float shakeIntensity = 10f;
    private float shakeTime = 2f; // Set the shake duration to 2 seconds

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmp;

    private void Awake()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cbmp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        StopShake();
    }

    void ShakeCamera()
    {
        if (timer <= 0)
        {
            _cbmp.m_AmplitudeGain = shakeIntensity;
            timer = shakeTime;
        }
    }

    void StopShake()
    {
        _cbmp.m_AmplitudeGain = 0f;
        timer = 0;
    }

    private void Update()
    {
        if (GameObject.FindWithTag("Player1") == null || GameObject.FindWithTag("Player2") == null)
        {
            ShakeCamera();
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    StopShake();
                }
            }
        }
    }
}