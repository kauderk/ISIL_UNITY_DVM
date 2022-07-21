using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KD_UIHealth : MonoBehaviour, IDamage
{
    TMP_Text text;
    private void Awake() => text = GetComponent<TMP_Text>();

    public void CurrentHealth(float health)
    {
        text.text = health.ToString();
    }

    public void TakeDamage(float damage)
    {
        //throw new System.NotImplementedException();
    }
}
