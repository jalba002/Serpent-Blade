using System.Collections;
using System.Linq;
using UnityEngine;

namespace Projectiles
{
    public class Sunshinee : Bullet
    {
        public float sineAmplitude = 2f;
        public int sides = 4;
        public float delayBetweenWaves = 0.1f;
        public float waves = 16;
        public float projectileSpeed = 8f;

        public override void InstantiateBullet(Vector3 center)
        {
            // Maybe get the center spawn point.
            Vector3 spawnPoint = FindObjectsOfType<SpawnPoint>().ToList().Find(x => x.id == 0).transform.position;
            StartCoroutine(BulletSpawnWaves(spawnPoint));
        }

        IEnumerator BulletSpawnWaves(Vector3 center)
        {
            int i = 0;
            while (i < waves)
            {
                for (int j = 0; j < sides; j++)
                {
                    float angle = 360 / sides * j;
                    angle *= Mathf.Deg2Rad;
                    Vector3 direction = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));
                    var go = Instantiate(projectilePrefab, center, Quaternion.identity);
                    go.transform.forward = direction;
                    go.transform.position += go.transform.right * (Mathf.Sin(Time.timeSinceLevelLoad) * sineAmplitude);
                    go.GetComponent<Projectile>().Shoot(direction * projectileSpeed);
                        //.velocity = direction * projectileSpeed;
                }

                i++;
                yield return new WaitForSeconds(delayBetweenWaves);
            }
        }
    }
}