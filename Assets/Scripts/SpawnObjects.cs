using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private Transform objectHolder;

    [SerializeField] private GameObject knifePrefab;

    [Tooltip("The angle is in degrees")]
    [SerializeField] private List<float> knifeAngles;

    public float radius;

    void Start()
    {
        for (int i = 0; i < knifeAngles.Count; i++)
        {
            GameObject knife = Instantiate(knifePrefab, objectHolder);

            Vector2 pos = new Vector2(
                (float)(objectHolder.position.x + radius * Math.Cos(degToRad(knifeAngles[i]))),
                (float)(objectHolder.position.y + radius * Math.Cos(degToRad(knifeAngles[i])))
            );

            knife.transform.position = pos;

            float dx = objectHolder.position.x - knife.transform.position.x;
            float dy = objectHolder.position.y - knife.transform.position.y;

            float angleRad = (float)Math.Atan2(-dx, dy);

            float angleDeg = (float)(angleRad * (180 / Math.PI));

            knife.transform.Rotate(0, 0, angleDeg);
        }
    }

    private float degToRad(float deg)
    {
        return (float)(deg * (Math.PI / 180f));
    }
}