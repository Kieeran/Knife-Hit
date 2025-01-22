using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{
    public static KnifeManager Instance { get; private set; }

    private Queue<GameObject> spawnKnives;
    private Queue<GameObject> throwKnives;

    [SerializeField] private GameObject spawnKnifePrefab;
    [SerializeField] private GameObject throwKnifePrefab;
    private int amount;

    [SerializeField] private KnifeConfig[] knifeConfigs;
    [SerializeField] private string knifeDirectoryPath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        amount = 10;
        LoadConfigs();
        InitPooling();
    }

    private void LoadConfigs()
    {
        knifeConfigs = Resources.LoadAll<KnifeConfig>(knifeDirectoryPath);
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
            knife.SetActive(false);
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
        knife.gameObject.SetActive(true);

        return knife;
    }

    public void ReturnSpawnKnife(GameObject knife)
    {
        knife.transform.SetParent(transform);
        knife.transform.SetLocalPositionAndRotation(Vector2.zero, Quaternion.identity);
        knife.SetActive(false);

        spawnKnives.Enqueue(knife);
    }

    public GameObject GetThrowKnifeByID(int id)
    {
        if (id > knifeConfigs.Length - 1)
        {
            Debug.Log("Unvalid ID");
            return null;
        }

        if (throwKnives.Count <= 0)
        {
            CreateThrowKnives(amount);
            Debug.Log("Create more throw knife");
        }

        GameObject knife = throwKnives.Dequeue();

        knife.SetActive(true);
        Knife _knife;
        if (TryGetComponent<Knife>(out _knife))
        {
            _knife.SetSprite(knifeConfigs[id].knifeSkin);
        }

        return knife;
    }

    public void ReturnThrowKnife(GameObject knife)
    {
        knife.transform.SetParent(transform);
        knife.transform.SetLocalPositionAndRotation(Vector2.zero, Quaternion.identity);
        knife.SetActive(false);

        throwKnives.Enqueue(knife);
    }
}