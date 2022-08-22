using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using Player;
using UnityEngine;
using UnityEngine.VFX;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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

        public float maximumOverload = 50f;
        private float currentOverload = 0f;

        private bool SetToBeStunned = false;

        [Header("Debug")] 
        public bool debugGUI = false;

        public bool UpdateAI = true;

        private void Awake()
        {
            _archnemesis = FindObjectOfType<PlayerAttackController>();
            // If not found, try again later?
        }

        private void Start()
        {
            currentRotationSpeed = startingRotationSpeed;
            StartCoroutine(EntranceCoroutine());
        }

        IEnumerator EntranceCoroutine()
        {
            yield return new WaitForSeconds(2f);
            _stateMachine.SwitchState<Entrance>();
        }

        public void Update()
        {
            // Rotate slowly towards player. Let users configure speed.
            RotateTowardsPlayer();
            if(UpdateAI)
                BossAI();
        }

        private void RotateTowardsPlayer()
        {
            Vector3 playerDirection = _archnemesis.transform.position - bossParent.transform.position;
            playerDirection.y = 0f;
            bossParent.transform.rotation = Quaternion.LerpUnclamped(bossParent.transform.rotation,
                Quaternion.LookRotation(playerDirection), Mathf.Deg2Rad * currentRotationSpeed * Time.deltaTime);
        }


        [Header("Boss AI Settings")] private float currentWaitTime = 0f;

        private float closeDistance = 16f;
        private float mediumDistance = 20f;

        private int slamBias = 0; //1
        private int laserAttackBias = 0; //2
        private int electricBias = 0; //3
        private int projectileBias = 0; //4

        private int[] closeChances = new[]
        {
            30, // Rock
            35, // Rays
            25, // Electric
            10 // Projectiles
        };

        private int[] mediumChances = new[]
        {
            5, // Rock
            35, // Rays
            20, // Electric
            40 // Projectiles
        };

        private int[] farChances = new[]
        {
            20, // Rock
            15, // Rays
            35, // Electric
            30 // Projectiles
        };


        private void BossAI()
        {
            // This works on the boss AI. Everything is done, just use and balance the attacks.
            // Try to only select the next state when the current one is on IDLE.
            // Then, weight every attack and do some shenanigans to select it.
            if (_stateMachine.GetCurrentState().GetType() != typeof(Idle)) return;
            
            if (currentWaitTime >= 0f)
            {
                currentWaitTime -= Time.deltaTime;
                return;
            }

            currentWaitTime = 0.1f;
            
            if (SetToBeStunned)
            {
                _stateMachine.SwitchState<Stunned>();
                SetToBeStunned = false;
                //currentOverload = 0f;
                return;
            }

            Vector3 bossPlayerVector = _archnemesis.transform.position - bossParent.transform.position;
            bossPlayerVector.y = 0f;
            float playerDistance = bossPlayerVector.magnitude;

            int chance = Random.Range(0, 101);
            int[] selectedChances;
            int distance = 0;
           

            if (playerDistance < closeDistance)
            {
                // Slam or any of the rays.
                // If bad luck, sunshine. Or another projectiles
                // Shout laser if the projectile and electric bias are high. 
                selectedChances = closeChances;
                distance = 0;
            }
            else if (playerDistance < mediumDistance)
            {
                // Close ray, but shout ray if annoyed or not overloaded.
                // Sunshine more ocasionally.
                // Absolutely no slam, open to attacks.
                // Rays if the players doesnt like to come close. Do not repeat rays if High bias.
                selectedChances = mediumChances;
                distance = 1;
                laserAttackBias -= 5;
                electricBias -= 10;
            }
            else
            {
                // 0 chance for slam
                // no close laser, maybe shout laser
                // more projectiles and WAY LOT RAYS. Use rays in combo to then projectiles. High bias
                // 
                selectedChances = farChances;
                distance = 2;
                electricBias -= 10;
                projectileBias -= 5;
            }

            chance -= selectedChances[0];
            chance += slamBias;
            if (chance <= 0)
            {
                //rock
                chance = Random.Range(0, 101);
                if(chance < 85)
                {
                    _stateMachine.SwitchState<Headslam>();
                }
                else
                {
                    _stateMachine.SwitchState<Scream>();
                }

                slamBias += 20;
                return;
            }

            chance -= selectedChances[1];
            chance += laserAttackBias;
            if (chance <= 0)
            {
                laserAttackBias += 10;
                //ray
                if (distance == 0)
                {
                    chance = Random.Range(0, 101);
                    if (chance < 70)
                    {
                        _stateMachine.SwitchState<LaserBeam>();
                    }
                    else if (chance < 95)
                    {
                        laserAttackBias += 10;
                        _stateMachine.SwitchState<LaserScream>();
                    }
                    else
                    {
                        _stateMachine.SwitchState<Scream>();
                    }
                }
                else if (distance == 1)
                {
                    chance = Random.Range(0, 101);
                    if (chance < 20)
                    {
                        _stateMachine.SwitchState<LaserBeam>();
                    }
                    else
                    {
                        laserAttackBias += 10;
                        _stateMachine.SwitchState<LaserScream>();
                    }
                }
                else if (distance == 2)
                {
                    laserAttackBias += 25;
                    _stateMachine.SwitchState<LaserScream>();
                }

                return;
            }

            chance -= selectedChances[2];
            chance += electricBias;
            if (chance <= 0)
            {
                electricBias += 20;
                _stateMachine.SwitchState<ElectricShout>();
                //electric
                if (distance != 0)
                {
                    projectileBias -= 10;
                    electricBias += 20;
                }

                return;
            }

            chance -= selectedChances[3];
            chance += projectileBias;
            if (chance <= 0)
            {
                //projectiles
                if (distance == 0)
                {
                    projectileBias += 60;
                }
                else if (distance == 1)
                {
                    projectileBias += 20;
                    laserAttackBias -= 10;
                }
                else if (distance == 2)
                {
                    projectileBias += 15;
                    laserAttackBias -= 30;
                }

                _stateMachine.SwitchState<Sunshine>();
            }
        }

        public void SetCurrentWaitTime(float time)
        {
            currentWaitTime = time;
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
            if (!debugGUI) return;

            if (GUILayout.Button("Entrada"))
            {
                _stateMachine.SwitchState<Entrance>();
            }

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
                _stateMachine.SwitchState<Sunshine>();
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
            

            if (GUILayout.Button("Stunned"))
            {
                _stateMachine.SwitchState<Stunned>();
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

        public void AddOverload(float amount)
        {
            //if (amount <= 0) return;

            if (currentOverload >= maximumOverload) return;

            currentOverload = Mathf.Min(currentOverload + amount, maximumOverload);
            CheckOverload();
        }

        public void CheckOverload()
        {
            if (currentOverload < maximumOverload) return;

            SetToBeStunned = true;
            currentOverload = 0f;
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