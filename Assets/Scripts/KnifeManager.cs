using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    [SerializeField] private KnifeConfig[] knifeConfigs;
    [SerializeField] private string scriptableObjectsPath;

    private void Start()
    {
        amount = 10;

        LoadConfigs();
        InitPooling();
    }

    private void LoadConfigs()
    {
        knifeConfigs = Resources.LoadAll<KnifeConfig>(scriptableObjectsPath);
    }

    private void InitPooling()
    {
        spawnKnives = new Queue<GameObject>();
        throwKnives = new Queue<GameObject>();

        CreateSpawnKnives(amount);
        CreateThrowKnives(amount);
    }

    private void CreateSpawnKnives(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject knife = Instantiate(spawnKnifePrefab, transform);
            knife.gameObject.SetActive(false);
            spawnKnives.Enqueue(knife);
        }
    }

    private void CreateThrowKnives(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject knife = Instantiate(throwKnifePrefab, transform);
            knife.gameObject.SetActive(false);
            throwKnives.Enqueue(knife);
        }
    }

    public GameObject GetSpawnKnife()
    {
        if (spawnKnives.Count <= 0)
        {
            CreateSpawnKnives(amount);
            Debug.Log("Create more spawn knife");
        }

        GameObject knife = spawnKnives.Dequeue();

        return knife;
    }

    public void GetThrowKnifeByID(int id)
    {

    }
}