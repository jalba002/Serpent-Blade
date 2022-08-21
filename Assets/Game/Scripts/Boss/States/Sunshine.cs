using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class Sunshine : State
{
    protected override void OnStateInitialize()
    {
        _attackData = _stateMachine.GetAttackData("Sunshine");
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
        _stateMachine.SetAnimationTrigger("Sunshine");
    }

    protected override void OnStateExit()
    {
        // none
    }
}
