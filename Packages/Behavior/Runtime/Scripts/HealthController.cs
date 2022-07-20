using UnityEngine;
using Photon.Pun;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class HealthController : MonoBehaviourPun, KD_IDamage
{
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Dead", gameObject);
            Destroy(this.gameObject);
            RespawnSystem.Instance.CallRespawnPlayer();
        }
    }
}

// create a custom editor for this class
#if UNITY_EDITOR
[CustomEditor(typeof(HealthController))]
public class HealthControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Take Damage"))
        {
            var script = target as HealthController;
            script.TakeDamage(100f);
        }
    }
}
#endif