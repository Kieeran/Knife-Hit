using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private Transform objectHolder;

    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject goldenApplePrefab;

    [Tooltip("The angle is in degrees")]
    [SerializeField] private List<float> knifeAngles;
    [Tooltip("The angle is in degrees")]
    [SerializeField] private List<float> appleAngles;
    [Tooltip("The angle is in degrees")]
    [SerializeField] private List<float> goldenAppleAngles;

    public float radius;

    void Start()
    {
        spawnObjects();
        alignObjects();
    }

    private float degToRad(float deg)
    {
        return (float)(deg * (Math.PI / 180f));
    }

    private void spawnObjects()
    {
        this.spawnObject(knifeAngles, knifePrefab);
        this.spawnObject(appleAngles, applePrefab);
        this.spawnObject(goldenAppleAngles, goldenApplePrefab);
    }

    private void spawnObject(List<float> anglesArray, GameObject prefabObject)
    {
        for (int i = 0; i < anglesArray.Count; i++)
        {
            GameObject obj = Instantiate(prefabObject, objectHolder);

            float tempRadius = this.radius;
            if (obj.name.Contains("Knife"))
            {
                tempRadius -= 13;
            }

            Vector2 pos = new Vector2(
                (float)(objectHolder.position.x + tempRadius * Math.Cos(degToRad(anglesArray[i]))),
                (float)(objectHolder.position.y + tempRadius * Math.Sin(degToRad(anglesArray[i])))
            );

            obj.transform.position = pos;
        }
    }

    private void alignObjects()
    {
        foreach (Transform child in objectHolder)
        {
            alignObject(child.gameObject);
        }
    }

    private void alignObject(GameObject obj)
    {
        float dx = objectHolder.position.x - obj.transform.position.x;
        float dy = objectHolder.position.y - obj.transform.position.y;

        float angleRad = (float)Math.Atan2(dx, -dy);
        if (obj.name.Contains("Knife"))
        {
            angleRad = (float)Math.Atan2(-dx, dy);
        }

        float angleDeg = (float)(angleRad * (180 / Math.PI));
        obj.transform.Rotate(0, 0, angleDeg);
    }
}