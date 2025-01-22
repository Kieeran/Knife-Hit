using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public StageConfig[] stageConfigs;
    public StageConfig[] stageBossConfigs;
    public TargetConfig[] targetConfigs;
    public int stageCount = 5;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private List<LevelData> levelDatas;

    [SerializeField] private string targetDirectoryPath;
    [SerializeField] private string stageDirectoryPath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LoadConfigs();
    }

    private void LoadConfigs()
    {
        levelDatas = new List<LevelData>();

        string stagePath;
        string targetPath;
        int i = 1;
        while (true)
        {
            stagePath = stageDirectoryPath + "/Level_" + i;
            targetPath = targetDirectoryPath + "/Level_" + i;

            StageConfig[] stageConfigs = Resources.LoadAll<StageConfig>(stagePath);
            TargetConfig[] targetConfigs = Resources.LoadAll<TargetConfig>(targetPath);

            if (stageConfigs != null && stageConfigs.Length > 0)
            {
                LevelData levelData = new LevelData
                {
                    stageConfigs = stageConfigs,
                    targetConfigs = targetConfigs
                };

                levelDatas.Add(levelData);

                i++;
            }
            else break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
