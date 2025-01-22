using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Target/TargetConfig", fileName = "TargetConfig")]
public class TargetConfig : ScriptableObject
{
    [field: SerializeField] public bool isTargetRotatingContinuously { get; private set; }
    [field: SerializeField] public bool isReverve { get; private set; }
    [field: SerializeField] public bool isCounterClockwise { get; private set; }

    [field: SerializeField] public float initAngularVel { get; private set; }
    [field: SerializeField] public float rotationSpeed { get; private set; }
    [field: SerializeField] public float rotationDuration { get; private set; }
    [field: SerializeField] public float anglDamping { get; private set; }
}