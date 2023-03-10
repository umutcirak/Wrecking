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

    [Header("Ball Settings")]   
    public float ballForce;
    public float ballVerticalForce;

    public enum abilityType { None, Tornado, Rocket }    

    [Header("ABILITY SETTINGS")]
    public abilityType ability;
    public bool isAbilityActive;
    

    [Header("Tornado Settings")]
    [SerializeField] ParticleSystem tornadoVFX;    
    [SerializeField] [Range(3f, 7f)] public float maxTornadoPeriod;
    [SerializeField] float tornadoForce;
    [SerializeField] GameObject ball;
    [SerializeField] public bool isUsingTornado;

    [Header("Rocket Settings")]
    [SerializeField] Rocket rocketPrefab;    
    [SerializeField] [Range(3, 8)] public int rocketMax;
    public int rocketLeft;
    [SerializeField] public float rocketSpeed;
    [SerializeField] public float rocketForce;
    [SerializeField] public float rocketRange;
    [SerializeField] public float explosionRadius;


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
        if(ability == abilityType.Rocket && !isUsingTornado)
        {            
            LaunchRocket();
            isAbilityActive = false;
        }
    }



    void LaunchRocket()
    {       
        if(rocketLeft > 0)
        {
            rocketLeft--;
            Vector3 launchPos = new Vector3(playerController.transform.position.x,
                         playerController.transform.position.y + 2.5f, playerController.transform.position.z);

            Instantiate(rocketPrefab, launchPos, Quaternion.identity);

            uiManager.FillAbilityBar(rocketLeft, rocketMax);
            
        }
        else
        {
            ability = abilityType.None;
            uiManager.ActivateAbilityUI(false);
            rocketLeft = rocketMax;
        }


        
    }
    

    private IEnumerator TornadoCo()
    {
        float countDown = maxTornadoPeriod;
        tornadoVFX.enableEmission = true;
        isUsingTornado = true;
       
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
        isUsingTornado = false;

        Debug.Log("TORNADO DONE");
    }










}
