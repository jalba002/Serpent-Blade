using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
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

        [Header("Debug")] public bool spawn = false;

        public override void InstantiateBullet(Vector3 center)
        {
            // Maybe get the center spawn point.
            Vector3 spawnPoint = FindObjectsOfType<SpawnPoint>().ToList().Find(x => x.id == 0).transform.position;
            StartCoroutine(BulletSpawnWaves(spawnPoint));
        }

        IEnumerator BulletSpawnWaves(Vector3 center)
        {
            List<GameObject> objectsToStart = new List<GameObject>();
            int i = 0;
            float side = 360 / sides;
            while (i < waves)
            {
                for (int j = 0; j < sides; j++)
                {
                    float angle = side * j * Mathf.Deg2Rad;
                    Vector3 direction = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));
                    var go = Instantiate(projectilePrefab, center, Quaternion.identity);
                    go.SetActive(false);
                    objectsToStart.Add(go);
                    go.transform.forward = direction;
                    go.transform.position += go.transform.right * (Mathf.Sin(Time.timeSinceLevelLoad) * sineAmplitude);
                    go.GetComponent<Projectile>().Shoot(direction * projectileSpeed);
                    yield return null;
                    //.velocity = direction * projectileSpeed;
                }

                foreach (var obj in objectsToStart)
                {
                    obj.SetActive(true);
                }
                
                i++;
                objectsToStart = new List<GameObject>();
                yield return new WaitForSeconds(delayBetweenWaves);
            }
        }

        private void OnValidate()
        {
            if (spawn)
            {
                spawn = false;
                InstantiateBullet(Vector3.zero);
            }
        }
    }
}