using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{
    public static KnifeManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private Queue<GameObject> spawnKnives;
    private Queue<GameObject> throwKnives;

    [SerializeField] private GameObject spawnKnifePrefab;
    [SerializeField] private GameObject throwKnifePrefab;
    private int amount;


    [SerializeField] private List<KnifeConfig> knifeConfigs;

    private void Start()
    {
        amount = 10;

        InitPooling();
    }

    private void InitPooling()
    {
        CreateSpawnKnives(amount);
        CreateThrowKnives(amount);
    }

    private void CreateSpawnKnives(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject spawnKnife = Instantiate(spawnKnifePrefab);
            spawnKnife.gameObject.SetActive(false);
            spawnKnives.Enqueue(spawnKnife);
        }
    }

    private void CreateThrowKnives(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject throwKnife = Instantiate(throwKnifePrefab);
            throwKnife.gameObject.SetActive(false);
            throwKnives.Enqueue(throwKnife);
        }
    }
}