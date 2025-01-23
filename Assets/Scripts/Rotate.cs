using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private TargetConfig targetConfig;
    private bool isTargetRotatingContinuously;
    private bool isReverve;
    private bool isCounterClockwise;

    private float initAngularVel;
    private float rotationSpeed;
    private float rotationDuration;
    private float anglDamping;

    private float counter;
    private float tempRS;
    private bool canRotate;

    private void Start()
    {

    }

    public void ResetRotate(TargetConfig config)
    {
        LoadConfig(config);

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

    private void LoadConfig(TargetConfig config)
    {
        targetConfig = config;

        isTargetRotatingContinuously = targetConfig.isTargetRotatingContinuously;
        isReverve = targetConfig.isReverve;
        isCounterClockwise = targetConfig.isCounterClockwise;

        initAngularVel = targetConfig.initAngularVel;
        rotationSpeed = targetConfig.rotationSpeed;
        rotationDuration = targetConfig.rotationDuration;
        anglDamping = targetConfig.anglDamping;
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
