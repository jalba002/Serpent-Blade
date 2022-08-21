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

    [Header("Deflect Settings")]
    [SerializeField] protected bool canBeDeflected = false;
    [SerializeField] protected float deflectedMultiplier = 2f;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.velocity = transform.forward * 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 
        Debug.Log($"Has hit {other.gameObject.name}");

        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealthManager>().DecreaseHealth(20f);
        }

        if (other.tag == "PlayerShield")
        {
            ReturnProjectile();
        }
    }

    public void Shoot(Vector3 vel)
    {
        _rigidbody.velocity = vel;
    }

    public void ReturnProjectile()
    {
        _rigidbody.velocity = -_rigidbody.velocity * deflectedMultiplier;
        tag = "PlayerDeflectedAttack";
    }
}