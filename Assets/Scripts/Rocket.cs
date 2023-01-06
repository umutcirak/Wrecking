using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{    
    [SerializeField] Car nearestCar;       
    [SerializeField] float launchSpeed;
    [SerializeField] [Range(1f,3f)] float waitToLock;

    Player player;
    PlayerController playerController;
    Car[] cars;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
    }


    private void Start()
    {
        StartCoroutine(ActivateRocketCo());
    }
       

    IEnumerator ActivateRocketCo()
    {
        GetCars();
        Debug.Log("Total Car Found:" + cars.Length);
        FindNearestCar();
        Debug.Log("Nearest:" + nearestCar.name);
        
        ThrowRocket();
        yield return new WaitForSeconds(waitToLock);

        GetComponent<Rigidbody>().isKinematic = true;

       while(nearestCar != null)
        {           
            yield return new WaitForEndOfFrame();
            transform.LookAt(nearestCar.transform,Vector3.up);

            transform.position = Vector3.MoveTowards(transform.position,
                nearestCar.transform.position, player.rocketSpeed * Time.deltaTime);
        }

        //yield return new WaitForSeconds(waitToLock);

    }

    void OnCollisionEnter(Collision collision)
    {     
        Explode();
        gameObject.SetActive(false);
        Destroy(gameObject);

    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, player.explosionRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rgbd = collider.GetComponent<Rigidbody>();

            if (rgbd != null)
            {
                Vector3 loweredPos = new Vector3(transform.position.x, 
                    transform.position.y - 4f, transform.position.z);

                rgbd.AddExplosionForce(player.rocketForce, loweredPos, player.explosionRadius);
            }
        }
    }




    void ThrowRocket()
    {        
        Rigidbody rgbd = GetComponent<Rigidbody>();

        float xForce = Random.Range(0f, launchSpeed) / 10f;
        float zForce = Random.Range(0f, launchSpeed) / 10f;

        Vector3 forceVector = new Vector3(xForce, launchSpeed, zForce);

        rgbd.AddForce(forceVector);
        

    }

    void GetCars()
    {
        cars = FindObjectsOfType<Car>();
    }


    void FindNearestCar()
    {
        Vector3 currentPos = playerController.transform.position;
        Car nearestObj = null;
        float nearestDistance = float.MaxValue;

        foreach (Car car in cars)
        {            
            float distance = Vector3.Distance(currentPos, car.transform.position); 
            
            if (distance < nearestDistance )
            {
                nearestDistance = distance;
                nearestObj = car;
            }
        }

        if(Vector3.Distance(currentPos,nearestObj.transform.position) > player.rocketRange)
        {
            this.nearestCar = null;
        }
        else
        {
            this.nearestCar = nearestObj;
        }
        
    }

}
