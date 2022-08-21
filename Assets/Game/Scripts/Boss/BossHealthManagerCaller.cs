using Player;
using UnityEngine;

public class BossHealthManagerCaller : MonoBehaviour
{
    private BossHealthManager healthManager;

    private void Awake()
    {
        healthManager = GetComponentInParent<BossHealthManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttack")
        {
            var playerAttack = other.GetComponentInParent<PlayerAttackController>();

            playerAttack.EnableAttackCollider(false);
            healthManager.DecreaseHealth(playerAttack.GetCurrentAttack().Damage);
        }
    }
}
