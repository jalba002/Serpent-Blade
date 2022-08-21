using System;
using Boss;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Boss
{
    public abstract class State
    {
        protected StateMachine _stateMachine;
        protected AttackData _attackData;
        public bool animationFinished = false;

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            OnStateInitialize();
        }

        public void Update(float dt)
        {
            OnStateUpdate(dt);
        }

        public void FixedUpdate(float fdt)
        {
            OnStateFixedUpdate(fdt);
        }

        public void CheckTransition()
        {
            OnStateCheckTransition();
        }

        public void Enter()
        {
            OnStateEnter();
        }

        public void Exit()
        { 
            OnStateExit();
            if(_attackData != null)
                _stateMachine.AddOverload(_attackData.overloadAmount);
        }

        protected abstract void OnStateInitialize();
        protected abstract void OnStateUpdate(float deltaTime);
        protected abstract void OnStateFixedUpdate(float fixedDeltaTime);
        protected abstract void OnStateCheckTransition();
        protected abstract void OnStateEnter();
        protected abstract void OnStateExit();

        protected AttackData GetAttackData(string attackDataName)
        {
            return _stateMachine.GetAttackData(attackDataName);
        }
    }
}