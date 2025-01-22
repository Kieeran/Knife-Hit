using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleManager : MonoBehaviour
{
    public static AppleManager Instance { get; private set; }

    private Queue<GameObject> apples;
    private Queue<GameObject> goldenApples;

    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject goldenApplePrefab;
    private int amount;

    [SerializeField] private AppleConfig[] appleConfigs;
    [SerializeField] private string scriptableObjectsPath;

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
        appleConfigs = Resources.LoadAll<AppleConfig>(scriptableObjectsPath);
    }

    private void InitPooling()
    {
        apples = new Queue<GameObject>();
        goldenApples = new Queue<GameObject>();

        CreateApples(amount);
        CreateGoldenApples(amount);
    }

    private void CreateApples(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject apple = Instantiate(applePrefab, transform);
            apple.SetActive(false);
            apples.Enqueue(apple);
        }
    }

    private void CreateGoldenApples(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject apple = Instantiate(goldenApplePrefab, transform);
            apple.SetActive(false);
            goldenApples.Enqueue(apple);
        }
    }

    public GameObject GetApple()
    {
        if (apples.Count <= 0)
        {
            CreateApples(amount);
            Debug.Log("Create more apple");
        }

        GameObject apple = apples.Dequeue();
        apple.SetActive(true);

        return apple;
    }

    public void ReturnApple(GameObject apple)
    {
        apple.transform.SetParent(transform);
        apple.transform.SetLocalPositionAndRotation(Vector2.zero, Quaternion.identity);
        apple.SetActive(false);

        apples.Enqueue(apple);
    }

    public GameObject GetGoldenApple()
    {
        if (goldenApples.Count <= 0)
        {
            CreateGoldenApples(amount);
            Debug.Log("Create more golden apple");
        }

        GameObject apple = goldenApples.Dequeue();
        apple.SetActive(true);

        return apple;
    }

    public void ReturnGoldenApple(GameObject apple)
    {
        apple.transform.SetParent(transform);
        apple.transform.SetLocalPositionAndRotation(Vector2.zero, Quaternion.identity);
        apple.SetActive(false);

        goldenApples.Enqueue(apple);
    }
}
