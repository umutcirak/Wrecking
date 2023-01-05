using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI
;
public class CollectEnergy : MonoBehaviour
{
    Player player;
    PlayerController playerController;

    [SerializeField] float energyPerShot;
    [SerializeField] Image energyBar;

    private void Start()
    {
        UpdateEnergyBar();
    }
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        if (playerController.isSpinning)
        {
            UpdateEnergyBar();
        }
    }


    void OnCollisionEnter(Collision collision)
    {       

        if(collision.gameObject.CompareTag("Energy"))
        {
            Destroy(collision.gameObject);
            player.currentEnergy += energyPerShot;
            player.currentEnergy = Mathf.Clamp(player.currentEnergy, 0f, player.MaxEnergy);
            UpdateEnergyBar();
        }        

    }

    void UpdateEnergyBar()
    {
        energyBar.fillAmount = player.currentEnergy / player.MaxEnergy;
    }

}
