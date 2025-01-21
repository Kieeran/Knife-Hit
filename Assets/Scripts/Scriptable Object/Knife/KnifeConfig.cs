using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Configs/Knife/KnifeConfig", fileName = "KnifeConfig")]
public class KnifeConfig : ScriptableObject
{
    [field: SerializeField] public int knifeID { get; private set; }
    [field: SerializeField] public Sprite knifeSprite { get; private set; }
    [field: SerializeField] public Sprite knifeShadowSprite { get; private set; }
    [field: SerializeField] public int knifeCost { get; private set; }
}