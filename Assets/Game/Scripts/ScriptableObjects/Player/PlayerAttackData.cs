using UnityEngine;

[CreateAssetMenu(fileName = "Data_PlayerAttack", menuName = "Data/PlayerAttackData")]
public class PlayerAttackData : ScriptableObject
{
    public enum AttackType
    {
        First,
        Second,
        Third
    }
    public AttackType ThisAttackType;
    public float StartPctTime = 0.0f;
    public float EndPctTime = 0.0f;

    public float AttackDamage = 1f;
}
