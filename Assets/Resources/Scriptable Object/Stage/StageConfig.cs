using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Stage/StageConfig", fileName = "StageConfig")]
public class StageConfig : ScriptableObject
{
    [field: SerializeField] public int throwKnifeAmount { get; private set; }
    [field: SerializeField] public List<float> spawnKnifeAngles { get; private set; }
    [field: SerializeField] public List<float> appleAngles { get; private set; }
    [field: SerializeField] public List<float> goldenAppleAngles { get; private set; }
    [field: SerializeField] public Sprite targetSkin { get; private set; }
}