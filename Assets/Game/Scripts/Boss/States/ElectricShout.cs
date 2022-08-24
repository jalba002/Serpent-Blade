using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class ElectricShout : State
{
    protected override void OnStateInitialize()
    {
        _attackData = _stateMachine.GetAttackData("ElectricShout");
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
        _stateMachine.SetAnimationTrigger("Electric Shout");
    }

    protected override void OnStateExit()
    {
    }
}