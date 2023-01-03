using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBall : MonoBehaviour
{
    bool isSpinning;

    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }



    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(0, 500 * Time.deltaTime, 0f); //rotates 50 degrees per second 
        }
        
    }



}
