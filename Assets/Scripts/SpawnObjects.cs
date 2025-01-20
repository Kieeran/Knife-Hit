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
            GameObject knife = Instantiate(knifePrefab);
            knife.transform.SetParent(objectHolder);

            Vector2 pos = new Vector2(
                (float)(objectHolder.position.x + radius * Math.Cos(knifeAngles[i])),
                (float)(objectHolder.position.y + radius * Math.Cos(knifeAngles[i]))
            );

            knife.transform.position = pos;
            knife.transform.localScale = Vector3.one;
        }
    }
}