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

    Player player;
    UIManager uiManager;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Gift>() != null)
        {
            Rigidbody rgbd = gameObject.GetComponent<Rigidbody>();

            rgbd.AddExplosionForce(1000f, new Vector3(0f, 1500f, 0f), 50f);
            
            Gift gift = collision.gameObject.GetComponent<Gift>();

            uiManager.ActivateAbilityUI(true);
            uiManager.FillAbilityBar();

            if (gift.present == Gift.presentType.tornado)
            {
                DestroyGift(gift);
                player.ability = Player.abilityType.Tornado;
                uiManager.SetAbilityName(Player.abilityType.Tornado.ToString());                
                
            }
            
        }
    }




    void DestroyGift(Gift gift)
    {
        gift.gameObject.SetActive(false);
        Destroy(gift.gameObject);
    }


}
