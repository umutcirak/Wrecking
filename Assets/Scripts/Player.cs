using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] wheelTrails;
    public ParticleSystem[] WheelTrails { get { return wheelTrails; } }

    public float currentEnergy;
    [SerializeField] private float maxEnergy;
    public float MaxEnergy { get { return maxEnergy; } }

    public float energyConsuming;
        
    public enum abilityType { None ,Tornado, Rocket}
    public abilityType ability;
    public bool isAbilityActive;

    [Header("Tornado Settings")]
    [SerializeField] ParticleSystem tornadoVFX;    
    [SerializeField] [Range(3f, 7f)] public float maxTornadoPeriod;
    [SerializeField] float tornadoForce;
    [SerializeField] GameObject ball;


    [Header("Rocket Settings")]
    [SerializeField] ParticleSystem rocketVFX;
    [SerializeField] [Range(3, 8)] public int rocketCount;
    [SerializeField] float rocketForce;
    [SerializeField] float rocketRange;
    [SerializeField] GameObject nearestCar;

    PlayerController playerController;
    UIManager uiManager;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        uiManager = FindObjectOfType<UIManager>();
    }


    private void LateUpdate()
    {
        RevivePlayer();
    }

    void RevivePlayer()
    {
        if(playerController.transform.position.y < -10f)
        {
            SceneManager.LoadScene(1);
        }
    }


    public void StartAbility()
    {
        if(ability == abilityType.None || isAbilityActive) { return; }

        uiManager.ActivateAbilityUI(true);
        isAbilityActive = true;
        if(ability == abilityType.Tornado)
        {
            StartCoroutine(TornadoCo());            
        }
    }


    

    private IEnumerator TornadoCo()
    {
        float countDown = maxTornadoPeriod;
        tornadoVFX.enableEmission = true;

       
        playerController.isSpinning = false;

        while (countDown >= 0)
        {
            Debug.Log("Time Left: " + countDown);
            countDown -= Time.smoothDeltaTime;
            uiManager.FillAbilityBar(countDown, maxTornadoPeriod);

            ball.transform.RotateAround(playerController.transform.position, Vector3.up, tornadoForce * Time.deltaTime);

            yield return null;
        }

        tornadoVFX.enableEmission = false;
        ability = abilityType.None;
        uiManager.ActivateAbilityUI(false);
        isAbilityActive = false;

        Debug.Log("TORNADO DONE");
    }










}
