using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Boss
{
    public class FightStart : State
    {
        private float timeToExit;
        protected override void OnStateInitialize()
        {
            _attackData = GetAttackData("FightStart");
            timeToExit = Time.timeSinceLevelLoad + _attackData.stateDuration;
        }

        protected override void OnStateUpdate(float deltaTime)
        {
        }

        protected override void OnStateFixedUpdate(float fixedDeltaTime)
        {
            
        }

        protected override void OnStateCheckTransition()
        {
            // Load another state?
            // Call the StateMAchine to load another thing?
            if (Time.timeSinceLevelLoad >= timeToExit)
            {
                Debug.Log("Time to exit!");
                // Call for the parent to get switched
                _stateMachine.SwitchState<Attack1>();
            }
        }

        protected override void OnStateEnter()
        {
            Debug.Log("Default State entered.");
        }

        protected override void OnStateExit()
        {
        }
    }
}