using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    private SphereCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 
        Debug.Log($"Has hit {other.gameObject.name}");
    }
}
