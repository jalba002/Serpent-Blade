using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Boss
{
    public class Headslam : State
    {
        private float stateTimeToExit;
        private bool waitForFall = true;
        protected override void OnStateInitialize()
        {
            _attackData = GetAttackData("Headslam");
            stateTimeToExit = Time.timeSinceLevelLoad + 999f;
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
                _stateMachine.SetAnimationTrigger("Headslam Recover");
                _stateMachine.SwitchState<Idle>();
            }
        }

        protected override void OnStateEnter()
        {
            _stateMachine.SetAnimationTrigger("Headslam");
            //_stateMachine.ResetAnimationTrigger("Headslam Recover");
            _stateMachine.SetRotationSpeed(_attackData.attackRotationSpeed);
        }

        protected override void OnStateExit()
        {
            _stateMachine.SetDefaultRotationSpeed();
        }
    }
}