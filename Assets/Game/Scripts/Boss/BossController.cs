using System;
using System.Collections;
using System.Collections.Generic;
using Player;
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

        [SerializeField] private Transform bossParent;

        [SerializeField] private List<Transform> bossBones;

        private PlayerAttackController _archnemesis;

        public float startingRotationSpeed = 180f;
        private float currentRotationSpeed;

        private void Awake()
        {
            _archnemesis = FindObjectOfType<PlayerAttackController>();
            // If not found, try again later?
        }

        private void Start()
        {
            currentRotationSpeed = startingRotationSpeed;
        }

        public void Update()
        {
            // Rotate slowly towards player. Let users configure speed.
            Vector3 playerDirection = _archnemesis.transform.position - bossParent.transform.position;
            playerDirection.y = 0f;
            bossParent.transform.rotation = Quaternion.LerpUnclamped(bossParent.transform.rotation,
                Quaternion.LookRotation(playerDirection), Mathf.Deg2Rad * currentRotationSpeed * Time.deltaTime);
        }

        public void StorePosition(Object spawnPoint)
        {
            lastSpawnPoint = ((GameObject) spawnPoint).transform.position;
        }

        public void StoreBonePosition(string boneName)
        {
            lastSpawnPoint = bossBones
                .Find(x => String.Equals(x.name, boneName, StringComparison.CurrentCultureIgnoreCase)).transform
                .position;
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
                _stateMachine.SwitchState<Scream>();
            }

            if (GUILayout.Button("Sunshine"))
            {
                //_stateMachine.SwitchState<Sunshine>();
            }

            if (GUILayout.Button("Laser Beam"))
            {
                _stateMachine.SwitchState<LaserBeam>();
            }
            
            if (GUILayout.Button("Laser Scream"))
            {
                _stateMachine.SwitchState<LaserScream>();
            }
            
            if (GUILayout.Button("Electric Shout"))
            {
                _stateMachine.SwitchState<ElectricShout>();
            }
            if (GUILayout.Button("Entrada"))
            {
                _stateMachine.SwitchState<Entrance>();
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

        public void ShowcaseLaser()
        {
            laserBeam.Showcase();
        }

        public Transform GetBossParent()
        {
            return bossParent;
        }

        public void SetDefaultRotationSpeed()
        {
            currentRotationSpeed = startingRotationSpeed;
        }
        
        public void SetRotationSpeed(float speed)
        {
            currentRotationSpeed = speed;
        }
        
        public float GetRotationSpeed()
        {
            return currentRotationSpeed;
        }

        public void SetStateAnimFinished()
        {
            _stateMachine.SetStateAnimFinished();
        }
    }
}