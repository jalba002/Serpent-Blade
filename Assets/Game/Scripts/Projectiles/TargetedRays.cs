using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class TargetedRays : Bullet
{
    public float spellDuration = 10f;
    public float delayBetweenRays = 0.5f;

    private IEnumerator raySpawner;
    public override void InstantiateBullet(Vector3 center)
    {
        base.InstantiateBullet(center);
        raySpawner = SpawnRaysOverTime();
        StartCoroutine(raySpawner);
    }

    IEnumerator SpawnRaysOverTime()
    {
        int projectiles = (int)(spellDuration / delayBetweenRays);
        PlayerAttackController player = FindObjectOfType<PlayerAttackController>();
        while (projectiles > 0)
        {
            Vector3 playerPos = player.transform.position;
            var a = Instantiate(projectilePrefab, playerPos, Quaternion.identity);
            a.GetComponent<Bullet>().InstantiateBullet(playerPos);
            projectiles--;
            yield return new WaitForSeconds(delayBetweenRays);
        }
        Destroy(this.gameObject);
    }
}
