using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{    
    void OnCollisionExit(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Car"))
        {
            Rigidbody rgbdCar = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 force = new Vector3(250f, 1000f, 150f);
            rgbdCar.AddForce(force);
            
            Debug.Log("Collided with a Car!");
        }
        
        
    }


}
