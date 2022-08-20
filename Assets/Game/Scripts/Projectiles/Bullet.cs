using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The bullet work is to instantiate the projectile using a valid position.
public class Bullet : MonoBehaviour
{
    [SerializeField] protected bool spawnPrefab = false;
    [SerializeField] protected GameObject projectilePrefab;
    
    [SerializeField]
    protected bool drawSphere = false;

    [SerializeField]
    protected bool IsStopped = false;

    public virtual void InstantiateBullet(Vector3 center)
    {
        // Nah.
    }

    void FixedUpdate()
    {
        
    }

    protected virtual void CheckOutOfGround()
    {
        if (IsStopped) return;
    
        RaycastHit hit;
    
        Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        if (Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, 2))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
        else
        {
            //transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            IsStopped = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    protected virtual void DealDamageArea(Vector3 center, float diameter)
    {
        var hitTargets =Physics.OverlapSphere(center, diameter * 0.5f);
    }
}
