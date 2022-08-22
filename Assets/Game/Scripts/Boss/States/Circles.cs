using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class Circles : State
{
    protected override void OnStateInitialize()
    {
        _attackData = _stateMachine.GetAttackData("Circles");
    }

    protected override void OnStateUpdate(float deltaTime)
    {
        
    }

    protected override void OnStateFixedUpdate(float fixedDeltaTime)
    {
        
    }

    protected override void OnStateCheckTransition()
    {
        if (animationFinished)
        {
            _stateMachine.SwitchState<Idle>();
        }
    }

    protected override void OnStateEnter()
    {
        _stateMachine.SetAnimationTrigger("Circles");
        _stateMachine.SetRotationSpeed(_attackData.attackRotationSpeed);
    }

    protected override void OnStateExit()
    {
        
    }
}
