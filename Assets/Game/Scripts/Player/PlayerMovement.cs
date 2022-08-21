using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        //Estados de player
        public enum PlayerStates
        {
            Standing,
            Dashing
        }

        private PlayerStates currentState;

        private CharacterController controller;
        private Vector2 direction;
        private Vector2 dashDirection;
        private Vector3 movement;
        private float currentSpeed;

        public Animator animator;
        private float _animationBlend;

        private float nextDashTime = 0f;

        public Transform ArenaCenter;

        public PlayerMovementData MovementData;
        private MeshTrail meshTrail;

        public StudioEventEmitter dashEventEmitter;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
            meshTrail = GetComponentInChildren<MeshTrail>();
        }

        private void Start()
        {
            currentState = PlayerStates.Standing;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            Vector3 forward = -(transform.position - ArenaCenter.position).normalized;
            Vector3 right = new Vector3(forward.z, forward.y, -forward.x);

            forward.y = 0.0f;
            right.y = 0.0f;

            forward.Normalize();
            right.Normalize();

            float targetSpeed = MovementData.MaxSpeed * direction.magnitude;

            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * MovementData.SpeedChangeRate);

            if (currentState != PlayerStates.Standing)
            {
                movement = dashDirection.y * forward + dashDirection.x * right;
                movement.Normalize();
                movement *= MovementData.DashSpeed;
            }
            else
            {
                movement = direction.y * forward + direction.x * right;
                movement.Normalize();
                movement *= currentSpeed;
            }

            movement.y = -1f;

            controller.Move(movement * Time.deltaTime);

            _animationBlend = Mathf.Lerp(_animationBlend, currentSpeed / MovementData.MaxSpeed, Time.deltaTime * MovementData.SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            if (direction != Vector2.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), MovementData.LerpRotationPct);
            }
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            animator.SetFloat("Speed", _animationBlend);
        }

        private void ChangeState(PlayerStates newState)
        {
            //OnExit
            switch (currentState)
            {
                case PlayerStates.Standing:
                    //animator.SetBool("Grounded", false);
                    break;
                case PlayerStates.Dashing:
                    //animator.SetBool("Dashing", false);
                    break;
            }

            //OnEnter
            switch (newState)
            {
                case PlayerStates.Standing:
                    //animator.SetBool("Grounded", true);
                    break;
                case PlayerStates.Dashing:
                    //animator.SetBool("Dashing", true);
                    break;
            }

            currentState = newState;
        }

        public void OnMove(InputValue value)
        {
            var inVal = value.Get<Vector2>();
            direction = inVal;
        }

        public void OnDash()
        {
            if (Time.time > nextDashTime)
            {
                nextDashTime = Time.time + MovementData.DashCooldown;
                ChangeState(PlayerStates.Dashing);
                StartCoroutine(DashCoroutine(MovementData.DashDuration));

                if (direction == Vector2.zero)
                {
                    dashDirection.x = transform.forward.x;
                    dashDirection.y = transform.forward.z;
                }
                else
                {
                    dashDirection = direction;
                }

                dashEventEmitter.Play();
            }
        }

        IEnumerator DashCoroutine(float duration)
        {
            meshTrail.ActivateTrail(duration);
            yield return new WaitForSeconds(duration);
            ChangeState(PlayerStates.Standing);
        }
    }
}