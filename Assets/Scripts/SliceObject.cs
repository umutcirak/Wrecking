using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Material matSlicedSide; // Kesilen tarafin materiali
    public float explosionForce;
    public float explosionRadius;
    public bool gravity, kinematic;
    [SerializeField] float destroyTime;

    [SerializeField] Transform parent;



    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("CanSliced"))
        {
            Debug.Log("Name" + other.gameObject.name);

            SlicedHull slicedObj = Slice(other.gameObject, matSlicedSide);
            GameObject slicedObjTop = slicedObj.CreateLowerHull(other.gameObject, matSlicedSide);
            GameObject slicedObjBottom = slicedObj.CreateUpperHull(other.gameObject, matSlicedSide);
            Destroy(other.gameObject);

            
            slicedObjTop.transform.parent = parent;
            slicedObjBottom.transform.parent = parent;
            slicedObjBottom.name = "Plane";
            slicedObjTop.name = "Sliced Part";

            AddComponentToPlane(slicedObjBottom);
            AddComponentToSliced(slicedObjTop);
            DestroyObj(slicedObjTop, destroyTime);
            
        }
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, transform.up, mat);
    }

    void AddComponentToSliced(GameObject obj)
    {        
        var rgbd = obj.AddComponent<Rigidbody>();
        rgbd.useGravity = true;
    }
    void AddComponentToPlane(GameObject obj)
    {
        obj.AddComponent<MeshCollider>();
        obj.tag = "CanSliced";       
    }

    void DestroyObj(GameObject obj, float time)
    {
        Destroy(obj, time);
    }




}
