using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private bool isTargetRotatingContinuously;
    [SerializeField] private bool isReverve;
    [SerializeField] private bool isCounterClockwise;

    [SerializeField] private float initAngularVel;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationDuration;
    [SerializeField] private float anglDamping;

    [SerializeField] private Rigidbody2D rb2d;

    private float counter;
    private float tempRS;
    private bool canRotate;

    private void Start()
    {
        tempRS = -rotationSpeed;
        counter = 0;
        canRotate = true;

        if (isCounterClockwise == true)
        {
            tempRS = -tempRS;
        }

        if (isTargetRotatingContinuously == false)
        {
            rb2d.angularDrag = anglDamping;
            return;
        }

        rb2d.angularVelocity = initAngularVel;
    }

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
        rb2d.angularVelocity = tempRS * 100;
    }

    private void RotateWithBreaks()
    {
        counter += Time.deltaTime;
        if (counter > rotationDuration)
        {
            canRotate = !canRotate;

            if (canRotate == true)
            {
                if (isReverve == true)
                {
                    tempRS = -tempRS;
                }
            }

            counter = 0;
        }

        if (canRotate == true)
        {
            rb2d.AddTorque(tempRS);
        }
    }
}
