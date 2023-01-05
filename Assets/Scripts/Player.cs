using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] wheelTrails;
    public ParticleSystem[] WheelTrails { get { return wheelTrails; } }

    public float currentEnergy;
    [SerializeField] private float maxEnergy;
    public float MaxEnergy { get { return maxEnergy; } }

    public float energyConsuming;   











}
