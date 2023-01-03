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

    [SerializeField] bool isSpinning;
    [SerializeField] float spinSpeed;

    void Update()
    {
        GetInput();    
    }
    private void FixedUpdate()
    {     
        Move();        
        Rotate();
        Spin();
    }

    void Move()
    {
        if(isSpinning) { return; }

        Vector3 force = new Vector3(horizontalInput, 0f, verticalInput);
        _rigidbody.AddForce(force * moveSpeed);
    }              

    void Rotate()
    {
        if (isSpinning) { return; }

        if (horizontalInput !=0 || verticalInput != 0) 
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }

    void GetInput()
    {
        horizontalInput = _joystick.Horizontal;
        verticalInput   = _joystick.Vertical;
    }

    void Spin()
    {
        if (isSpinning)
        {
            transform.Rotate(0, spinSpeed, 0f);           
        }
    }

    public void SetSpinning()
    {
        isSpinning = !isSpinning;
    }


}
