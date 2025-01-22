using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private Transform objectHolder;

    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject goldenApplePrefab;

    [SerializeField] private float appleRadius;
    [SerializeField] private float knifeRadius;

    [SerializeField] private SpriteRenderer targetSkin;
    [SerializeField] private StageConfig stageConfig;

    private List<float> knifeAngles;
    private List<float> appleAngles;
    private List<float> goldenAppleAngles;

    public float GetKnifeRadius() { return knifeRadius; }

    void Start()
    {
        LoadConfig();

        spawnObjects();
        alignObjects();
    }

    private void LoadConfig()
    {
        knifeAngles = stageConfig.spawnKnifeAngles;
        appleAngles = stageConfig.appleAngles;
        goldenAppleAngles = stageConfig.goldenAppleAngles;

        targetSkin.sprite = stageConfig.targetSkin;
    }

    private float degToRad(float deg)
    {
        return (float)(deg * (Math.PI / 180f));
    }

    private void spawnObjects()
    {
        for (int i = 0; i < knifeAngles.Count; i++)
        {
            //GameObject obj = Instantiate(prefabObject, objectHolder);
            GameObject obj = KnifeManager.Instance.GetSpawnKnife();
            obj.transform.SetParent(objectHolder);

            ArrangeObject(obj, knifeAngles[i]);
        }

        this.spawnObject(appleAngles, applePrefab);
        this.spawnObject(goldenAppleAngles, goldenApplePrefab);
    }

    private void spawnObject(List<float> anglesArray, GameObject prefabObject)
    {
        for (int i = 0; i < anglesArray.Count; i++)
        {
            GameObject obj = Instantiate(prefabObject, objectHolder);
            ArrangeObject(obj, anglesArray[i]);
        }
    }

    private void ArrangeObject(GameObject obj, float angle)
    {
        float tempRadius = appleRadius;
        if (obj.name.Contains("Knife"))
        {
            tempRadius = knifeRadius;
        }

        Vector2 pos = new Vector2(
            (float)(objectHolder.position.x + tempRadius * Math.Cos(degToRad(angle))),
            (float)(objectHolder.position.y + tempRadius * Math.Sin(degToRad(angle)))
        );

        obj.transform.position = pos;
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