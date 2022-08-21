using FMODUnity;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        private Animator animator;
        private ShieldController shieldController;

        public BoxCollider SwordCollider;

        public float AttackComboValidTime = 0.5f;
        [SerializeField] private bool attackComboTimerDone;
        Coroutine punchComboCo;
        [SerializeField] private int attackCombo = 1;
        [SerializeField] private bool isAttacking = false;

        public float ShieldCooldown = 2f;
        public float NextShieldTime;

        private PlayerAttackData currentAttack;

        public StudioEventEmitter BlockEventEmitter;
        public StudioEventEmitter Attack1EventEmitter;
        public StudioEventEmitter Attack2EventEmitter;
        public StudioEventEmitter Attack3EventEmitter;

        private PlayerHealthManager healthManager;

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            shieldController = GetComponentInChildren<ShieldController>();
            healthManager = GetComponent<PlayerHealthManager>();
        }

        private void Start()
        {
            shieldController.gameObject.SetActive(false);
        }

        public void OnShield()
        {
            if (isAttacking || NextShieldTime >= Time.time) return;

            NextShieldTime = Time.time + ShieldCooldown;
            shieldController.gameObject.SetActive(true);
            animator.SetTrigger("Block");
            healthManager.InvulnerableOverTime(0.2f);
            BlockEventEmitter.Play();
        }

        public void OnAttack()
        {
            if (isAttacking || attackCombo > 3)
                return;

            if (punchComboCo != null)
                StopCoroutine(punchComboCo);

            if (attackComboTimerDone)
            {
                isAttacking = true;
                attackComboTimerDone = false;
            }

            animator.SetTrigger("Attack");
            animator.SetInteger("AttackCombo", attackCombo);
            animator.SetBool("InCombo", true);

            if (!attackComboTimerDone)
            {
                isAttacking = true;
                attackCombo++;
            }

            switch (attackCombo)
            {
                case 2:
                    Attack1EventEmitter.Play();
                    break;
                case 3:
                    Attack2EventEmitter.Play();
                    break;
                case 4:
                    Attack3EventEmitter.Play();
                    break;
            }
        }

        public void ResetAttackComboTimer()
        {
            isAttacking = false;
            punchComboCo = StartCoroutine(AttackComboTimerCo());
        }

        IEnumerator AttackComboTimerCo()
        {
            yield return new WaitForSeconds(AttackComboValidTime);
            attackComboTimerDone = true;
            attackCombo = 1;
            animator.SetBool("InCombo", false);
        }

        public void EnableAttackCollider(bool active)
        {
            SwordCollider.enabled = active;
        }

        public PlayerAttackData GetCurrentAttack()
        {
            return currentAttack;
        }

        public void SetCurrentAttack(PlayerAttackData newAttack)
        {
            currentAttack = newAttack;
        }
    }
}