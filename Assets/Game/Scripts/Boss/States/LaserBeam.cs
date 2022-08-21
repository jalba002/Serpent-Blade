using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Boss
{
    public class LaserBeam : State
    {
        private float stateTimeToExit;
        protected override void OnStateInitialize()
        {
            _attackData = GetAttackData("LaserBeam");
            if (_attackData.stateDuration >= 0)
            {
                stateTimeToExit = Time.timeSinceLevelLoad+_attackData.stateDuration;
            }
            // Otherwise, just let it finish by itself.
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
            _stateMachine.SetAnimationTrigger("Laser Beam");
        }

        protected override void OnStateExit()
        {
            //_stateMachine.SetAnimationTrigger("Headslam Recover");
        }
    }
}