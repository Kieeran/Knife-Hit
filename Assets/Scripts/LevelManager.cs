using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public List<StageConfig> stageConfigs;
    public List<StageConfig> stageBossConfigs;
    public List<TargetConfig> targetConfigs;
    public int stageCount = 5;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
