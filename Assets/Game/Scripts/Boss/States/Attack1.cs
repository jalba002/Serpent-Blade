using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Boss
{
    public class Attack1 : State
    {
        protected override void OnStateInitialize()
        {
            _attackData = GetAttackData("Attack1");
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
        }

        protected override void OnStateExit()
        {
        }
    }
}