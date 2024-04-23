using Player.Scriptables;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{
    public PlayerStats _statsTarget => target as PlayerStats;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset Stats"))
        {
            _statsTarget.ResetStats();
        }
    }
}
