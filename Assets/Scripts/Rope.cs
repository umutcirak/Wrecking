using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] Transform carPlayer;
    [SerializeField] Transform ball;

    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        RenderRope();
    }

    void RenderRope()
    {
        lr.SetPosition(0, carPlayer.position);
        lr.SetPosition(1, ball.position);
    }
}
