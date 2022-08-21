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

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            shieldController = GetComponentInChildren<ShieldController>();
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