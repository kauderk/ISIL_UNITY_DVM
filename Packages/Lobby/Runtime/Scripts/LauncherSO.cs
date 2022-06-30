using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISObject_ : ScriptableObject
{
    public string Name;
}
[CreateAssetMenu(fileName = "LauncherSO", menuName = "C6_ISIL_UNITY/LauncherSO", order = 0)]
public class LauncherSO : ISObject_
{

}