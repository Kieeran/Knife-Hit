using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public List<StageConfig> stageConfigs = new();
    public List<StageConfig> stageBossConfigs = new();
    public List<TargetConfig> targetConfigs = new();
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
        int index = 1;
        while (true)
        {
            stagePath = stageDirectoryPath + "/Level_" + index;
            targetPath = targetDirectoryPath + "/Level_" + index;

            StageConfig[] stageConfigs = Resources.LoadAll<StageConfig>(stagePath);
            TargetConfig[] targetConfigs = Resources.LoadAll<TargetConfig>(targetPath);

            if (stageConfigs != null && stageConfigs.Length > 0 && targetConfigs != null && targetConfigs.Length > 0)
            {
                LevelData levelData = new();

                for (int i = 0; i < targetConfigs.Length; i++)
                {
                    levelData.targetConfigs.Add(targetConfigs[i]);
                }

                for (int i = 0; i < stageConfigs.Length; i++)
                {
                    if (stageConfigs[i].name.Contains("Boss"))
                    {
                        levelData.stageBossConfigs.Add(stageConfigs[i]);
                    }

                    else
                    {
                        levelData.stageConfigs.Add(stageConfigs[i]);
                    }
                }

                levelDatas.Add(levelData);

                index++;
            }
            else break;
        }

        // for (int i = 0; i < levelDatas.Count; i++)
        // {
        //     for (int j = 0; j < levelDatas[i].stageConfigs.Count; j++)
        //     {
        //         Debug.Log(levelDatas[i].stageConfigs[j]);
        //     }

        //     for (int j = 0; j < levelDatas[i].targetConfigs.Count; j++)
        //     {
        //         Debug.Log(levelDatas[i].targetConfigs[j]);
        //     }

        //     for (int j = 0; j < levelDatas[i].stageBossConfigs.Count; j++)
        //     {
        //         Debug.Log(levelDatas[i].stageBossConfigs[j]);
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
