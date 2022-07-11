using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Game Data/Game Settings", order = 1)]
public class SO_GameplaySettings : SingletonScriptableObject<SO_GameplaySettings>
{
    public float radSpeed = 10f;
}
