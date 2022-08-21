using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerHealthManager))]
public class PlayerHealthManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerHealthManager manager = target as PlayerHealthManager;

        if (manager == null)
            return;

        GUILayout.Label("\nDebug Mode");

        if (GUILayout.Button("Increase 10 HP"))
        {
            manager.IncreaseHealth(10f);
        }

        if (GUILayout.Button("Lose 10 HP"))
        {
            manager.DecreaseHealth(10f);
        }

        if (GUILayout.Button("Lose 50 HP"))
        {
            manager.DecreaseHealth(50f);
        }
    }
}