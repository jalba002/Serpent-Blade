namespace Boss
{
    public class Headslam : State
    {
        protected override void OnStateInitialize()
        {
            _attackData = GetAttackData("Headslam");
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
                _stateMachine.SwitchState<Headslam_Loop>();
            }
        }

        protected override void OnStateEnter()
        {
            _stateMachine.SetAnimationTrigger("Headslam");
            _stateMachine.SetRotationSpeed(_attackData.attackRotationSpeed);
        }

        protected override void OnStateExit()
        {
            
        }
    }
}