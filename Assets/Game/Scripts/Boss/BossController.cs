using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Object = UnityEngine.Object;

namespace Boss
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private Transform _defaultSpawnPoint;

        [SerializeField] private StateMachine _stateMachine;

        private Vector3 lastSpawnPoint;

        // public void SpawnBullet(Object prefab)
        // {
        //     BulletSpawnManager.Instance.SpawnBullet(prefab, _defaultSpawnPoint.position);
        // }

        [SerializeField] private LaserBeamScript laserBeam;
       
        public Transform bossParent { get; private set; }
        
        [SerializeField]
        private List<Transform> bossBones;

        private void Awake()
        {
            bossParent = GetComponentInParent<Transform>();
        }

        public void StorePosition(Object spawnPoint)
        {
            lastSpawnPoint = ((GameObject) spawnPoint).transform.position;
        }

        public void StoreBonePosition(string boneName)
        {
            lastSpawnPoint = bossBones.Find(x => String.Equals(x.name, boneName, StringComparison.CurrentCultureIgnoreCase)).transform.position;
        }

        public void SpawnBulletLastPoint(Object bulletPrefab)
        {
            BulletSpawnManager.Instance.SpawnBullet(bulletPrefab, lastSpawnPoint);
        }

        public void SpawnBullet(Object spawnPoint, string bulletName)
        {
            BulletSpawnManager.Instance.SpawnBullet(spawnPoint, bulletName);
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Headslam"))
            {
                // Apply headslam animation
                //_animator.SetTrigger("Headslam");
                _stateMachine.SwitchState<Headslam>();
            }
            if (GUILayout.Button("Scream"))
            {
                // Apply headslam animation
                //_animator.SetTrigger("Headslam");
                _stateMachine.SwitchState<Scream>();
            }
            if (GUILayout.Button("Sunshine"))
            {
                // Apply headslam animation
                //_animator.SetTrigger("Headslam");
                //_stateMachine.SwitchState<Sunshine>();
            }
            if (GUILayout.Button("Laser Beam"))
            {
                // Apply headslam animation
                //_animator.SetTrigger("Headslam");
                _stateMachine.SwitchState<LaserBeam>();
            }
        }

        public void ToggleLaser(int enable)
        {
            // find the particle and call it to start shooting.
            //if positive, go, if 0 or negative. Stop.
            if (enable >= 1)
            {
                laserBeam.Shoot();
            }
            else
            {
                laserBeam.Stop();
            }
        }
    }
}