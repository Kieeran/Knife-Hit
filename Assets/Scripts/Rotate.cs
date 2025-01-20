using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;

    public Rigidbody2D rb2d;

    void Update()
    {
        rb2d.angularVelocity = rotationSpeed;
    }
}
