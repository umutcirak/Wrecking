using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{       
    Player player;

    float ballSpeed;

    ShakeCamera camShaker;

    


    private void Awake()
    {
        player = FindObjectOfType<Player>();
        camShaker = FindObjectOfType<ShakeCamera>();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Car"))
        {
            Rigidbody rgbdCar = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 ballVelocity = GetComponent<Rigidbody>().velocity;
            ballVelocity.y = ballVelocity.magnitude *  player.ballVerticalForce;

            Vector3 forceVector = ballVelocity * player.ballForce;
            rgbdCar.AddForce(forceVector);


            camShaker.Play();
            Debug.Log("Collided with a Car!");
        }
        
        
    }


}
