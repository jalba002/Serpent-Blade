using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The bullet work is to instantiate the projectile using a valid position.
public class Bullet : MonoBehaviour
{
    [SerializeField] protected bool spawnPrefab = false;
    [SerializeField] protected GameObject projectilePrefab;
    public virtual void InstantiateBullet(Vector3 center)
    {
        
    }
}
