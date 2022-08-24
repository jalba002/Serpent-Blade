using UnityEngine;

namespace Boss
{
    public class Headslam_Loop : State
    {
        private float stateTimeToExit;
        private bool waitForFal = true;

        protected override void OnStateInitialize()
        {
            _attackData = GetAttackData("Headslam");
            if (_attackData)
            {
                stateTimeToExit = Time.timeSinceLevelLoad + _attackData.stateDuration;
            }
        }

        protected override void OnStateUpdate(float deltaTime)
        {
        }

        protected override void OnStateFixedUpdate(float fixedDeltaTime)
        {
        }

        protected override void OnStateCheckTransition()
        {
            if (Time.timeSinceLevelLoad >= stateTimeToExit)
            {
                _stateMachine.SwitchState<Headslam_Recovery>();
            }
        }

        protected override void OnStateEnter()
        {
            _stateMachine.SetRotationSpeed(_attackData.attackRotationSpeed);
        }

        protected override void OnStateExit()
        {
            _stateMachine.SetDefaultRotationSpeed();
        }
    }
}