using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float moveSpeed;

    private float verticalInput;
    private float horizontalInput;

    [SerializeField] private float steeringSpeed;

    [SerializeField] Transform rotatingPoint;

    void Update()
    {
        GetInput();    
    }
    private void FixedUpdate()
    {
        // Move();
        //RotateTowards();
        Move();        
        Rotate();
    }




    void Move()
    {
        Vector3 force = new Vector3(horizontalInput, 0f, verticalInput);
        _rigidbody.AddForce(force * moveSpeed);
    }

    
   
    /*
    void Move()
    {
        _rigidbody.velocity = new Vector3(horizontalInput * moveSpeed,
           _rigidbody.velocity.y, verticalInput * moveSpeed);
    }
    */
    

    void Rotate()
    {
        if (horizontalInput !=0 || verticalInput != 0) 
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }


    }

    /*
    public void RotateTowards()
    {
        if (horizontalInput < 0.1f && verticalInput < 0.1f) { return; }
                
        Quaternion rotTarget = Quaternion.LookRotation(rotatingPoint.position - this.transform.position);
        Quaternion result = Quaternion.RotateTowards(transform.rotation, rotTarget, steeringSpeed * Time.deltaTime);

        float angleDif = transform.eulerAngles.y - rotTarget.eulerAngles.y;
        if (Mathf.Abs(angleDif) > 1f)  // Mathf.Epsilon
        {

            transform.eulerAngles = new Vector3(0, result.eulerAngles.y, 0);

        }
        
    }
    */
    void GetInput()
    {
        horizontalInput = _joystick.Horizontal;
        verticalInput   = _joystick.Vertical;
    }


}
