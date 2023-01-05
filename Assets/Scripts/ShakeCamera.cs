using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ShakeCamera : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.5f;
      
    
    

    void Start()
    {
        
    }


    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {        
        

        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            composer.m_TrackedObjectOffset = initialTrackedOffset + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        composer.m_TrackedObjectOffset = initialTrackedOffset;
    }
}
