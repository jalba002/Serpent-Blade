using System.Collections;
using System.Collections.Generic;
using Boss;
using FMOD;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Stunned : State
{
    private float timeToExit;
    protected override void OnStateInitialize()
    {
        _attackData = _stateMachine.GetAttackData("Stunned");
        timeToExit = _attackData.stateDuration + Time.timeSinceLevelLoad;
    }

    protected override void OnStateUpdate(float deltaTime)
    {
        
    }

    protected override void OnStateFixedUpdate(float fixedDeltaTime)
    {
        
    }

    protected override void OnStateCheckTransition()
    {
        if (timeToExit <= Time.timeSinceLevelLoad)
        {
            _stateMachine.SwitchState<Idle>();
        }
    }

    protected override void OnStateEnter()
    {
        _stateMachine.SetAnimationTrigger("Stunned");
        _stateMachine.SetRotationSpeed(_attackData.attackRotationSpeed);
    }

    protected override void OnStateExit()
    {
        _stateMachine.SetAnimationTrigger("Stun Recovery");
        _stateMachine.SetDefaultRotationSpeed();
    }
}
