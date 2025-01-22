using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Apple/AppleConfig", fileName = "AppleConfig")]
public class AppleConfig : ScriptableObject
{
    [field: SerializeField] public int appleID { get; private set; }
    [field: SerializeField] public int appleCoinValue { get; private set; }
    [field: SerializeField] public Sprite appleSprite { get; private set; }
}