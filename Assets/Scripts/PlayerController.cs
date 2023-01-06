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
    [SerializeField] private float dropRotationSpeed;
      

    [SerializeField] public bool isSpinning;
    
    [SerializeField] float spinSpeed;

    Player player;

    [SerializeField] GameObject ball;

    [SerializeField] float heightToControl;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
       
        isSpinning = false;
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
        //FixDropRotation();
    }

    void Move()
    {
        if(isSpinning) { return; }
        if (transform.position.y > heightToControl) { return; }

        Vector3 force = new Vector3(horizontalInput, 0f, verticalInput);
        _rigidbody.AddForce(force * moveSpeed);
    }              

    void Rotate()
    {
        if (isSpinning) { return; }
        if (transform.position.y > heightToControl) { return; }

        if (Mathf.Abs(_rigidbody.velocity.magnitude) > 0.15f) 
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
        if(player.isAbilityActive || transform.position.y > 5f) { return; }

        if(isSpinning && player.currentEnergy >= Mathf.Epsilon)
        {
            SpinCar();
            SpinBall();
            player.currentEnergy -= player.energyConsuming;
            if(player.currentEnergy <= 0f)
            {
                player.currentEnergy = 0f;
                isSpinning = false;
            }
        }        
    }

    void SpinCar()
    {
        //transform.Rotate(0, spinSpeed, 0f);           

        Vector3 torque = new Vector3(0f, spinSpeed, 0f);

        _rigidbody.AddTorque(torque);


    }
    void SpinBall()
    {
        ball.transform.RotateAround(transform.position, Vector3.up, 10f);

        Rigidbody ballRgbd = ball.GetComponent<Rigidbody>();        
        //ballRgbd.AddRelativeForce(Vector3.forward * 0.1f);

        

    }

    void FixDropRotation()
    {
        float yVelocity = _rigidbody.velocity.y;        
        
        if(yVelocity <= 0f && transform.position.y > 5f && transform.position.y < 50f)
        {
            //_rigidbody.velocity = new Vector3(0f, -6f, 0f);
            Vector3 direction = new Vector3(0f, 1f, 0f);

            Quaternion rotTarget = Quaternion.LookRotation(direction);
            Quaternion result = Quaternion.RotateTowards(transform.rotation, rotTarget, dropRotationSpeed);

            float angleDif = transform.eulerAngles.y - rotTarget.eulerAngles.y;

            if (Mathf.Abs(angleDif) > 5f)  // Mathf.Epsilon
            {
                transform.eulerAngles = new Vector3(0f, result.eulerAngles.y, 0f);
            }
        }        

    }



    public void SetSpinning()
    {
        if(player.isAbilityActive) { return; }

        if(player.currentEnergy >= Mathf.Epsilon)
        {
            isSpinning = !isSpinning;
        }        
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
