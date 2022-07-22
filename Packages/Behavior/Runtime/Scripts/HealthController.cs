using UnityEngine;
using Photon.Pun;
using System.Collections;
using Visual;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class HealthController : MonoBehaviourPun, IDamage, IPlayerStatsSubscriber
{
    [field: SerializeField]
    public PlayerStats Stats { get; private set; }
    public void CurrentHealth(float health)
    {
        //throw new System.NotImplementedException();
    }

    public void OnStatsChanged(PlayerStats stats)
    {
        Stats = stats;
        transform.NotifyChildren<IDamage>(I => I.CurrentHealth(Stats.Health));
    }

    public void TakeDamage(float damage)
    {
        Stats.Health -= damage;
        if (Stats.Health <= 0)
        {
            //gameObject.SetActive(false);
            RespawnSystem.Instance.CallRespawnPlayer(Stats);
        }
        else
        {
            transform.NotifyChildren<ISkin>(I => I.Flicker());
            transform.NotifyChildren<IDamage>(I => I.CurrentHealth(Stats.Health));
        }
    }
}

// create a custom editor for this class
#if UNITY_EDITOR
[CustomEditor(typeof(HealthController))]
public class HealthControllerEditor : Editor
{
    HealthController script;
    private void OnEnable()
    {
        script = (HealthController)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Take Damage"))
        {
            script.TakeDamage(10f);
        }
        //kill me
        if (GUILayout.Button("Kill Me"))
        {
            script.TakeDamage(script.Stats.Health + 1);
        }
    }
}
#endif