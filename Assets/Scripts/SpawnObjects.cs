using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private Transform objectHolder;

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

    }

    private void LoadConfig(StageConfig config)
    {
        stageConfig = config;

        knifeAngles = stageConfig.spawnKnifeAngles;
        appleAngles = stageConfig.appleAngles;
        goldenAppleAngles = stageConfig.goldenAppleAngles;

        targetSkin.sprite = stageConfig.targetSkin;
    }

    public void ResetSpawnObjects(StageConfig config)
    {
        if (objectHolder.childCount > 0)
        {
            foreach (Transform child in objectHolder)
            {
                if (child.name.Contains("Knife"))
                {
                    KnifeManager.Instance.ReturnSpawnKnife(child.gameObject);
                }

                else if (child.name.Contains("Golden"))
                {
                    AppleManager.Instance.ReturnGoldenApple(child.gameObject);
                }

                else
                {
                    AppleManager.Instance.ReturnApple(child.gameObject);
                }
            }
        }

        LoadConfig(config);

        spawnObjects();
        alignObjects();
    }

    private float degToRad(float deg)
    {
        return deg * (Mathf.PI / 180f);
    }

    private void spawnObjects()
    {
        for (int i = 0; i < knifeAngles.Count; i++)
        {
            GameObject obj = KnifeManager.Instance.GetSpawnKnife();
            obj.transform.SetParent(objectHolder);

            ArrangeObject(obj, knifeAngles[i]);
        }

        for (int i = 0; i < appleAngles.Count; i++)
        {
            GameObject obj = AppleManager.Instance.GetApple();
            obj.transform.SetParent(objectHolder);

            ArrangeObject(obj, appleAngles[i]);
        }

        for (int i = 0; i < goldenAppleAngles.Count; i++)
        {
            GameObject obj = AppleManager.Instance.GetGoldenApple();
            obj.transform.SetParent(objectHolder);

            ArrangeObject(obj, goldenAppleAngles[i]);
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
            objectHolder.position.x + tempRadius * Mathf.Cos(degToRad(angle)),
            objectHolder.position.y + tempRadius * Mathf.Sin(degToRad(angle))
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

        float angleRad = Mathf.Atan2(dx, -dy);
        if (obj.name.Contains("Knife"))
        {
            angleRad = Mathf.Atan2(-dx, dy);
        }

        float angleDeg = angleRad * (180 / Mathf.PI);
        obj.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
    }
}