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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
