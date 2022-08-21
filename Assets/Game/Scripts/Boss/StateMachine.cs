using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

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
            _currentState?.Exit();
            _currentState = new StateClass();
            _currentState?.Initialize(this);
            _currentState?.Enter();
        }

        public void RequestSwitchState<StateClass>() where StateClass : State, new()
        {
            // Add the request to a queue and if it's ready to go, do it.
        }

        #region Data

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

        #endregion
    }
}