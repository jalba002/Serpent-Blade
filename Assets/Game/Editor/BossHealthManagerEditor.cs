using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BossHealthManager))]
public class BossHealthManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BossHealthManager manager = target as BossHealthManager;

        if (manager == null)
            return;

        GUILayout.Label("\nDebug Mode");

        if (GUILayout.Button("Increase 100 HP"))
        {
            manager.IncreaseHealth(100f);
        }

        if (GUILayout.Button("Lose 100 HP"))
        {
            manager.DecreaseHealth(100f);
        }
    }
}