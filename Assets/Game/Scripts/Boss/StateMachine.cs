using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Boss
{
    public class StateMachine : MonoBehaviour
    {
        public bool startAutomatically = true;
        protected State _currentState;

        [SerializeField] private bool _instantiateAttackStorer = true;
        [SerializeField] private AttackStorer _attackStorer;
        [SerializeField] private Animator _animator;

        private BossController _bossController;

        private void Awake()
        {
            if(_instantiateAttackStorer)
                _attackStorer = Instantiate(_attackStorer);
            
            _bossController = GetComponent<BossController>();
        }

        private void Start()
        {
            if(startAutomatically)
                SwitchState<Spawn>();
        }

        public void Update()
        {
            UpdateTick(Time.deltaTime);
        }

        public void FixedUpdate()
        {
            FixedUpdateTick(Time.fixedDeltaTime);
        }

        public void UpdateTick(float dt)
        {
            _currentState?.Update(dt);
            _currentState?.CheckTransition();
        }

        public void FixedUpdateTick(float fdt)
        {
            _currentState?.FixedUpdate(fdt);
        }

        public void SwitchState<StateClass>() where StateClass : State, new()
        {
            if (_currentState != null && _currentState.GetType() == typeof(StateClass))
            {
                Debug.Log("Repeated state.");
                return;
            }
            if(_currentState != null)
                Debug.Log($"{_currentState.GetType().ToString()} to {typeof(StateClass)}");
            
            _currentState?.Exit();
            _currentState = new StateClass();
            _currentState?.Initialize(this);
            _currentState?.Enter();
        }

        public State GetCurrentState()
        {
            return _currentState;
        }

        public void RequestSwitchState<StateClass>() where StateClass : State, new()
        {
            // Add the request to a queue and if it's ready to go, do it.
        }

        #region Data
        
        public void SetCurrentWaitTime(float time)
        {
            _bossController.SetCurrentWaitTime(time);
        }

        public void AddOverload(float amount)
        {
            _bossController.AddOverload(amount);
        }

        public void SetOverload(float amount)
        {
            _bossController.SetOverload(amount);
        }

        public void SetStateAnimFinished()
        {
            _currentState.animationFinished = true;
        }

        public void SetRotationSpeed(float speed)
        {
            _bossController.SetRotationSpeed(speed);
        }

        public float GetRotationSpeed()
        {
            return _bossController.GetRotationSpeed();
        }

        public void SetDefaultRotationSpeed()
        {
            _bossController.SetDefaultRotationSpeed();
        }

        public AttackData GetAttackData(string attackData)
        {
            return _attackStorer.GetAttackData(attackData);
        }
        
        public void SetAnimationTrigger(string name)
        {
            _animator.SetTrigger(name);
        }

        public void ResetAnimationTrigger(string name)
        {
            _animator.ResetTrigger(name);
        }

        #endregion
    }
}