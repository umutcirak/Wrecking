using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit Features")]
public class UnitSO : ScriptableObject
{
    [SerializeField] [Range(0.1f, 5f)] float mass;
    float Mass { get { return mass; } }

    [SerializeField] [Range(10f, 150f)] float speed;
    float Speed { get { return speed; } }

    [SerializeField] [Range(10f, 150f)] float steeringSpeed;
    float SteeringSpeed { get { return steeringSpeed; } }


}
