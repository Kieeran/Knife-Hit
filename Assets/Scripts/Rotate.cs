using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private bool isTargetRotatingContinuously;
    [SerializeField] private bool isReverve;

    [SerializeField] private float initAngularVel;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationDuration;
    [SerializeField] private float anglDamping;

    public Rigidbody2D rb2d;

    void Update()
    {
        rb2d.angularVelocity = rotationSpeed;
    }
}
