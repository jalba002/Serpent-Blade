using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Boss
{
    public class Slam : State
    {
        protected override void OnStateInitialize()
        {
            _attackData = GetAttackData("Slam");
            if (_attackData != null)
            {
                Debug.Log("ALL LOADED GOOD!");
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
        }

        protected override void OnStateEnter()
        {
            _stateMachine.SetAnimationTrigger("Slam");
            // Analyze how much time it takes and wait for that, OR wait the specified time.
        }

        protected override void OnStateExit()
        {
        }
    }
}