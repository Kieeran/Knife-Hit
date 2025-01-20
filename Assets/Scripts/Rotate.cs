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

    [SerializeField] private Rigidbody2D rb2d;

    private void Update()
    {
        if (isTargetRotatingContinuously == true)
        {
            RotateContinuously();
        }
        else
        {
            RotateWithBreaks();
        }
    }

    private void RotateContinuously()
    {
        rb2d.angularVelocity = this.rotationSpeed * 100 * Time.deltaTime;
    }

    private void RotateWithBreaks()
    {

    }
}
