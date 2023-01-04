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

    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        GetInput();    
    }

    void LateUpdate()
    {
        SetTrails();
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
            Vector3 dir = _rigidbody.velocity;
            dir.y = 0;

            //transform.rotation = Quaternion.LookRotation(dir);


            Quaternion rotTarget = Quaternion.LookRotation(dir);
            Quaternion result = Quaternion.RotateTowards(transform.rotation, rotTarget, steeringSpeed);

            float angleDif = transform.eulerAngles.y - rotTarget.eulerAngles.y;

            if (Mathf.Abs(angleDif) > 1f)  // Mathf.Epsilon
            {
                transform.eulerAngles = new Vector3(0, result.eulerAngles.y, 0);
            }

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
            //transform.Rotate(0, spinSpeed, 0f);           

            Vector3 torque = new Vector3(0f, spinSpeed, 0f);

            _rigidbody.AddTorque(torque);
        }
    }

    public void SetSpinning()
    {
        isSpinning = !isSpinning;
    }


    void SetTrails()
    {
        bool activation;
        if(_rigidbody.velocity.magnitude > 5f)
        {
            activation = true;
        }
        else
        {
            activation = false;
        }

        foreach (var item in player.WheelTrails)
        {
            item.enableEmission = activation;
        }
    }


}
