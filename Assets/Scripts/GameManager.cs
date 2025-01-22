using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform Holder;
    [SerializeField] private Transform KnifeHolder;
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

        StageConfig stageConfig = currentLevelData.stageConfigs[Random.Range(0, currentLevelData.stageConfigs.Count - 2)];
        TargetConfig targetConfig = currentLevelData.targetConfigs[Random.Range(0, currentLevelData.targetConfigs.Count - 2)];

        Holder.GetComponent<SpawnObjects>().ResetSpawnObjects(stageConfig);
        Holder.GetComponent<Rotate>().ResetRotate(targetConfig);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q is being held down");
            StageConfig stageConfig = currentLevelData.stageConfigs[Random.Range(0, currentLevelData.stageConfigs.Count - 2)];
            TargetConfig targetConfig = currentLevelData.targetConfigs[Random.Range(0, currentLevelData.targetConfigs.Count - 2)];

            Holder.GetComponent<SpawnObjects>().ResetSpawnObjects(stageConfig);
            Holder.GetComponent<Rotate>().ResetRotate(targetConfig);
        }
    }
}
