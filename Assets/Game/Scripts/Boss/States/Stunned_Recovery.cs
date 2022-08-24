namespace Boss
{
    public class Stunned_Recovery : State
    {
        private float stateTimeToExit;
        private bool waitForFall = true;

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
                // time to change state.
                _stateMachine.SwitchState<Idle>();
            }
        }

        protected override void OnStateEnter()
        {
            _stateMachine.SetAnimationTrigger("Stun Recovery");
        }

        protected override void OnStateExit()
        {
        }
    }
}