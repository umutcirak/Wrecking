using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;

    public float expForce;
    public float expRadius;


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);

            GameObject _exp = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(_exp, 3f);

            Explode();
        }
              


    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, expRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rgbd = collider.GetComponent<Rigidbody>();

            if(rgbd != null)
            {
                rgbd.AddExplosionForce(expForce, transform.parent.position, expRadius);
            }
        }
    }

}
