using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Boss
{
    public class Headslam : State
    {
        private float stateTimeToExit;
        protected override void OnStateInitialize()
        {
            _attackData = GetAttackData("Headslam");
            stateTimeToExit = Time.timeSinceLevelLoad+_attackData.stateDuration;
            Debug.Log(Time.timeSinceLevelLoad + "/" + stateTimeToExit);
        }

        protected override void OnStateUpdate(float deltaTime)
        {
        }

        protected override void OnStateFixedUpdate(float fixedDeltaTime)
        {
        }

        protected override void OnStateCheckTransition()
        {
            if (stateTimeToExit <= Time.timeSinceLevelLoad)
            {
                // time to change state.
                _stateMachine.SwitchState<Idle>();
            }
        }

        protected override void OnStateEnter()
        {
            _stateMachine.SetAnimationTrigger("Headslam");
            _stateMachine.SetRotationSpeed(_attackData.attackRotationSpeed);
        }

        protected override void OnStateExit()
        {
            _stateMachine.SetAnimationTrigger("Headslam Recover");
            _stateMachine.SetDefaultRotationSpeed();
        }
    }
}