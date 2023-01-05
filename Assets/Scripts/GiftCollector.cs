using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftCollector : MonoBehaviour
{
    [Header("Tornado Settings")]
    [SerializeField] ParticleSystem tornadoVFX;

    [SerializeField] [Range(1f, 5f)] float tornadoPeriod;

    [SerializeField] float tornadoForce;

    [SerializeField] GameObject ball;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Gift>() != null)
        {
            Rigidbody rgbd = gameObject.GetComponent<Rigidbody>();

            rgbd.AddExplosionForce(1000f, new Vector3(0f, 1500f, 0f), 50f);
            
            Gift gift = collision.gameObject.GetComponent<Gift>();

            if(gift.present == Gift.presentType.tornado)
            {
                DestroyGift(gift);

                StartTornado();                               

            }
            
        }
    }


    void StartTornado()
    {
        StartCoroutine(TornadoCo());
    }

    private IEnumerator TornadoCo()
    {
        float countDown = tornadoPeriod;
        tornadoVFX.enableEmission = true;

        PlayerController player = GetComponent<PlayerController>();
        player.isSpinning = false;

        while (countDown >= 0)
        {
            Debug.Log("Time Left: " + countDown);
            countDown -= Time.smoothDeltaTime;

            ball.transform.RotateAround(player.transform.position, Vector3.up, tornadoForce * Time.deltaTime);
            
            yield return null;
        }

        tornadoVFX.enableEmission = false;
        Debug.Log("BITTI");
    }




    void DestroyGift(Gift gift)
    {
        gift.gameObject.SetActive(false);
        Destroy(gift.gameObject);
    }


}
