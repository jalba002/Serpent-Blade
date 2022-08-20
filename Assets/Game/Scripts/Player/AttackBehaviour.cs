using Player;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    enum AttackStates
    {
        startUp,
        active,
        recover,
        finished
    }

    private AttackStates attackState;
    public PlayerAttackData AttackData;
    PlayerAttackController playerAttackController;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackState = AttackStates.startUp;
        playerAttackController = animator.GetComponentInParent<PlayerAttackController>();
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= AttackData.StartPctTime && attackState == AttackStates.startUp)
        {
            playerAttackController.EnableAttackCollider(true);
            attackState = AttackStates.active;
        }
        else if (stateInfo.normalizedTime >= AttackData.EndPctTime && attackState == AttackStates.active)
        {
            playerAttackController.EnableAttackCollider(false);
            attackState = AttackStates.recover;
        }
        else if (stateInfo.normalizedTime >= 0.95f && attackState != AttackStates.finished)
        {
            playerAttackController.ResetAttackComboTimer();
            attackState = AttackStates.finished;
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {

    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {

    }
}
