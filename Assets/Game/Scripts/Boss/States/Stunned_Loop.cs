using UnityEngine;

namespace Boss
{
    public class Stunned_Loop : State
    {
        private float stateTimeToExit;

        protected override void OnStateInitialize()
        {
            _attackData = _stateMachine.GetAttackData("Stunned");
            if (_attackData != null)
            {
                stateTimeToExit = _attackData.stateDuration + Time.timeSinceLevelLoad;
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
                _stateMachine.SwitchState<Stunned_Recovery>();
            }
        }

        protected override void OnStateEnter()
        {
            _stateMachine.SetRotationSpeed(_attackData.attackRotationSpeed);
        }

        protected override void OnStateExit()
        {
            _stateMachine.SetDefaultRotationSpeed();
            _stateMachine.SetOverload(0f);
        }
    }
}