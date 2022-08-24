namespace Boss
{
    public class Stunned : State
    {
        private float stateTimeToExit;

        protected override void OnStateInitialize()
        {
            _attackData = _stateMachine.GetAttackData("Stunned");
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
                _stateMachine.SwitchState<Stunned_Loop>();
            }
        }

        protected override void OnStateEnter()
        {
            _stateMachine.SetAnimationTrigger("Stunned");
            _stateMachine.SetRotationSpeed(_attackData.attackRotationSpeed);
        }

        protected override void OnStateExit()
        {
        }
    }
}