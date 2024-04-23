using UnityEngine;
using Entities;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    public Waypoint WaypointTarget => target as Waypoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.red;

        if(WaypointTarget.Points == null || WaypointTarget.Points.Length == 0)
            return;

        for(int point = 0; point < WaypointTarget.Points.Length; point++)
        {
            EditorGUI.BeginChangeCheck();

            Vector3 actualPoint = WaypointTarget.ActualPosition + WaypointTarget.Points[point];
            Vector3 newPoint = Handles.FreeMoveHandle(actualPoint, 0.5f, new Vector3(0.3f, 0.3f,0.3f), Handles.SphereHandleCap);

            // create text to know point number
            GUIStyle text = new GUIStyle();
            text.normal.textColor = Color.white;
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 14;
            Vector3 textPosition = Vector3.down * 0.3f + Vector3.right * 0.3f;
            Handles.Label(actualPoint + textPosition, $"{point + 1}", text);

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(WaypointTarget, "Change Waypoint Position");
                WaypointTarget.Points[point] = newPoint - WaypointTarget.ActualPosition;
            }
        }
    }
}
