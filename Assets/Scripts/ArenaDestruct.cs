using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ArenaDestruct : MonoBehaviour
{
    [SerializeField] GameObject canvasDestruction;
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] GameObject[] knifePools;
    [SerializeField] GameObject destructButton;
    ItemGenerator itemGenerator;

    [SerializeField] float period;

    public float timeLeft;



    private void Awake()
    {
        itemGenerator = FindObjectOfType<ItemGenerator>();
    }



    public void Destruct()
    {
        destructButton.SetActive(false);
        ChangeBorders();
        StartCoroutine(StartDestruction());
    }


    IEnumerator StartDestruction()
    {
        for (int i = 0; i < knifePools.Length; i++)
        {
            timeLeft = period;
            canvasDestruction.SetActive(true);
            while(timeLeft >= Mathf.Epsilon)
            {
                timeLeft -= Time.deltaTime;
                timeText.text = timeLeft.ToString("#.00");
                yield return new WaitForEndOfFrame();
            }
            knifePools[i].SetActive(true);
            StartCoroutine(DestroyPool(knifePools[i]));
        }
        canvasDestruction.SetActive(false);
    }

    IEnumerator DestroyPool(GameObject obj)
    {
        yield return new WaitForSeconds(6f);

        obj.SetActive(false);
        Destroy(obj);


    }


    void ChangeBorders()
    {
        itemGenerator.xLower = itemGenerator.xLower / 3;
        itemGenerator.zLower = itemGenerator.zLower / 3;
        itemGenerator.xUpper = itemGenerator.xUpper / 3;
        itemGenerator.zUpper = itemGenerator.zUpper / 3;
    }



}
