using UnityEngine;
using Photon.Pun;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class HealthController : MonoBehaviourPun, IDamage, IPlayerStatsSubscriber
{
    [field: SerializeField]
    public PlayerStats Stats { get; private set; }

    public void OnStatsChanged(PlayerStats stats) => Stats = stats;

    public void TakeDamage(float damage)
    {
        Stats.Health -= damage;
        if (Stats.Health <= 0)
        {
            Destroy(this.gameObject);
            RespawnSystem.Instance.CallRespawnPlayer(Stats);
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