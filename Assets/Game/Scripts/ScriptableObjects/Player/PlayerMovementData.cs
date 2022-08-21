using UnityEngine;

[CreateAssetMenu(fileName = "Data_PlayerMovement", menuName = "Data/PlayerMovementData")]
public class PlayerMovementData : ScriptableObject
{
    public float MaxSpeed = 5f;

    public float DashSpeed = 30f;
    public float DashCooldown = 1f;
    public float DashDuration = 0.1f;

    /// <summary>
    /// Speed at which the player goes from 0 to max speed and from max speed to 0
    /// </summary>
    [Range(0, 10)]
    public float SpeedChangeRate = 8.0f;

    /// <summary>
    /// Speed at which the player rotates when changing direction
    /// </summary>
    [Range(0, 1)]
    public float LerpRotationPct = 0.1f;
}
