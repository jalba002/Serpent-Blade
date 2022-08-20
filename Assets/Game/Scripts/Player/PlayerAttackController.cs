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
        private bool attackComboTimerDone;
        Coroutine punchComboCo;
        private int attackCombo = 1;
        private bool isAttacking = false;

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
            if (isAttacking) return;

            shieldController.gameObject.SetActive(true);
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

            if (!attackComboTimerDone)
            {
                isAttacking = true;
                attackCombo++;
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
        }

        public void EnableAttackCollider(bool active)
        {
            SwordCollider.enabled = active;
        }
    }
}