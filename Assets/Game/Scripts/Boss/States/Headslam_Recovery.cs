namespace Boss
{
    public class Headslam_Recovery : State
    {
        protected override void OnStateInitialize()
        {
            _attackData = _stateMachine.GetAttackData("Headslam");
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
                _stateMachine.SwitchState<Idle>();
            }
        }

        protected override void OnStateEnter()
        {
            _stateMachine.SetAnimationTrigger("Headslam Recover");
        }

        protected override void OnStateExit()
        {
            
        }
    }
}