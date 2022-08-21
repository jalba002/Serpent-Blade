using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class LightningRays : State
{
    private float timeToBackScene;
    protected override void OnStateInitialize()
    {
        _attackData = _stateMachine.GetAttackData("LightningRays");
        timeToBackScene = _attackData.stateDuration + Time.timeSinceLevelLoad;
    }

    protected override void OnStateUpdate(float deltaTime)
    {
        
    }

    protected override void OnStateFixedUpdate(float fixedDeltaTime)
    {
        
    }

    protected override void OnStateCheckTransition()
    {
        if (timeToBackScene <= Time.timeSinceLevelLoad || animationFinished)
        {
            _stateMachine.SwitchState<Idle>();
        }
    }

    protected override void OnStateEnter()
    {
        // 
        _stateMachine.SetAnimationTrigger("High Shout");   
        _stateMachine.SetRotationSpeed(_attackData.attackRotationSpeed);
    }

    protected override void OnStateExit()
    {
        // Idk
        Debug.Log("Exiting Lightning Rays");
        _stateMachine.SetDefaultRotationSpeed();

    }
}
