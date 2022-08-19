using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Boss
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private Transform _defaultSpawnPoint;

        [SerializeField] private StateMachine _stateMachine;

        public void SpawnBullet(Object prefab)
        {
            BulletSpawnManager.Instance.SpawnBullet(prefab, _defaultSpawnPoint.position);
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Slam"))
            {
                // Apply slam animation
                //_animator.SetTrigger("Slam");
                _stateMachine.SwitchState<Slam>();
            }

            if (GUILayout.Button("Headslam"))
            {
                // Apply headslam animation
                //_animator.SetTrigger("Headslam");
                _stateMachine.SwitchState<Headslam>();
            }
        }
    }
}