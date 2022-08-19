using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Boss
{
    public class Headslam : State
    {
        protected override void OnStateInitialize()
        {
            _attackData = GetAttackData("Headslam");
            if (_attackData != null)
            {
                Debug.Log($"{this.GetType().ToString()} is ready.");
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
            _stateMachine.SetAnimationTrigger("Headslam");
        }

        protected override void OnStateExit()
        {
        }
    }
}