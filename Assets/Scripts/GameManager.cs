using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform Holder;
    [SerializeField] private Transform KnifeHolder;
    private LevelData currentLevelData;
    private int currentLevel;
    private int stageNumInLevel;
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
        currentLevel = 1;
        stageNumInLevel = 1;

        currentLevelData = LevelManager.Instance.GetLevelDatas()[currentLevel - 1];

        // StageConfig stageConfig = currentLevelData.stageConfigs[Random.Range(0, currentLevelData.stageConfigs.Count - 2)];
        // TargetConfig targetConfig = currentLevelData.targetConfigs[Random.Range(0, currentLevelData.targetConfigs.Count - 2)];
        StageConfig stageConfig = GetRandomStageConfig(currentLevelData.stageConfigs);
        TargetConfig targetConfig = GetRandomTargetConfig(currentLevelData.targetConfigs);

        LoadData(stageConfig, targetConfig);
    }

    private void LoadData(StageConfig stageConfig, TargetConfig targetConfig)
    {
        Holder.GetComponent<SpawnObjects>().ResetSpawnObjects(stageConfig);
        Holder.GetComponent<Rotate>().ResetRotate(targetConfig);
        KnifeHolder.GetComponent<ThrowKnife>().SetKnifeAmount(stageConfig.throwKnifeAmount);
    }

    private StageConfig GetRandomStageConfig(List<StageConfig> stageConfigs)
    {
        return stageConfigs[Random.Range(0, stageConfigs.Count)];
    }

    private TargetConfig GetRandomTargetConfig(List<TargetConfig> targetConfigs)
    {
        return targetConfigs[Random.Range(0, targetConfigs.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     Debug.Log("Q is being held down");
        //     StageConfig stageConfig = currentLevelData.stageConfigs[Random.Range(0, currentLevelData.stageConfigs.Count - 2)];
        //     TargetConfig targetConfig = currentLevelData.targetConfigs[Random.Range(0, currentLevelData.targetConfigs.Count - 2)];

        //     LoadData(stageConfig, targetConfig);
        // }
    }

    public void Win()
    {
        Debug.Log("You win!!!");
        stageNumInLevel++;

        if (stageNumInLevel == 6)
        {
            stageNumInLevel = 1;
            currentLevel++;

            currentLevelData = LevelManager.Instance.GetLevelDatas()[currentLevel - 1];
        }

        if (stageNumInLevel == 5)
        {
            StageConfig stageConfig = GetRandomStageConfig(currentLevelData.stageBossConfigs);
            //StageConfig stageConfig = currentLevelData.stageBossConfigs[0];
            TargetConfig targetConfig = GetRandomTargetConfig(currentLevelData.targetConfigs);

            LoadData(stageConfig, targetConfig);
        }
        else
        {
            StageConfig stageConfig = GetRandomStageConfig(currentLevelData.stageConfigs);
            TargetConfig targetConfig = GetRandomTargetConfig(currentLevelData.targetConfigs);

            LoadData(stageConfig, targetConfig);
        }
    }
}
