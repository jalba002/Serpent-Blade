using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class BossDeath : State
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
        _stateMachine.SetAnimationTrigger("Death");
    }

    protected override void OnStateExit()
    {
        
    }
}
