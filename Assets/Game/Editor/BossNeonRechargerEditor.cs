using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BossNeonRecharger))]
public class BossNeonRechargerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BossNeonRecharger manager = target as BossNeonRecharger;

        if (manager == null)
            return;

        GUILayout.Label("\nDebug Mode");

        if (GUILayout.Button("Recharge"))
        {
            manager.Recharge();
        }

        if (GUILayout.Button("Lose 10 Energy"))
        {
            manager.DecreaseNeonCharge(10f);
        }
    }
}