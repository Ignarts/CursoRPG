using Quests;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Quest))]
public class QuestEditor :  Editor
{
    public Quest _questTarget => target as Quest;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset Quest"))
        {
            _questTarget.ResetQuest();
        }
    }
}
