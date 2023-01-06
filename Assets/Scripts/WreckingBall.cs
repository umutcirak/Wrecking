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

    private void Update()
    {
        SolveCollisionBug();
    }


    void SolveCollisionBug()
    {
        if(transform.position.y > -1.5f) { return; }

        Vector3 liftedPos = new Vector3(transform.position.x, 1f, transform.position.z);

        transform.position = liftedPos;
        
    }


}
