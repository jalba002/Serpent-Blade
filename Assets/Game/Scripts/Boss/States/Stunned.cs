using System.Collections;
using System.Collections.Generic;
using Boss;
using FMOD;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Stunned : State
{
    private float stateTimeToExit;
    private bool waitForFall = true;
    protected override void OnStateInitialize()
    {
        _attackData = _stateMachine.GetAttackData("Stunned");
        stateTimeToExit = _attackData.stateDuration + Time.timeSinceLevelLoad;
        waitForFall = true;
    }

    protected override void OnStateUpdate(float deltaTime)
    {
        
    }

    protected override void OnStateFixedUpdate(float fixedDeltaTime)
    {
        
    }

    protected override void OnStateCheckTransition()
    {
        if (animationFinished && waitForFall)
        {
            stateTimeToExit = Time.timeSinceLevelLoad+_attackData.stateDuration;
            animationFinished = false;
            waitForFall = false;
        }
        else if (stateTimeToExit <= Time.timeSinceLevelLoad)
        {
            // time to change state.
            _stateMachine.SetAnimationTrigger("Stun Recovery");
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
        _stateMachine.SetDefaultRotationSpeed();
    }
}
