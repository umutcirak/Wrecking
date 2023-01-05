using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ShakeCamera : MonoBehaviour
{  

    [SerializeField] float duration = 0.5f;
    [SerializeField] float intensity = 0.5f;
    [SerializeField] float frequency;


    CinemachineVirtualCamera virtualCam;
    CinemachineBasicMultiChannelPerlin cameraNoise;

    private void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        cameraNoise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        cameraNoise.m_AmplitudeGain = 0f;
        cameraNoise.m_FrequencyGain = 0f;
    }


    public void ShakeTheCamera()
    {
        StartCoroutine(ShakeCo());
    }

    IEnumerator ShakeCo()
    {
        float elapsedTime = 0f;
        cameraNoise.m_AmplitudeGain = intensity;
        cameraNoise.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);

        cameraNoise.m_AmplitudeGain = 0f;
        cameraNoise.m_FrequencyGain = 0f;

    }


}
