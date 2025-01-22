using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private LevelData currentLevelData;
    private int currentAppleCoin;
    private int bestScore;
    private int maxStage;
    private int equippedKnifeID;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        currentLevelData = LevelManager.Instance.GetLevelDatas()[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
