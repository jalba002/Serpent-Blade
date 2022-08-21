using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_BossAttack", menuName = "Data/BossAttackData")]
// This class contains all basic attack data for every boss state.
public class AttackData : ScriptableObject
{
    /// <summary>
    /// Animation speed to execute the animation.
    /// </summary>
    public float animationSpeed = 1f;

    /// <summary>
    /// Time in seconds the boss will wait until doing the next attack.
    /// </summary>
    public float waitTime = 0f;

    /// <summary>
    /// State duration in seconds. If negative, the animation duration is done. Anyway, state transitions.
    /// </summary>
    public float stateDuration = -1f;

    /// <summary>
    /// Rotation speed of the creature during the attack.
    /// </summary>
    public float attackRotationSpeed = 0f;
}
