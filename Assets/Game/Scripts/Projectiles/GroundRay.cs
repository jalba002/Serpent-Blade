using FMODUnity;
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
    private StudioEventEmitter audioRef;

    private void Awake()
    {
        audioRef = GetComponent<StudioEventEmitter>();
    }

    public override void InstantiateBullet(Vector3 center)
    {
        spawnPoint = center;
        var visualEffect = GetComponentInChildren<VisualEffect>();
        var delay = visualEffect.GetFloat("Ring Duration");
        ringSize = visualEffect.GetFloat("Ring Max Size");
        var groundMark = visualEffect.GetFloat("Ground Mark Duration");
        StartCoroutine(ShockGround(delay, groundMark));
    }

    IEnumerator ShockGround(float delay, float maxLifetime)
    {
        yield return new WaitForSeconds(delay);
        DealDamageArea(spawnPoint, ringSize);
        drawSphere = true;
        audioRef.Play();
        Destroy(this.gameObject, maxLifetime + 1f);
    }
    
    private void OnDrawGizmos()
    {
        if (!drawSphere) return;
        
        Gizmos.DrawWireSphere(spawnPoint, ringSize*0.5f);
    }
}
