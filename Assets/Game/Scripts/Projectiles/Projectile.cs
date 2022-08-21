using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private SphereCollider _collider;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 
        Debug.Log($"Has hit {other.gameObject.name}");

        if (other.tag == "Player")
        {
            
        }

        if (other.tag == "PlayerShield")
        {

        }
    }

    public void Shoot(Vector3 vel)
    {
        _rigidbody.velocity = vel;
    }

    public void ReturnBullet()
    {

    }
}