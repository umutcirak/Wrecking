using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    ParticleSystem ballTrail;

    private void Awake()
    {
        ballTrail = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        ballTrail.enableEmission = false;
    }


}
