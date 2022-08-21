using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class Spawn : State
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
        
    }

    protected override void OnStateEnter()
    {
        _stateMachine.SetAnimationTrigger("Spawn");
    }

    protected override void OnStateExit()
    {
    }
}
