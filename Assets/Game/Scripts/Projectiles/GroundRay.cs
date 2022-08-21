using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.VFX;

public class GroundRay : Bullet
{
    private Vector3 spawnPoint;
    private float ringSize;
    public override void InstantiateBullet(Vector3 center)
    {
        spawnPoint = center;
        var visualEffect = GetComponentInChildren<VisualEffect>();
        var delay = visualEffect.GetFloat("Ring Duration");
        ringSize = visualEffect.GetFloat("Ring Max Size");
        var maxParticleLifetime = visualEffect.GetFloat("Sparks Max Life");
        StartCoroutine(ShockGround(delay, maxParticleLifetime));
    }

    IEnumerator ShockGround(float delay, float maxLifetime)
    {
        yield return new WaitForSeconds(delay);
        DealDamageArea(spawnPoint, ringSize);
        drawSphere = true;
        Destroy(this.gameObject, maxLifetime * 2f);
    }
    
    private void OnDrawGizmos()
    {
        if (!drawSphere) return;
        
        Gizmos.DrawWireSphere(spawnPoint, ringSize*0.5f);
    }
}
