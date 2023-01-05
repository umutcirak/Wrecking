using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Ability")]
    [SerializeField] Image background;
    [SerializeField] Image abilityBar;
    [SerializeField] TextMeshProUGUI abilityText;



    private void Start()
    {
        ActivateAbilityUI(false);
    }

    public void ActivateAbilityUI(bool activation)
    {
        background.enabled = activation;
        abilityBar.enabled = activation;
        abilityText.enabled = activation;
    }


    public void FillAbilityBar(float current, float max)
    {
        abilityBar.fillAmount = current / max;
    }
    public void FillAbilityBar()
    {
        abilityBar.fillAmount = 1f;
    }

    public void SetAbilityName(string abilityName)
    {
        abilityText.text = abilityName;
    }





}
