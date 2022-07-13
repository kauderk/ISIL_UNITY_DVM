using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponMagazine", menuName = "Game Data/WeaponMagazine")]
public class SO_WeaponMagazine : ScriptableObject, KD_IWeaponMagazine
{
    [field: SerializeField]
    public int magazineSize { get; private set; } = 10;

    [field: SerializeField]
    public int amoution { get; set; } = 10;

    [field: SerializeField]
    public TYPEWEAPON Type { get; private set; }

    public void consume(int amount = 1) => amoution -= amount;
    public void load(int amount = 1) => amoution += amount;
    public void fill() => amoution = magazineSize;
}
