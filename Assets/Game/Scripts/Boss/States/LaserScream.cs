using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Boss
{
    public class LaserScream : State
    {
        private float stateTimeToExit;
        protected override void OnStateInitialize()
        {
            _attackData = GetAttackData("LaserScream");
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
                // time to change state.
                _stateMachine.SwitchState<Idle>();
            }
        }

        protected override void OnStateEnter()
        {
            _stateMachine.SetAnimationTrigger("Laser Scream");
            _stateMachine.SetRotationSpeed(_attackData.attackRotationSpeed);
        }

        protected override void OnStateExit()
        {
            _stateMachine.SetDefaultRotationSpeed();
            //_stateMachine.SetAnimationTrigger("Headslam Recover");
        }
    }
}