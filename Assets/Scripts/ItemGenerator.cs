using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] pandoraBoxes;
   
    [SerializeField] GameObject[] giftBoxes;
  
    [SerializeField] GameObject car;
    [SerializeField] GameObject energyPotion;

    [SerializeField] public float xLower;
    [SerializeField] public float xUpper;
    [SerializeField] public float zLower;
    [SerializeField] public float zUpper;
    [SerializeField] float heightDrop;

    [SerializeField] GameObject itemCanvas;

    public void GenerateCar()
    {
        GenerateItem(car);
    }
    public void GeneratePandora()
    {
        int max = pandoraBoxes.Length;
        int rand = Random.Range(0, max);
        GenerateItem(pandoraBoxes[rand]);
        
    }
    public void GenerateGift()
    {
        int max = giftBoxes.Length;
        int rand = Random.Range(0, max);
        GenerateItem(giftBoxes[rand]);
    }
    public void GeneratePotion()
    {
        GenerateItem(energyPotion);
    }

    Vector3 GetRandomPos()
    {
        float x = Random.Range(xLower, xUpper);
        float z = Random.Range(zLower, zUpper);

        return new Vector3(x, heightDrop, z);
    }


    void GenerateItem(GameObject obj)
    {
        Vector3 pos = GetRandomPos();
        GameObject newObject =  Instantiate(obj, pos, Quaternion.identity);
        newObject.transform.parent = this.transform;
    }

    public void SetListCanvas()
    {
        bool active = itemCanvas.activeSelf;
        itemCanvas.SetActive(!active);
    }


}
