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

    public void UpdateAppleCoin(int addition)
    {
        currentAppleCoin += addition;
        UIManager.Instance.ShowAppleCoinUI(currentAppleCoin);
    }

    private void Start()
    {
        RestartGame();
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

    public void RestartGame()
    {
        currentLevel = 1;
        stageNumInLevel = 1;

        currentLevelData = LevelManager.Instance.GetLevelDatas()[currentLevel - 1];

        StageConfig stageConfig = GetRandomStageConfig(currentLevelData.stageConfigs);
        TargetConfig targetConfig = GetRandomTargetConfig(currentLevelData.targetConfigs);

        LoadData(stageConfig, targetConfig);
    }

    public void Win()
    {
        Debug.Log("You win!!!");
        stageNumInLevel++;

        if (currentLevel > LevelManager.Instance.GetLevelDatas().Count)
        {
            Debug.Log("You all win!!!");
            UIManager.Instance.OpenGameOverPopUp();

            return;
        }

        if (stageNumInLevel == 6)
        {
            stageNumInLevel = 1;
            currentLevel++;

            currentLevelData = LevelManager.Instance.GetLevelDatas()[currentLevel - 1];
        }

        if (stageNumInLevel == 5)
        {
            StageConfig stageConfig = GetRandomStageConfig(currentLevelData.stageBossConfigs);
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
