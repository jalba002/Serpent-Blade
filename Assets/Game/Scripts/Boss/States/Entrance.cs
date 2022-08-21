using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class Entrance : State
{
    protected override void OnStateInitialize()
    {
        
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
        _stateMachine.SetAnimationTrigger("Enter");
    }

    protected override void OnStateExit()
    {
        
    }
}
